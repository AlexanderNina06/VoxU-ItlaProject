using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Crypto.Paddings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VoxU_Backend.Core.Application.DTOS.Account;
using VoxU_Backend.Core.Application.DTOS.Email;
using VoxU_Backend.Core.Application.Enums;
using VoxU_Backend.Core.Application.Interfaces.Services;
using VoxU_Backend.Core.Domain.Settings;
using VoxU_Backend.Pesistence.Identity.Entities;

namespace VoxU_Backend.Pesistence.Identity.Service
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly JWTSettings _JwtSettings;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailService emailService, IOptions<JWTSettings> jwt)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _JwtSettings = jwt.Value;
        }


        #region Log in/out
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
            Response.Description = user.Description;
            Response.Career = user.Career;
            Response.isBlocked = user.IsBlocked;

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            Response.Roles = rolesList.ToList();
            Response.IsVerified = user.EmailConfirmed;
            JwtSecurityToken Jwtoken = await GenerateJWToken(user);
            Response.JWToken = new JwtSecurityTokenHandler().WriteToken(Jwtoken);
            var refreshToken = GetRefreshToken();
            Response.RefreshToken = refreshToken.Token;

            return Response;
        }
       
        //LogOut
        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        #endregion

        #region JWT
        private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user)
        {
            //El token que generaremos debe de tener la info del usuario

            var userClaims = await _userManager.GetClaimsAsync(user); //Los claims son comos los permisos del usuarios
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            //Por cada role que tenga el usuario agregaremos un claim con ese rol
            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, new Guid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)

            }.Union(userClaims).Union(roleClaims);

            //Para crear un token con seguridad se necesitan dos elementos, Los SignInCredentials y el symetricSecurityTeam

            var symmectricSecuritykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JwtSettings.Key)); // Recuperamos el key y lo encondeamos
            var signInCredentials = new SigningCredentials(symmectricSecuritykey, SecurityAlgorithms.HmacSha256); // Para recuperar el signInCredential se necesita del 
                                                                                                                  //Security key y el metodo de encriptacion

            //Creando el token
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _JwtSettings.Issuer,
                audience: _JwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_JwtSettings.DurationInMinutes),
                signingCredentials: signInCredentials);

            return jwtSecurityToken;
        }
        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);

            return BitConverter.ToString(randomBytes).Replace("-", "");
        }
        private RefreshToken GetRefreshToken()
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Created = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(7)
            };
        }
        #endregion

        #region RegisterProcess
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
                response.Error = $"Ya existe un usuario con la matricula: {registerRequest.collegeId}, intente nuevamente!";
                return response;
            }

            if (!ValidatePassword(registerRequest.Password))
            {
                response.HasError = true;
                response.Error = "La contraseña debe contener al menos 8 caracteres, una letra mayúscula, una letra minúscula, un número y un carácter especial.";
                return response;
            }


            /*var emailUser = await _userManager.FindByEmailAsync(registerRequest.Email);

            if (user != emailUser)
            {
                response.HasError = true;
                response.Error = $"The email: {registerRequest.Email} already exist, try another email !";
                return response;
            }*/

            //Add New User
            ApplicationUser newUser = new();
            newUser.UserName = registerRequest.collegeId;
            newUser.Name = registerRequest.Name;
            newUser.LastName = registerRequest.LastName;
            newUser.User = registerRequest.User;
            newUser.PhoneNumber = registerRequest.PhoneNumber;
            newUser.Email = registerRequest.collegeId + "@itla.edu.do";
            newUser.ProfilePicture = registerRequest.ProfilePicture;
            newUser.Created_At = registerRequest.Created_At;
            newUser.Career = registerRequest.Career;
            newUser.Description = registerRequest.Description;
            newUser.IsBlocked = false;
            var result = await _userManager.CreateAsync(newUser, registerRequest.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, Roles.Basic.ToString());
                var emailVerificationUri = await SendVerificationEmailUri(newUser, origin);
                await _emailService.SendAsync(new EmailRequest
                {
                    To = newUser.Email,
                    Subject = "Verificación de Usuario",
                    Body = $"Por favor, verifica tu cuenta haciendo clic en el siguiente enlace de verificación: {emailVerificationUri}"
                });
                response.UserId = newUser.Id;
            }

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Hubo un error al intentar registrar al usuario";
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

        //Forgot Password -- enviar correo con url para reset Password
        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPassword passwordRequest, string origin)
        {
            ForgotPasswordResponse response = new();
            response.HasError = false;

            var accountExist = await _userManager.FindByEmailAsync(passwordRequest.Email);

            if (accountExist == null)
            {
                response.HasError = true;
                response.Error = $"No account exits with the requested email: {passwordRequest.Email}";
            }

            var resetPasswordUri = await SendForgotPasswordUri(accountExist, origin);
            await _emailService.SendAsync(new EmailRequest
            {
                To = accountExist.Email,
                Body = $"Please reset your password by clicking on this url {resetPasswordUri}",
                Subject = "Password Reset"
            });

            return response;
        }

        //Metodo para resetear el password
        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPassword passwordRequest)
        {
            ResetPasswordResponse response = new();
            response.HasError = false;

            var accountExist = await _userManager.FindByEmailAsync(passwordRequest.Email);
            if (accountExist == null)
            {
                response.HasError = true;
                response.Error = $"No account exits with the requested email: {passwordRequest.Email}";
            }

            // Decodeo el token
            passwordRequest.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(passwordRequest.Token));
            var result = await _userManager.ResetPasswordAsync(accountExist, passwordRequest.Token, passwordRequest.Password);

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = "There was an error while attempting to reset the password";
            }


            return response;
        }


        #endregion


        #region Email
        //Generar uri con el token de reset password 
        private async Task<string> SendForgotPasswordUri(ApplicationUser user, string origin)
        {
            // Genero un token de solicitud de cambio de password
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code)); // Encodeamos el token asociado a usuario para poder agregarselo a la url
            var route = "api/Account/ResetPassword"; // Ruta donde ira -- controlador/action
            var Uri = new Uri(string.Concat($"{origin}/", route)); // Concatemos el origen y la ruta
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "token", code);

            return verificationUri;
        }

        // Metodo para generar el uri con el id y token de confirmacion
        private async Task<string> SendVerificationEmailUri(ApplicationUser user, string origin)
        {
            //Se necesita formar una url que sea valida para el usuario y asi pueda confirmar sus cuentas

            //Logica para enviar un codigo con token de confirmacion para activar/validar cuentas
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code)); // Encodeamos el token asociado a usuario para poder agregarselo a la url
            var route = "api/Account/ConfirmEmail";
            var Uri = new Uri(string.Concat($"{origin}/", route)); // Concatemos el origen y la ruta
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "userId", user.Id); // Agregamos el userId y el token de confirmacion 
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "token", code);
            //Utilizamos la uri que previamente tenia el userId y le agregamos el token, basicamente el valor actual de la uri mas el token
            return verificationUri;
        }



        #endregion



        //Update user/account
        public async Task<string> UpdateImage(string UserId, byte[] ImageLocation)
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
        public async Task<byte[]> FindImageUserId(string userId)
        {

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                byte[] bytes1 = new byte[1];
                return bytes1;
            }

            byte[] profilePicture = user.ProfilePicture;
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

        public async Task<UpdateAccountResponse> UpdateUser(UpdateAccountRequest updateAccountRequest)
        {
            UpdateAccountResponse response = new();
            response.HasError = false;

            var user = await _userManager.FindByEmailAsync(updateAccountRequest.Email);

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No existe un usuario vinculado con el Email: {updateAccountRequest.Email}";
            }

            user.Name = updateAccountRequest.Name;
            user.LastName = updateAccountRequest.LastName;
            user.PhoneNumber = updateAccountRequest.PhoneNumber;
            user.ProfilePicture = updateAccountRequest.ProfilePicture;
            user.Email = updateAccountRequest.Email;
            user.Career = updateAccountRequest.Email;
            user.Description = updateAccountRequest.Email;
            user.IsBlocked = updateAccountRequest.IsBlocked;

            var userResult = await _userManager.UpdateAsync(user);
            if (!userResult.Succeeded)
            {
                response.HasError = true;
                response.Error = "There was an error while attempting to update the user's information";
            }

            return response;
        }
        private bool ValidatePassword(string password)
        {
            const int minLength = 8;
            var hasUpper = password.Any(char.IsUpper);
            var hasLower = password.Any(char.IsLower);
            var hasSymbol = password.Any(c => !char.IsLetterOrDigit(c));

            return password.Length >= minLength && hasUpper && hasLower && hasSymbol;
        }

        public async Task<List<GetUsersResponse>> GetAllUsersAsync()
        {
            var userList = await _userManager.Users.ToListAsync();
            return userList.Select(user => new GetUsersResponse
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                User = user.User,
                IsBlocked = user.IsBlocked,
                Description = user.Description,
                Career = user.Career,
                ProfilePicture = user.ProfilePicture,
                Created_At = user.Created_At
            }).ToList();

        }
    }
}
