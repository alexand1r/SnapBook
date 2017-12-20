namespace Snapbook.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class SnapbookDbContext : IdentityDbContext<User>
    {
        public SnapbookDbContext(DbContextOptions<SnapbookDbContext> options)
            : base(options)
        {
        }

        public DbSet<Ad> Ads { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<UsersLikedImages> UsersLikedImages { get; set; }
        public DbSet<UsersSavedImages> UsersSavedImages { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // User - Liked Photos
            builder.Entity<UsersLikedImages>()
                .HasKey(uli => new { uli.UserId, uli.PhotoId });

            builder.Entity<UsersLikedImages>()
                .HasOne(uli => uli.Photo)
                .WithMany(p => p.Likers)
                .HasForeignKey(uli => uli.PhotoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UsersLikedImages>()
                .HasOne(uli => uli.User)
                .WithMany(u => u.LikedPhotos)
                .HasForeignKey(uli => uli.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // User - Saved Photos
            builder.Entity<UsersSavedImages>()
                .HasKey(usi => new { usi.UserId, usi.PhotoId });

            builder.Entity<UsersSavedImages>()
                .HasOne(uli => uli.Photo)
                .WithMany(p => p.Savers)
                .HasForeignKey(uli => uli.PhotoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UsersSavedImages>()
                .HasOne(uli => uli.User)
                .WithMany(u => u.SavedPhotos)
                .HasForeignKey(uli => uli.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // User - Notifications
            builder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId);

            // User - Comments
            builder.Entity<Comment>()
                .HasOne(c => c.Author)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.AuthorId);

            // User - Albums
            builder.Entity<Album>()
                .HasOne(n => n.User)
                .WithMany(u => u.Albums)
                .HasForeignKey(n => n.UserId);

            // User - Ad
            builder.Entity<User>()
                .HasOne(u => u.Ad)
                .WithOne(a => a.User)
                .HasForeignKey<Ad>(a => a.UserId);

            // Album - Photos
            builder.Entity<Photo>()
                .HasOne(p => p.Album)
                .WithMany(u => u.Photos)
                .HasForeignKey(p => p.AlbumId);

            // Category - Albums
            builder.Entity<Album>()
                .HasOne(a => a.Category)
                .WithMany(c => c.Albums)
                .HasForeignKey(a => a.CategoryId);

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

            // Photo - Tags
            builder.Entity<Tag>()
                .HasOne(c => c.Photo)
                .WithMany(p => p.Tags)
                .HasForeignKey(c => c.PhotoId);

            base.OnModelCreating(builder);
        }
    }
}
