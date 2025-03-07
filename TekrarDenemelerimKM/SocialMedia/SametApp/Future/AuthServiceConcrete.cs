using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SocialMediaApp.Core.Entities;
using SocialMediaApp.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        
        public AuthService(UserManager<User> userManager, IMapper mapper, IConfiguration configuration)
        {
            _userManager = userManager;
            _mapper = mapper;
            _configuration = configuration;
        }
        
        public async Task<ResponseDto<TokenDto>> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            
            if (user == null)
            {
                return new ResponseDto<TokenDto> { Success = false, Message = "Email or password is incorrect" };
            }
            
            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return new ResponseDto<TokenDto> { Success = false, Message = "Email or password is incorrect" };
            }
            
            var token = CreateToken(user);
            var refreshToken = CreateRefreshToken();
            
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiration = DateTime.Now.AddDays(7);
            
            await _userManager.UpdateAsync(user);
            
            return new ResponseDto<TokenDto> { Data = token };
        }
        
        public async Task<ResponseDto<UserDto>> RegisterAsync(RegisterDto registerDto)
        {
            var user = new User
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                Name = registerDto.Name,
                Surname = registerDto.Surname
            };
            
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            
            if (!result.Succeeded)
            {
                return new ResponseDto<UserDto> 
                { 
                    Success = false, 
                    Message = string.Join(", ", result.Errors.Select(e => e.Description)) 
                };
            }
            
            return new ResponseDto<UserDto> { Data = _mapper.Map<UserDto>(user) };
        }
        
        public async Task<ResponseDto<NoDataDto>> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
            
            if (user == null)
            {
                return new ResponseDto<NoDataDto> { Success = false, Message = "User not found" };
            }
            
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            
            // Burada e-posta gönderme işlemi yapılabilir
            
            return new ResponseDto<NoDataDto> { Message = "Password reset link has been sent to your email" };
        }
        
        public async Task<ResponseDto<NoDataDto>> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            
            if (user == null)
            {
                return new ResponseDto<NoDataDto> { Success = false, Message = "User not found" };
            }
            
            var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.NewPassword);
            
            if (!result.Succeeded)
            {
                return new ResponseDto<NoDataDto> 
                { 
                    Success = false, 
                    Message = string.Join(", ", result.Errors.Select(e => e.Description)) 
                };
            }
            
            return new ResponseDto<NoDataDto> { Message = "Password has been reset successfully" };
        }
        
        public async Task<ResponseDto<TokenDto>> RefreshTokenAsync(string refreshToken)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshToken == refreshToken && u.RefreshTokenExpiration > DateTime.Now);
            
            if (user == null)
            {
                return new ResponseDto<TokenDto> { Success = false, Message = "Refresh token is invalid or expired" };
            }
            
            var token = CreateToken(user);
            var newRefreshToken = CreateRefreshToken();
            
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiration = DateTime.Now.AddDays(7);
            
            await _userManager.UpdateAsync(user);
            
            return new ResponseDto<TokenDto> { Data = token };
        }
        
        public async Task<ResponseDto<NoDataDto>> RevokeRefreshTokenAsync(string refreshToken)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshToken == refreshToken);
            
            if (user == null)
            {
                return new ResponseDto<NoDataDto> { Success = false, Message = "User not found" };
            }
            
            user.RefreshToken = null;
            user.RefreshTokenExpiration = null;
            
            await _userManager.UpdateAsync(user);
            
            return new ResponseDto<NoDataDto> { Message = "Refresh token has been revoked" };
        }
        
        private TokenDto CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName)
            };
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.Now.AddHours(3);
            
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );
            
            return new TokenDto
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
                RefreshToken = user.RefreshToken
            };
        }
        
        private string CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}