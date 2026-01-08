using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProiectMedii_Anime___Manga_Tracking_.Data;
using ProiectMedii_Anime___Manga_Tracking_.Models;

namespace ProiectMedii_Anime___Manga_Tracking_.Pages.MediaItems
{
    public class EditModel : PageModel
    {
        private readonly ProiectMedii_Anime___Manga_Tracking_.Data.ProiectMedii_Anime___Manga_Tracking_Context _context;

        public EditModel(ProiectMedii_Anime___Manga_Tracking_.Data.ProiectMedii_Anime___Manga_Tracking_Context context)
        {
            _context = context;
        }

        [BindProperty]
        public MediaItem MediaItem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaitem =  await _context.MediaItem.FirstOrDefaultAsync(m => m.Id == id);
            if (mediaitem == null)
            {
                return NotFound();
            }
            MediaItem = mediaitem;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(MediaItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MediaItemExists(MediaItem.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MediaItemExists(int id)
        {
            return _context.MediaItem.Any(e => e.Id == id);
        }
    }
}
