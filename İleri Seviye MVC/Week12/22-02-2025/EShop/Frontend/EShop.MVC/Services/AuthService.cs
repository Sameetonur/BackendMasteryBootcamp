using System;
using EShop.MVC.Models;
using EShop.MVC.Services.Interfaces;

namespace EShop.MVC.Services
{
    public class AuthService : IAuthService
    {
        public Task<ResponseModel<NoContent>> ChangePasswordAsync(ChangePasswordModel changePasswordModel)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<NoContent>> ForgotPasswordAsync(ForgotPasswordModel forgotPasswordModel)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<TokenModel>> LoginAsync(LoginModel loginModel)
        {
            throw new NotImplementedException();
        }

        public Task LogoutAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<NoContent>> RegisterAsync(RegisterModel registerModel)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<NoContent>> ResetPasswordAsync(ResetPasswordModel resetPasswordModel)
        {
            throw new NotImplementedException();
        }
    }
}
