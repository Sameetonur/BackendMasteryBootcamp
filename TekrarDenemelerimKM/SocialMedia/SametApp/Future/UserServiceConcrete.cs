using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Core.Entities;
using SocialMediaApp.Data.UnitOfWork;
using SocialMediaApp.Shared.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaApp.Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDto<UserDto>> GetByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return new ResponseDto<UserDto> { Success = false, Message = "User not found" };
            }

            var userDto = _mapper.Map<UserDto>(user);

            return new ResponseDto<UserDto> { Data = userDto };
        }

        public async Task<ResponseDto<UserDto>> GetByUsernameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return new ResponseDto<UserDto> { Success = false, Message = "User not found" };
            }

            var userDto = _mapper.Map<UserDto>(user);

            return new ResponseDto<UserDto> { Data = userDto };
        }

        public async Task<ResponseDto<List<UserDto>>> GetAllAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var usersDto = _mapper.Map<List<UserDto>>(users);

            return new ResponseDto<List<UserDto>> { Data = usersDto };
        }

        public async Task<ResponseDto<List<UserDto>>> SearchUsersAsync(string searchTerm)
        {
            var users = await _userManager.Users
                .Where(u => u.UserName.Contains(searchTerm) ||
                            u.Name.Contains(searchTerm) ||
                            u.Surname.Contains(searchTerm) ||
                            u.Email.Contains(searchTerm))
                .ToListAsync();

            var usersDto = _mapper.Map<List<UserDto>>(users);

            return new ResponseDto<List<UserDto>> { Data = usersDto };
        }

        public async Task<ResponseDto<UserDto>> UpdateProfileAsync(string userId, UserDto userDto)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return new ResponseDto<UserDto> { Success = false, Message = "User not found" };
            }

            user.Name = userDto.Name;
            user.Surname = userDto.Surname;
            user.Bio = userDto.Bio;
            user.ProfilePicture = userDto.ProfilePicture;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return new ResponseDto<UserDto>
                {
                    Success = false,
                    Message = string.Join(", ", result.Errors.Select(e => e.Description))
                };
            }

            var updatedUserDto = _mapper.Map<UserDto>(user);

            return new ResponseDto<UserDto> { Data = updatedUserDto };
        }

        public async Task<ResponseDto<NoDataDto>> ChangePasswordAsync(string userId, ChangePasswordDto changePasswordDto)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return new ResponseDto<NoDataDto> { Success = false, Message = "User not found" };
            }

            var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.OldPassword, changePasswordDto.NewPassword);

            if (!result.Succeeded)
            {
                return new ResponseDto<NoDataDto>
                {
                    Success = false,
                    Message = string.Join(", ", result.Errors.Select(e => e.Description))
                };
            }

            return new ResponseDto<NoDataDto> { Message = "Password has been changed successfully" };
        }

        public async Task<ResponseDto<List<UserDto>>> GetFollowersAsync(string userId)
        {
            var followers = await _unitOfWork.UserFollows.Where(uf => uf.FollowingId == userId)
                .Select(uf => uf.Follower)
                .ToListAsync();

            var followersDto = _mapper.Map<List<UserDto>>(followers);

            return new ResponseDto<List<UserDto>> { Data = followersDto };
        }

        public async Task<ResponseDto<List<UserDto>>> GetFollowingAsync(string userId)
        {
            var following = await _unitOfWork.UserFollows.Where(uf => uf.FollowerId == userId)
                .Select(uf => uf.Following)
                .ToListAsync();

            var followingDto = _mapper.Map<List<UserDto>>(following);

            return new ResponseDto<List<UserDto>> { Data = followingDto };
        }

        public async Task<ResponseDto<NoDataDto>> FollowUserAsync(string followerId, string followingId)
        {
            if (followerId == followingId)
            {
                return new ResponseDto<NoDataDto> { Success = false, Message = "You cannot follow yourself" };
            }

            var isFollowing = await _unitOfWork.UserFollows.AnyAsync(uf =>
                uf.FollowerId == followerId && uf.FollowingId == followingId);

            if (isFollowing)
            {
                return new ResponseDto<NoDataDto> { Success = false, Message = "You are already following this user" };
            }

            var userFollow = new UserFollow
            {
                FollowerId = followerId,
                FollowingId = followingId
            };

            await _unitOfWork.UserFollows.AddAsync(userFollow);
            await _unitOfWork.CommitAsync();

            return new ResponseDto<NoDataDto> { Message = "User followed successfully" };
        }

        public async Task<ResponseDto<NoDataDto>> UnfollowUserAsync(string followerId, string followingId)
        {
            var userFollow = await _unitOfWork.UserFollows.Where(uf =>
                uf.FollowerId == followerId && uf.FollowingId == followingId)
                .FirstOrDefaultAsync();

            if (userFollow == null)
            {
                return new ResponseDto<NoDataDto> { Success = false, Message = "You are not following this user" };
            }

            _unitOfWork.UserFollows.Remove(userFollow);
            await _unitOfWork.CommitAsync();

            return new ResponseDto<NoDataDto> { Message = "User unfollowed successfully" };
        }

        public async Task<ResponseDto<bool>> IsFollowingAsync(string followerId, string followingId)
        {
            var isFollowing = await _unitOfWork.UserFollows.AnyAsync(uf =>
                uf.FollowerId == followerId && uf.FollowingId == followingId);

            return new ResponseDto<bool> { Data = isFollowing };
        }
    }
}