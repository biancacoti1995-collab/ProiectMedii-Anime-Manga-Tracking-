using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProiectMedii_Anime___Manga_Tracking_.Data;
using ProiectMedii_Anime___Manga_Tracking_.Models;

namespace ProiectMedii_Anime___Manga_Tracking_.Pages.MediaItems
{
    public class CreateModel : PageModel
    {
        private readonly ProiectMedii_Anime___Manga_Tracking_.Data.ProiectMedii_Anime___Manga_Tracking_Context _context;

        public CreateModel(ProiectMedii_Anime___Manga_Tracking_.Data.ProiectMedii_Anime___Manga_Tracking_Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public MediaItem MediaItem { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.MediaItem.Add(MediaItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
