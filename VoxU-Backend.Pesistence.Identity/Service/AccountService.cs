using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxU_Backend.Core.Application.DTOS.Account;
using VoxU_Backend.Core.Application.DTOS.Email;
using VoxU_Backend.Core.Application.Enums;
using VoxU_Backend.Core.Application.Interfaces.Services;
using VoxU_Backend.Pesistence.Identity.Entities;

namespace VoxU_Backend.Pesistence.Identity.Service
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }


        //Authenticate
        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest authenticationRequest)
        {
            AuthenticationResponse Response = new();
            Response.HasError = false;

            var user = await _userManager.FindByNameAsync(authenticationRequest.CollegeId);

            if (user == null)
            {
                Response.HasError = true;
                Response.Error = $"The user inserted {authenticationRequest.CollegeId} does not exist";
                return Response;
            }


            var result = await _signInManager.PasswordSignInAsync(user.UserName, authenticationRequest.Password, false, false);

            if (!result.Succeeded)
            {
                Response.HasError = true;
                Response.Error = $"The password inserted is incorrect";
                return Response;
            }

            if (!user.EmailConfirmed)
            {
                Response.HasError = true;
                Response.Error = $"The user inserted needs its email to be confirmed";
                return Response;
            }

            Response.Id = user.Id;
            Response.collegeId = user.UserName;
            Response.Name = user.Name;
            Response.LastName = user.LastName;
            Response.User = user.User;
            Response.ProfilePicture = user.ProfilePicture;
            Response.PhoneNumber = user.PhoneNumber;
            Response.IsVerified = user.EmailConfirmed;
            Response.Email = user.Email;
            return Response;
        }

        //LogOut
        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }


        //Register
        public async Task<RegisterResponse> RegisterAsync(RegisterRequest registerRequest, string origin)
        {
            RegisterResponse response = new();
            response.HasError = false;

            //Verify the user introduced doesn't exist
            var user = await _userManager.FindByNameAsync(registerRequest.collegeId);

            if (user != null)
            {
                response.HasError = true;
                response.Error = $"The user: {registerRequest.collegeId} already exist, try another user !";
                return response;
            }

            //Add New User
            ApplicationUser newUser = new();
            newUser.UserName = registerRequest.collegeId;
            newUser.Name = registerRequest.Name;
            newUser.LastName = registerRequest.LastName;
            newUser.User = registerRequest.User;
            newUser.PhoneNumber = registerRequest.PhoneNumber;
            newUser.Email = registerRequest.Email;
            newUser.ProfilePicture = registerRequest.ProfilePicture;
            newUser.Created_At = registerRequest.Created_At;

            var result = await _userManager.CreateAsync(newUser, registerRequest.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, Roles.Basic.ToString());
                var emailVerificationUri = await GetConfirmAccountUri(newUser, origin);
                await _emailService.SendAsync(new EmailRequest
                {
                    To = registerRequest.Email,
                    Subject = "User Verification",
                    Body = $"Please verificate your account by clicking on the following verification link: {emailVerificationUri}"
                });
                response.UserId = newUser.Id;
            }

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"There was an error while attempting to register the user";
                return response;
            }

            return response;
        }



        //ConfirmAccount
        public async Task<string> ConfirmAccountAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return "There is not user registered with the given user";
            }

            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token)); //Decode the token
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return "Your account was succesfully confirmed !";
            }
            else
            {
                return "There was an error while attemting to confirm your account";
            }

        }

        //SendNewPassword
        public async Task<SendNewPasswordResponse> SendNewPasswordToEmail(SendNewPasswordRequest newPasswordRequest)
        {
            SendNewPasswordResponse response = new();
            response.HasError = false;

            var user = await _userManager.FindByNameAsync(newPasswordRequest.CollegeId);

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"The user {newPasswordRequest.CollegeId} does not exists !";
                return response;
            }

            // generate and assign to the user a new password 
            var newPassword = Guid.NewGuid().ToString().Substring(1, 16);
            var oldPassword = newPasswordRequest.OldPassword;

            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"The password inserted was wrong!";
                return response;
            }


            if (result.Succeeded && newPassword != oldPassword)
            {
                // Send to user's email the new generated password
                await _emailService.SendAsync(new EmailRequest
                {
                    To = user.Email,
                    Subject = "Create New Password Request",
                    Body = $"Here's your new password: {newPassword}"
                });
            }

            return response;
        }


        //ConfirmAccountUri
        private async Task<string> GetConfirmAccountUri(ApplicationUser user, string origin)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));            // Encondear el token
            string route = "User/ConfirmEmail";
            var uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(uri.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "token", token);

            return verificationUri;
        }

        //ForgotPasswordUri - generates an uri with a token with change email purposes
        private async Task<string> GetForgotPasswordUri(ApplicationUser user, string origin)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));            // Encondear el token
            string route = "User/ResetPassword";
            var uri = new Uri(string.Concat($"{origin}/", route));
            var forgotPasswordUri = QueryHelpers.AddQueryString(uri.ToString(), "token", token);

            return forgotPasswordUri;
        }


        //Update user/account
        public async Task<string> UpdateImage(string UserId, string ImageLocation)
        {
            var editedUser = await _userManager.FindByIdAsync(UserId);
            editedUser.ProfilePicture = ImageLocation;

            var result = await _userManager.UpdateAsync(editedUser);

            if (result.Succeeded)
            {
                return "The user was successfully updated";

            }
            else
            {
                return "Something went wrong while trying to update the user";
            }
        }

        //Find User By UserName
        //public async Task<FindUserResponse> FindUserName(FindUserRequest userRequest)
        //{
        //    FindUserResponse userResponse = new();
        //    userResponse.HasError = false;

        //    var user = await _userManager.FindByNameAsync(userRequest.userName);

        //    if (user == null)
        //    {
        //        userResponse.HasError = true;
        //        userResponse.error = "The user does not exists !";
        //        return userResponse;
        //    }
        //    else
        //    {
        //        userResponse.UserId = user.Id;
        //        userResponse.FriendUserName = user.UserName;
        //        userResponse.FriendPicture = user.ProfilePicture;
        //        return userResponse;
        //    }

        //}

        //Find Image By UserId
        public async Task<string> FindImageUserId(string userId)
        {

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return "No Picture";
            }

            string profilePicture = user.ProfilePicture;
            return profilePicture;

        }

        public async Task<string> FindUserNameById(string userId)
        {

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return "No Picture";
            }

            string userName = user.UserName;
            return userName;

        }


      

    }
}
