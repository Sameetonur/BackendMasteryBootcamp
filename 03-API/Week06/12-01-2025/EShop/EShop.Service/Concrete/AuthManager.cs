using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EShop.Entity.Concrete;
using EShop.Service.Abstract;
using EShop.Shared.Configurations.Auth;
using EShop.Shared.Dtos.Auth;
using EShop.Shared.Dtos.ResponseDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EShop.Service.Concrete;

public class AuthManager :IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    
    private readonly SignInManager<ApplicationUser> _singInManager;

    private JwtConfig _jwtConfig;

    //aslında buraya başka servisler de olacak , ancak henüz yazmadık.

    public AuthManager(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> singInManager, IOptions<JwtConfig> options)
    {
        _userManager = userManager;
        _singInManager = singInManager;
        _jwtConfig = options.Value; //options pattern
    }

    public Task<ResponseDto<NoContent>> ChangePasswordAsync(ChangePasswordDto changePasswordDto)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto<NoContent>> ForgetPasswordAsync(ForgotPasswordDto forgotPasswordDto)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseDto<TokenDto>> LoginAsycn(LoginDto loginDto)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if( user == null)
            {
                return ResponseDto<TokenDto>.Fail("Kullanıcı adı veya şifre hatalı",StatusCodes.Status400BadRequest);

            }
            var isValidPassword = await _userManager.CheckPasswordAsync(user,loginDto.Password);
            if(!isValidPassword)
            {
                return ResponseDto<TokenDto>.Fail("Kullanıcı adı veya şifre hatalı", StatusCodes.Status400BadRequest);
            }
            // token yaratmamız gerekiyor. GenerateJwtToken(user).
            var tokenDto = await GenerateJwtToken(user);
            return ResponseDto<TokenDto>.Success(tokenDto,StatusCodes.Status200OK);

        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine($"Giriş yapılırken hata oluştu {ex.Message}");
            throw;
        }
    }

    public Task<ResponseDto<ApplicationUser>> RegisterAync(RegisterDto registerDto)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto<NoContent>> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
    {
        throw new NotImplementedException();
    }

    private async Task<TokenDto> GenerateJwtToken(ApplicationUser user)
    {
        try
        {
            var roles= await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),

            }.Union(roles.Select(x=> new Claim(ClaimTypes.Role,x)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Secret));
            var credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var expiry =DateTime.Now.AddDays(Convert.ToDouble(_jwtConfig.AccessTokenExpiration));

            var token = new  JwtSecurityToken(

                issuer: _jwtConfig.Issuer,
                audience: _jwtConfig.Audience,
                claims:claims,
                expires:expiry,
                signingCredentials:credentials
            );

            var tokendto = new TokenDto()
            {
                AccessToken= new  JwtSecurityTokenHandler().WriteToken(token),
                AccessTokenExpirationDate=expiry
            };
            return tokendto;

        }
        catch (System.Exception ex)
        {
            
            System.Console.WriteLine($"Token Oluşturulurken Bir hata oluştu{ex.Message}");
            throw;
        }

    }
}