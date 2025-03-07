using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Core.Entities;
using SocialMediaApp.Data.UnitOfWork;
using SocialMediaApp.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaApp.Service.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PostService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDto<PostDto>> GetByIdAsync(int id)
        {
            var post = await _unitOfWork.Posts.Where(p => p.Id == id && p.IsActive)
                .Include(p => p.User)
                .Include(p => p.Likes)
                .Include(p => p.Comments)
                    .ThenInclude(c => c.User)
                .FirstOrDefaultAsync();

            if (post == null)
            {
                return new ResponseDto<PostDto> { Success = false, Message = "Post not found" };
            }

            var postDto = _mapper.Map<PostDto>(post);

            return new ResponseDto<PostDto> { Data = postDto };
        }

        public async Task<ResponseDto<List<PostDto>>> GetAllAsync()
        {
            var posts = await _unitOfWork.Posts.Where(p => p.IsActive)
                .Include(p => p.User)
                .Include(p => p.Likes)
                .Include(p => p.Comments)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            var postsDto = _mapper.Map<List<PostDto>>(posts);

            return new ResponseDto<List<PostDto>> { Data = postsDto };
        }

        public async Task<ResponseDto<List<PostDto>>> GetUserPostsAsync(string userId)
        {
            var posts = await _unitOfWork.Posts.Where(p => p.UserId == userId && p.IsActive)
                .Include(p => p.User)
                .Include(p => p.Likes)
                .Include(p => p.Comments)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            var postsDto = _mapper.Map<List<PostDto>>(posts);

            return new ResponseDto<List<PostDto>> { Data = postsDto };
        }

        public async Task<ResponseDto<List<PostDto>>> GetFeedPostsAsync(string userId)
        {
            // Kullanıcının takip ettiği kullanıcıların ID'lerini al
            var followingIds = await _unitOfWork.UserFollows.Where(uf => uf.FollowerId == userId)
                .Select(uf => uf.FollowingId)
                .ToListAsync();

            // Takip edilen kullanıcıların gönderilerini al
            var posts = await _unitOfWork.Posts.Where(p => followingIds.Contains(p.UserId) && p.IsActive)
                .Include(p => p.User)
                .Include(p => p.Likes)
                .Include(p => p.Comments)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            var postsDto = _mapper.Map<List<PostDto>>(posts);

            // Kullanıcının beğendiği gönderileri işaretle
            foreach (var postDto in postsDto)
            {
                postDto.IsLiked = await _unitOfWork.Likes.AnyAsync(l => l.PostId == postDto.Id && l.UserId == userId);
            }

            return new ResponseDto<List<PostDto>> { Data = postsDto };
        }

        public async Task<ResponseDto<List<PostDto>>> GetExplorePostsAsync()
        {
            var posts = await _unitOfWork.Posts.Where(p => p.IsActive)
                .Include(p => p.User)
                .Include(p => p.Likes)
                .Include(p => p.Comments)
                .OrderByDescending(p => p.Likes.Count)
                .ThenByDescending(p => p.Comments.Count)
                .ThenByDescending(p => p.CreatedAt)
                .Take(100)
                .ToListAsync();

            var postsDto = _mapper.Map<List<PostDto>>(posts);

            return new ResponseDto<List<PostDto>> { Data = postsDto };
        }

        public async Task<ResponseDto<PostDto>> CreateAsync(string userId, PostDto postDto)
        {
            var post = new Post
            {
                Content = postDto.Content,
                ImagePath = postDto.ImagePath,
                UserId = userId,
                CreatedAt = DateTime.Now
            };

            await _unitOfWork.Posts.AddAsync(post);
            await _unitOfWork.CommitAsync();

            var createdPostDto = _mapper.Map<PostDto>(post);

            return new ResponseDto<PostDto> { Data = createdPostDto };
        }

        public async Task<ResponseDto<PostDto>> UpdateAsync(string userId, int postId, PostDto postDto)
        {
            var post = await _unitOfWork.Posts.Where(p => p.Id == postId && p.UserId == userId)
                .FirstOrDefaultAsync();

            if (post == null)
            {
                return new ResponseDto<PostDto> { Success = false, Message = "Post not found or you are not authorized" };
            }

            post.Content = postDto.Content;
            post.ImagePath = postDto.ImagePath;
            post.UpdatedAt = DateTime.Now;

            _unitOfWork.Posts.Update(post);
            await _unitOfWork.CommitAsync();

            var updatedPostDto = _mapper.Map<PostDto>(post);

            return new ResponseDto<PostDto> { Data = updatedPostDto };
        }

        public async Task<ResponseDto<NoDataDto>> DeleteAsync(string userId, int postId)
        {
            var post = await _unitOfWork.Posts.Where(p => p.Id == postId && p.UserId == userId)
                .FirstOrDefaultAsync();

            if (post == null)
            {
                return new ResponseDto<NoDataDto> { Success = false, Message = "Post not found or you are not authorized" };
            }

            post.IsActive = false;

            _unitOfWork.Posts.Update(post);
            await _unitOfWork.CommitAsync();

            return new ResponseDto<NoDataDto> { Message = "Post deleted successfully" };
        }

        public async Task<ResponseDto<NoDataDto>> LikePostAsync(string userId, int postId)
        {
            var isLiked = await _unitOfWork.Likes.AnyAsync(l => l.PostId == postId && l.UserId == userId);

            if (isLiked)
            {
                return new ResponseDto<NoDataDto> { Success = false, Message = "You have already liked this post" };
            }

            var like = new Like
            {
                PostId = postId,
                UserId = userId,
                CreatedAt = DateTime.Now
            };

            await _unitOfWork.Likes.AddAsync(like);
            await _unitOfWork.CommitAsync();

            return new ResponseDto<NoDataDto> { Message = "Post liked successfully" };
        }

        public async Task<ResponseDto<NoDataDto>> UnlikePostAsync(string userId, int postId)
        {
            var like = await _unitOfWork.Likes.Where(l => l.PostId == postId && l.UserId == userId)
                .FirstOrDefaultAsync();

            if (like == null)
            {
                return new ResponseDto<NoDataDto> { Success = false, Message = "You have not liked this post" };
            }

            _unitOfWork.Likes.Remove(like);
            await _unitOfWork.CommitAsync();

            return new ResponseDto<NoDataDto> { Message = "Post unliked successfully" };
        }

        public async Task<ResponseDto<bool>> IsLikedAsync(string userId, int postId)
        {
            var isLiked = await _unitOfWork.Likes.AnyAsync(l => l.PostId == postId && l.UserId == userId);

            return new ResponseDto<bool> { Data = isLiked };
        }

        public async Task<ResponseDto<List<UserDto>>> GetLikesAsync(int postId)
        {
            var users = await _unitOfWork.Likes.Where(l => l.PostId == postId)
                .Select(l => l.User)
                .ToListAsync();

            var usersDto = _mapper.Map<List<UserDto>>(users);

            return new ResponseDto<List<UserDto>> { Data = usersDto };
        }
    }
}