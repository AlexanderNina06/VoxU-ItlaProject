﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxU_Backend.Core.Application.DTOS.Account;
using VoxU_Backend.Core.Application.DTOS.Account.Delete;

namespace VoxU_Backend.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest authenticationRequest);
        Task<string> ConfirmAccountAsync(string userId, string token);
        Task LogOut();
        Task<RegisterResponse> RegisterAsync(RegisterRequest registerRequest, string origin);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPassword passwordRequest, string origin);
       Task<ResetPasswordResponse> ResetPasswordAsync(ResetPassword passwordRequest);
        Task<byte[]> FindImageUserId(string userId);
        Task<string> FindUserNameById(string userId);
        Task<UpdateAccountResponse> UpdateUser(UpdateAccountRequest updateAccountRequest);
        Task<List<GetUsersResponse>> GetAllUsersAsync();
        Task<DeleteUserResponse> DeleteUser(string userName);
        Task<lockUnlockUserResponse> lockUnlockUser(string userName);
        Task<List<GetUsersResponse>> GetActiveUsers();
    }
}
