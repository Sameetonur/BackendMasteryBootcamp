// SocialMediaApp.Service/Services/IUserService.cs
using SocialMediaApp.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMediaApp.Service.Services
{
    public interface IUserService
    {
        Task<ResponseDto<UserDto>> GetByIdAsync(string id);
        Task<ResponseDto<UserDto>> GetByUsernameAsync(string username);
        Task<ResponseDto<List<UserDto>>> GetAllAsync();
        Task<ResponseDto<List<UserDto>>> SearchUsersAsync(string searchTerm);
        Task<ResponseDto<UserDto>> UpdateProfileAsync(string userId, UserDto userDto);
        Task<ResponseDto<NoDataDto>> ChangePasswordAsync(string userId, ChangePasswordDto changePasswordDto);
        Task<ResponseDto<List<UserDto>>> GetFollowersAsync(string userId);
        Task<ResponseDto<List<UserDto>>> GetFollowingAsync(string userId);
        Task<ResponseDto<NoDataDto>> FollowUserAsync(string followerId, string followingId);
        Task<ResponseDto<NoDataDto>> UnfollowUserAsync(string followerId, string followingId);
        Task<ResponseDto<bool>> IsFollowingAsync(string followerId, string followingId);
    }
}

// SocialMediaApp.Service/Services/IPostService.cs
using SocialMediaApp.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMediaApp.Service.Services
{
    public interface IPostService
    {
        Task<ResponseDto<PostDto>> GetByIdAsync(int id);
        Task<ResponseDto<List<PostDto>>> GetAllAsync();
        Task<ResponseDto<List<PostDto>>> GetUserPostsAsync(string userId);
        Task<ResponseDto<List<PostDto>>> GetFeedPostsAsync(string userId);
        Task<ResponseDto<List<PostDto>>> GetExplorePostsAsync();
        Task<ResponseDto<PostDto>> CreateAsync(string userId, PostDto postDto);
        Task<ResponseDto<PostDto>> UpdateAsync(string userId, int postId, PostDto postDto);
        Task<ResponseDto<NoDataDto>> DeleteAsync(string userId, int postId);
        Task<ResponseDto<NoDataDto>> LikePostAsync(string userId, int postId);
        Task<ResponseDto<NoDataDto>> UnlikePostAsync(string userId, int postId);
        Task<ResponseDto<bool>> IsLikedAsync(string userId, int postId);
        Task<ResponseDto<List<UserDto>>> GetLikesAsync(int postId);
    }
}

// SocialMediaApp.Service/Services/ICommentService.cs
using SocialMediaApp.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMediaApp.Service.Services
{
    public interface ICommentService
    {
        Task<ResponseDto<List<CommentDto>>> GetPostCommentsAsync(int postId);
        Task<ResponseDto<CommentDto>> CreateAsync(string userId, int postId, CommentDto commentDto);
        Task<ResponseDto<CommentDto>> UpdateAsync(string userId, int commentId, CommentDto commentDto);
        Task<ResponseDto<NoDataDto>> DeleteAsync(string userId, int commentId);
    }
}

// SocialMediaApp.Service/Services/IMessageService.cs
using SocialMediaApp.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMediaApp.Service.Services
{
    public interface IMessageService
    {
        Task<ResponseDto<List<MessageDto>>> GetConversationAsync(string senderId, string receiverId);
        Task<ResponseDto<List<UserDto>>> GetConversationsAsync(string userId);
        Task<ResponseDto<MessageDto>> SendMessageAsync(string senderId, string receiverId, string content);
        Task<ResponseDto<NoDataDto>> MarkAsReadAsync(string userId, int messageId);
        Task<ResponseDto<int>> GetUnreadCountAsync(string userId);
    }
}

// SocialMediaApp.Service/Services/IAuthService.cs
using SocialMediaApp.Core.Entities;
using SocialMediaApp.Shared.DTOs;
using System.Threading.Tasks;

namespace SocialMediaApp.Service.Services
{
    public interface IAuthService
    {
        Task<ResponseDto<TokenDto>> LoginAsync(LoginDto loginDto);
        Task<ResponseDto<UserDto>> RegisterAsync(RegisterDto registerDto);
        Task<ResponseDto<NoDataDto>> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto);
        Task<ResponseDto<NoDataDto>> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
        Task<ResponseDto<TokenDto>> RefreshTokenAsync(string refreshToken);
        Task<ResponseDto<NoDataDto>> RevokeRefreshTokenAsync(string refreshToken);
    }
}

// SocialMediaApp.Shared/DTOs/NoDataDto.cs
namespace SocialMediaApp.Shared.DTOs
{
    public class NoDataDto
    {
    }
}