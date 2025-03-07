// SocialMediaApp.Data/AppDbContext.cs
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Core.Entities;
using System;

namespace SocialMediaApp.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<UserFollow> UserFollows { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // UserFollow ilişkileri
            builder.Entity<UserFollow>()
                .HasOne(uf => uf.Follower)
                .WithMany(u => u.Following)
                .HasForeignKey(uf => uf.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserFollow>()
                .HasOne(uf => uf.Following)
                .WithMany(u => u.Followers)
                .HasForeignKey(uf => uf.FollowingId)
                .OnDelete(DeleteBehavior.Restrict);

            // Message ilişkileri
            builder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

// SocialMediaApp.Data/Repositories/IGenericRepository.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SocialMediaApp.Data.Repositories
    {
        public interface IGenericRepository<T> where T : class
        {
            Task<T> GetByIdAsync(int id);
            Task<IEnumerable<T>> GetAllAsync();
            IQueryable<T> Where(Expression<Func<T, bool>> predicate);
            Task AddAsync(T entity);
            void Update(T entity);
            void Remove(T entity);
            Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        }
    }

// SocialMediaApp.Data/Repositories/GenericRepository.cs
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SocialMediaApp.Data.Repositories
    {
        public class GenericRepository<T> : IGenericRepository<T> where T : class
        {
            private readonly DbContext _context;
            private readonly DbSet<T> _dbSet;

            public GenericRepository(AppDbContext context)
            {
                _context = context;
                _dbSet = context.Set<T>();
            }

            public async Task<T> GetByIdAsync(int id)
            {
                return await _dbSet.FindAsync(id);
            }

            public async Task<IEnumerable<T>> GetAllAsync()
            {
                return await _dbSet.ToListAsync();
            }

            public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
            {
                return _dbSet.Where(predicate);
            }

            public async Task AddAsync(T entity)
            {
                await _dbSet.AddAsync(entity);
            }

            public void Update(T entity)
            {
                _dbSet.Update(entity);
            }

            public void Remove(T entity)
            {
                _dbSet.Remove(entity);
            }

            public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
            {
                return await _dbSet.AnyAsync(predicate);
            }
        }
    }

// SocialMediaApp.Data/UnitOfWork/IUnitOfWork.cs
using SocialMediaApp.Core.Entities;
using SocialMediaApp.Data.Repositories;
using System;
using System.Threading.Tasks;

namespace SocialMediaApp.Data.UnitOfWork
    {
        public interface IUnitOfWork : IDisposable
        {
            IGenericRepository<Post> Posts { get; }
            IGenericRepository<Comment> Comments { get; }
            IGenericRepository<Like> Likes { get; }
            IGenericRepository<UserFollow> UserFollows { get; }
            IGenericRepository<Message> Messages { get; }

            Task<int> CommitAsync();
        }
    }

// SocialMediaApp.Data/UnitOfWork/UnitOfWork.cs
using SocialMediaApp.Core.Entities;
using SocialMediaApp.Data.Repositories;
using System;
using System.Threading.Tasks;

namespace SocialMediaApp.Data.UnitOfWork
    {
        public class UnitOfWork : IUnitOfWork
        {
            private readonly AppDbContext _context;

            private IGenericRepository<Post> _posts;
            private IGenericRepository<Comment> _comments;
            private IGenericRepository<Like> _likes;
            private IGenericRepository<UserFollow> _userFollows;
            private IGenericRepository<Message> _messages;

            public UnitOfWork(AppDbContext context)
            {
                _context = context;
            }

            public IGenericRepository<Post> Posts => _posts ??= new GenericRepository<Post>(_context);
            public IGenericRepository<Comment> Comments => _comments ??= new GenericRepository<Comment>(_context);
            public IGenericRepository<Like> Likes => _likes ??= new GenericRepository<Like>(_context);
            public IGenericRepository<UserFollow> UserFollows => _userFollows ??= new GenericRepository<UserFollow>(_context);
            public IGenericRepository<Message> Messages => _messages ??= new GenericRepository<Message>(_context);

            public async Task<int> CommitAsync()
            {
                return await _context.SaveChangesAsync();
            }

            public void Dispose()
            {
                _context.Dispose();
            }
        }
    }