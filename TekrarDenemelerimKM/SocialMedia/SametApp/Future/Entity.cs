//Entity
// SocialMediaApp.Core/Entities/User.cs
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace SocialMediaApp.Core.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ProfilePicture { get; set; }
        public string Bio { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<UserFollow> Followers { get; set; }
        public ICollection<UserFollow> Following { get; set; }
        public ICollection<Message> SentMessages { get; set; }
        public ICollection<Message> ReceivedMessages { get; set; }
    }

    // SocialMediaApp.Core/Entities/Post.cs
    namespace SocialMediaApp.Core.Entities
    {
        public class Post
        {
            public int Id { get; set; }
            public string Content { get; set; }
            public string ImagePath { get; set; }
            public DateTime CreatedAt { get; set; } = DateTime.Now;
            public DateTime? UpdatedAt { get; set; }
            public bool IsActive { get; set; } = true;

            // Foreign keys
            public string UserId { get; set; }

            // Navigation properties
            public User User { get; set; }
            public ICollection<Comment> Comments { get; set; }
            public ICollection<Like> Likes { get; set; }
        }

        // SocialMediaApp.Core/Entities/Comment.cs
        namespace SocialMediaApp.Core.Entities
        {
            public class Comment
            {
                public int Id { get; set; }
                public string Content { get; set; }
                public DateTime CreatedAt { get; set; } = DateTime.Now;
                public DateTime? UpdatedAt { get; set; }
                public bool IsActive { get; set; } = true;

                // Foreign keys
                public int PostId { get; set; }
                public string UserId { get; set; }

                // Navigation properties
                public Post Post { get; set; }
                public User User { get; set; }
            }

            // SocialMediaApp.Core/Entities/Like.cs
            namespace SocialMediaApp.Core.Entities
            {
                public class Like
                {
                    public int Id { get; set; }
                    public DateTime CreatedAt { get; set; } = DateTime.Now;

                    // Foreign keys
                    public int PostId { get; set; }
                    public string UserId { get; set; }

                    // Navigation properties
                    public Post Post { get; set; }
                    public User User { get; set; }
                }

                // SocialMediaApp.Core/Entities/UserFollow.cs
                namespace SocialMediaApp.Core.Entities
                {
                    public class UserFollow
                    {
                        public int Id { get; set; }
                        public DateTime CreatedAt { get; set; } = DateTime.Now;

                        // Foreign keys
                        public string FollowerId { get; set; }
                        public string FollowingId { get; set; }

                        // Navigation properties
                        public User Follower { get; set; }
                        public User Following { get; set; }
                    }

                    // SocialMediaApp.Core/Entities/Message.cs
                    namespace SocialMediaApp.Core.Entities
                    {
                        public class Message
                        {
                            public int Id { get; set; }
                            public string Content { get; set; }
                            public bool IsRead { get; set; } = false;
                            public DateTime CreatedAt { get; set; } = DateTime.Now;

                            // Foreign keys
                            public string SenderId { get; set; }
                            public string ReceiverId { get; set; }

                            // Navigation properties
                            public User Sender { get; set; }
                            public User Receiver { get; set; }
                        }