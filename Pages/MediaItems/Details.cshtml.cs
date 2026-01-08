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
    public class DetailsModel : PageModel
    {
        private readonly ProiectMedii_Anime___Manga_Tracking_.Data.ProiectMedii_Anime___Manga_Tracking_Context _context;

        public DetailsModel(ProiectMedii_Anime___Manga_Tracking_.Data.ProiectMedii_Anime___Manga_Tracking_Context context)
        {
            _context = context;
        }

        public MediaItem MediaItem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaitem = await _context.MediaItem.FirstOrDefaultAsync(m => m.Id == id);
            if (mediaitem == null)
            {
                return NotFound();
            }
            else
            {
                MediaItem = mediaitem;
            }
            return Page();
        }
    }
}
