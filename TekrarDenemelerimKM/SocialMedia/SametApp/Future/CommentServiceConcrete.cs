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
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<ResponseDto<List<CommentDto>>> GetPostCommentsAsync(int postId)
        {
            var comments = await _unitOfWork.Comments.Where(c => c.PostId == postId && c.IsActive)
                .Include(c => c.User)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
                
            var commentsDto = _mapper.Map<List<CommentDto>>(comments);
            
            return new ResponseDto<List<CommentDto>> { Data = commentsDto };
        }
        
        public async Task<ResponseDto<CommentDto>> CreateAsync(string userId, int postId, CommentDto commentDto)
        {
            var post = await _unitOfWork.Posts.Where(p => p.Id == postId && p.IsActive)
                .FirstOrDefaultAsync();
                
            if (post == null)
            {
                return new ResponseDto<CommentDto> { Success = false, Message = "Post not found" };
            }
            
            var comment = new Comment
            {
                Content = commentDto.Content,
                UserId = userId,
                PostId = postId,
                CreatedAt = DateTime.Now
            };
            
            await _unitOfWork.Comments.AddAsync(comment);
            await _unitOfWork.CommitAsync();
            
            // Yeni oluşturulan yorumun kullanıcı bilgilerini almak için
            comment = await _unitOfWork.Comments.Where(c => c.Id == comment.Id)
                .Include(c => c.User)
                .FirstOrDefaultAsync();
                
            var createdCommentDto = _mapper.Map<CommentDto>(comment);
            
            return new ResponseDto<CommentDto> { Data = createdCommentDto };
        }
        
        public async Task<ResponseDto<CommentDto>> UpdateAsync(string userId, int commentId, CommentDto commentDto)
        {
            var comment = await _unitOfWork.Comments.Where(c => c.Id == commentId && c.UserId == userId && c.IsActive)
                .Include(c => c.User)
                .FirstOrDefaultAsync();
                
            if (comment == null)
            {
                return new ResponseDto<CommentDto> { Success = false, Message = "Comment not found or you are not authorized" };
            }
            
            comment.Content = commentDto.Content;
            comment.UpdatedAt = DateTime.Now;
            
            _unitOfWork.Comments.Update(comment);
            await _unitOfWork.CommitAsync();
            
            var updatedCommentDto = _mapper.Map<CommentDto>(comment);
            
            return new ResponseDto<CommentDto> { Data = updatedCommentDto };
        }
        
        public async Task<ResponseDto<NoDataDto>> DeleteAsync(string userId, int commentId)
        {
            var comment = await _unitOfWork.Comments.Where(c => c.Id == commentId && c.UserId == userId)
                .FirstOrDefaultAsync();
                
            if (comment == null)
            {
                return new ResponseDto<NoDataDto> { Success = false, Message = "Comment not found or you are not authorized" };
            }
            
            comment.IsActive = false;
            
            _unitOfWork.Comments.Update(comment);
            await _unitOfWork.CommitAsync();
            
            return new ResponseDto<NoDataDto> { Message = "Comment deleted successfully" };
        }
    }
}