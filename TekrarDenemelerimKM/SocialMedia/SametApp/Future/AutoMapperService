using AutoMapper;
using SocialMediaApp.Core.Entities;
using SocialMediaApp.Shared.DTOs;

namespace SocialMediaApp.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            // User -> UserDto
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.FollowersCount, opt => opt.MapFrom(src => src.Followers.Count))
                .ForMember(dest => dest.FollowingCount, opt => opt.MapFrom(src => src.Following.Count))
                .ForMember(dest => dest.PostsCount, opt => opt.MapFrom(src => src.Posts.Count))
                .ForMember(dest => dest.LikesCount, opt => opt.MapFrom(src => src.Likes.Count));
            
            // Post -> PostDto
            CreateMap<Post, PostDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.UserProfilePicture, opt => opt.MapFrom(src => src.User.ProfilePicture))
                .ForMember(dest => dest.LikesCount, opt => opt.MapFrom(src => src.Likes.Count))
                .ForMember(dest => dest.CommentsCount, opt => opt.MapFrom(src => src.Comments.Count));
            
            // Comment -> CommentDto
            CreateMap<Comment, CommentDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.UserProfilePicture, opt => opt.MapFrom(src => src.User.ProfilePicture));
            
            // Message -> MessageDto
            CreateMap<Message, MessageDto>()
                .ForMember(dest => dest.SenderUserName, opt => opt.MapFrom(src => src.Sender.UserName))
                .ForMember(dest => dest.SenderProfilePicture, opt => opt.MapFrom(src => src.Sender.ProfilePicture))
                .ForMember(dest => dest.ReceiverUserName, opt => opt.MapFrom(src => src.Receiver.UserName))
                .ForMember(dest => dest.ReceiverProfilePicture, opt => opt.MapFrom(src => src.Receiver.ProfilePicture));
        }
    }
}