// SocialMediaApp.Shared/DTOs/UserDto.cs
using System;
using System.Collections.Generic;

namespace SocialMediaApp.Shared.DTOs
{
    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ProfilePicture { get; set; }
        public string Bio { get; set; }
        public DateTime CreatedAt { get; set; }
        public int FollowersCount { get; set; }
        public int FollowingCount { get; set; }
        public int PostsCount { get; set; }
        public int LikesCount { get; set; }
        public bool IsFollowing { get; set; }
    }
}

// SocialMediaApp.Shared/DTOs/PostDto.cs
using System;
using System.Collections.Generic;

namespace SocialMediaApp.Shared.DTOs
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserProfilePicture { get; set; }
        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }
        public bool IsLiked { get; set; }
        public List<CommentDto> Comments { get; set; }
    }
}

// SocialMediaApp.Shared/DTOs/CommentDto.cs
using System;

namespace SocialMediaApp.Shared.DTOs
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserProfilePicture { get; set; }
    }
}

// SocialMediaApp.Shared/DTOs/MessageDto.cs
using System;

namespace SocialMediaApp.Shared.DTOs
{
    public class MessageDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public string SenderId { get; set; }
        public string SenderUserName { get; set; }
        public string SenderProfilePicture { get; set; }
        public string ReceiverId { get; set; }
        public string ReceiverUserName { get; set; }
        public string ReceiverProfilePicture { get; set; }
    }
}

// SocialMediaApp.Shared/DTOs/LoginDto.cs
namespace SocialMediaApp.Shared.DTOs
{
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

// SocialMediaApp.Shared/DTOs/RegisterDto.cs
namespace SocialMediaApp.Shared.DTOs
{
    public class RegisterDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}

// SocialMediaApp.Shared/DTOs/ResetPasswordDto.cs
namespace SocialMediaApp.Shared.DTOs
{
    public class ResetPasswordDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}

// SocialMediaApp.Shared/DTOs/ForgotPasswordDto.cs
namespace SocialMediaApp.Shared.DTOs
{
    public class ForgotPasswordDto
    {
        public string Email { get; set; }
    }
}

// SocialMediaApp.Shared/DTOs/ChangePasswordDto.cs
namespace SocialMediaApp.Shared.DTOs
{
    public class ChangePasswordDto
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}

// SocialMediaApp.Shared/DTOs/TokenDto.cs
namespace SocialMediaApp.Shared.DTOs
{
    public class TokenDto
    {
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
        public string RefreshToken { get; set; }
    }
}

// SocialMediaApp.Shared/DTOs/ResponseDto.cs
namespace SocialMediaApp.Shared.DTOs
{
    public class ResponseDto<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; }
    }
}