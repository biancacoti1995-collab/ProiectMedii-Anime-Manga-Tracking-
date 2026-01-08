using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProiectMedii_Anime___Manga_Tracking_.Models;

namespace ProiectMedii_Anime___Manga_Tracking_.Data
{
    public class ProiectMedii_Anime___Manga_Tracking_Context : DbContext
    {
        public ProiectMedii_Anime___Manga_Tracking_Context (DbContextOptions<ProiectMedii_Anime___Manga_Tracking_Context> options)
            : base(options)
        {
        }

        public DbSet<ProiectMedii_Anime___Manga_Tracking_.Models.MediaItem> MediaItem { get; set; } = default!;
        public DbSet<ProiectMedii_Anime___Manga_Tracking_.Models.Review> Review { get; set; } = default!;
    }
}
