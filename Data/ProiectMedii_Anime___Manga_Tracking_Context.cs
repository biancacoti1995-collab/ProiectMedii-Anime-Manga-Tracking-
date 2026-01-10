using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProiectMedii_Anime___Manga_Tracking_.Models;

namespace ProiectMedii_Anime___Manga_Tracking_.Data
{
    
    public class ProiectMedii_Anime___Manga_Tracking_Context : IdentityDbContext
    {
        public ProiectMedii_Anime___Manga_Tracking_Context(DbContextOptions<ProiectMedii_Anime___Manga_Tracking_Context> options)
            : base(options)
        {
        }

    
        public DbSet<MediaItem> MediaItem { get; set; } = default!;
        public DbSet<Category> Category { get; set; } = default!;
        public DbSet<Review> Review { get; set; } = default!;

        public DbSet<UserTracker> UserTracker { get; set; } = default!;
    }
}