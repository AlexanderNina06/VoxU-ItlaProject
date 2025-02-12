using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxU_Backend.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest authenticationRequest);
        Task<string> ConfirmAccountAsync(string userId, string token);
        Task LogOut();
        Task<RegisterResponse> RegisterAsync(RegisterRequest registerRequest, string origin);
        Task<SendNewPasswordResponse> SendNewPasswordToEmail(SendNewPasswordRequest newPasswordRequest);
        Task<string> UpdateImage(string UserId, string ImageLocation);
        Task<FindUserResponse> FindUserName(FindUserRequest userRequest);
        Task<string> FindImageUserId(string userId);
        Task<string> FindUserNameById(string userId);
        Task<UpdateAccountResponse> UpdateUser(UpdateAccountRequest updateAccountRequest);

    }
}
