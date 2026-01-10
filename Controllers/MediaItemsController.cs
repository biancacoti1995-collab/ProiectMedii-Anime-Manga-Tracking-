using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProiectMedii_Anime___Manga_Tracking_.Data;
using ProiectMedii_Anime___Manga_Tracking_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectMedii_Anime___Manga_Tracking_.Controllers
{
    public class MediaItemsController : Controller
    {
        private readonly ProiectMedii_Anime___Manga_Tracking_Context _context;
        private readonly UserManager<IdentityUser> _userManager;
        public MediaItemsController(ProiectMedii_Anime___Manga_Tracking_Context context)
        {
            _context = context;
        }

        // GET: MediaItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.MediaItem.ToListAsync());
        }

        // GET: MediaItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaItem = await _context.MediaItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mediaItem == null)
            {
                return NotFound();
            }

            return View(mediaItem);
        }

        // GET: MediaItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MediaItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Type,TotalVolumes")] MediaItem mediaItem)
        {
            // Pasul A: Spunem programului să ignore faptul că nu am ales o Categorie sau Review-uri
            ModelState.Remove("Category");
            ModelState.Remove("Reviews");

            // Pasul B: Dacă 'Type' e gol, îi dăm noi o valoare ca să nu dea eroare baza de date
            if (string.IsNullOrEmpty(mediaItem.Type))
            {
                mediaItem.Type = "Anime";
            }

            if (ModelState.IsValid)
            {
                _context.Add(mediaItem);
                await _context.SaveChangesAsync(); // Aici se salvează în SQL
                return RedirectToAction(nameof(Index)); // Te trimite la lista unde vei vedea noul rând
            }

            return View(mediaItem);
        }
        // GET: MediaItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaItem = await _context.MediaItem.FindAsync(id);
            if (mediaItem == null)
            {
                return NotFound();
            }
            return View(mediaItem);
        }

        // POST: MediaItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Type,TotalVolumes")] MediaItem mediaItem)
        {
            if (id != mediaItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mediaItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MediaItemExists(mediaItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(mediaItem);
        }

        // GET: MediaItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaItem = await _context.MediaItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mediaItem == null)
            {
                return NotFound();
            }

            return View(mediaItem);
        }

        // POST: MediaItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mediaItem = await _context.MediaItem.FindAsync(id);
            if (mediaItem != null)
            {
                _context.MediaItem.Remove(mediaItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MediaItemExists(int id)
        {
            return _context.MediaItem.Any(e => e.Id == id);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddReview(int mediaId, int rating, string comment)
        {
            var review = new Review
            {
                MediaItemId = mediaId,
                StarRating = rating,
                Comment = comment,
                UserEmail = User.Identity.Name // salvăm cine a scris review-ul
            };

            _context.Review.Add(review);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = mediaId });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateProgress(int mediaId, int progress)
        {
            // 1. Obținem ID-ul utilizatorului logat (nu email-ul)
            var userId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(userId))
            {
                return Challenge(); // Te trimite la Login dacă nu ești logat
            }

            // 2. Căutăm tracker-ul folosind UserId
            var tracker = await _context.UserTracker
                .FirstOrDefaultAsync(t => t.MediaItemId == mediaId && t.UserId == userId);

            if (tracker == null)
            {
                // 3. Dacă nu există, creăm unul nou cu UserId
                tracker = new UserTracker
                {
                    MediaItemId = mediaId,
                    UserId = userId,
                    Status = "In Progress" // Punem un status default obligatoriu
                };
                _context.UserTracker.Add(tracker);
            }

            // 4. Actualizăm progresul
            tracker.CurrentProgress = progress;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = mediaId });
        }
    }
}


