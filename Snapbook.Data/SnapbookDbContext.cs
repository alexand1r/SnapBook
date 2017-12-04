namespace Snapbook.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Snapbook.Data.Models;

    public class SnapbookDbContext : IdentityDbContext<User>
    {
        public SnapbookDbContext(DbContextOptions<SnapbookDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Followers - Following
            builder.Entity<UsersFollowers>()
                .HasKey(k => new { k.UserId, k.FollowerId });

            builder.Entity<UsersFollowers>()
                .HasOne(l => l.User)
                .WithMany(a => a.Followers)
                .HasForeignKey(l => l.UserId);

            builder.Entity<UsersFollowers>()
                .HasOne(l => l.Follower)
                .WithMany(a => a.Following)
                .HasForeignKey(l => l.FollowerId);

            // User - Liked Photos
            builder.Entity<UsersLikedImages>()
                .HasKey(uli => new { uli.UserId, uli.PhotoId });

            builder.Entity<UsersLikedImages>()
                .HasOne(uli => uli.Photo)
                .WithMany(p => p.Likers)
                .HasForeignKey(uli => uli.PhotoId);

            builder.Entity<UsersLikedImages>()
                .HasOne(uli => uli.User)
                .WithMany(u => u.LikedPhotos)
                .HasForeignKey(uli => uli.UserId);

            // User - Saved Photos
            builder.Entity<UsersSavedImages>()
                .HasKey(usi => new { usi.UserId, usi.PhotoId });

            builder.Entity<UsersSavedImages>()
                .HasOne(uli => uli.Photo)
                .WithMany(p => p.Savers)
                .HasForeignKey(uli => uli.PhotoId);

            builder.Entity<UsersSavedImages>()
                .HasOne(uli => uli.User)
                .WithMany(u => u.SavedPhotos)
                .HasForeignKey(uli => uli.UserId);

            // User - Profile Photos
            builder.Entity<Photo>()
                .HasOne(p => p.Profile)
                .WithMany(u => u.Photos)
                .HasForeignKey(p => p.ProfileId);

            // Ad - Photos
            builder.Entity<Photo>()
                .HasOne(p => p.Ad)
                .WithMany(a => a.Photos)
                .HasForeignKey(p => p.AdId);

            // Photo - Comments
            builder.Entity<Comment>()
                .HasOne(c => c.Photo)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PhotoId);

            // User - Notifications
            builder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId);

            base.OnModelCreating(builder);
        }
    }
}
