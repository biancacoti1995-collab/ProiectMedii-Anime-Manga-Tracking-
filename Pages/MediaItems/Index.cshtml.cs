using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProiectMedii_Anime___Manga_Tracking_.Data;
using ProiectMedii_Anime___Manga_Tracking_.Models;

namespace ProiectMedii_Anime___Manga_Tracking_.Pages.MediaItems
{
    public class IndexModel : PageModel
    {
        private readonly ProiectMedii_Anime___Manga_Tracking_.Data.ProiectMedii_Anime___Manga_Tracking_Context _context;

        public IndexModel(ProiectMedii_Anime___Manga_Tracking_.Data.ProiectMedii_Anime___Manga_Tracking_Context context)
        {
            _context = context;
        }

        public IList<MediaItem> MediaItem { get;set; } = default!;

        public async Task OnGetAsync()
        {
            MediaItem = await _context.MediaItem.ToListAsync();
        }
    }
}
