using System;
using EShop.Entity.Concrete;
using EShop.Shared.Dtos;
using EShop.Shared.Dtos.Auth;
using EShop.Shared.Dtos.ResponseDtos;

namespace EShop.Service.Abstract;

public interface IAuthService
{
 Task<ResponseDto<TokenDto>> LoginAsycn(LoginDto loginDto);
 Task<ResponseDto<ApplicationUserDto>> RegisterAync(RegisterDto registerDto);
 Task<ResponseDto<NoContent>> ForgetPasswordAsync(ForgotPasswordDto  forgotPasswordDto);
 Task<ResponseDto<NoContent>> ChangePasswordAsync(ChangePasswordDto changePasswordDto);
 Task<ResponseDto<NoContent>> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);

}
