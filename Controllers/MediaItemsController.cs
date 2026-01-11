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
            ModelState.Remove("Category");
            ModelState.Remove("Reviews");

           
            if (string.IsNullOrEmpty(mediaItem.Type))
            {
                mediaItem.Type = "Anime";
            }

            if (ModelState.IsValid)
            {
                _context.Add(mediaItem);
                await _context.SaveChangesAsync(); 
                return RedirectToAction(nameof(Index)); 
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
        // --- UPDATE PROGRESS ---
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [Route("MediaItems/UpdateProgress")] 
        public async Task<IActionResult> UpdateProgress(int mediaId, int progress)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Challenge();

            var tracker = await _context.UserTracker
                .FirstOrDefaultAsync(t => t.MediaItemId == mediaId && t.UserId == userId);

            if (tracker == null)
            {
                tracker = new UserTracker { MediaItemId = mediaId, UserId = userId, Status = "In Progress" };
                _context.UserTracker.Add(tracker);
            }

            tracker.CurrentProgress = progress;
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = mediaId });
        }

        // --- ADD REVIEW ---
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [Route("MediaItems/AddReview")] 
        public async Task<IActionResult> AddReview(int mediaId, int rating, string comment)
        {
            if (mediaId == 0) return NotFound();

            var review = new Review
            {
                MediaItemId = mediaId,
                StarRating = rating,
                Comment = comment,
                UserEmail = User.Identity?.Name ?? "Anonymous"
            };

            _context.Review.Add(review);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = mediaId });
        }

    }
}


