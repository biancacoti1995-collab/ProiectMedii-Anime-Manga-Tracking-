using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProiectMedii_Anime___Manga_Tracking_.Data;
using ProiectMedii_Anime___Manga_Tracking_.Models;

namespace ProiectMedii_Anime___Manga_Tracking_.Controllers
{
    public class UserTrackersController : Controller
    {
        private readonly ProiectMedii_Anime___Manga_Tracking_Context _context;

        public UserTrackersController(ProiectMedii_Anime___Manga_Tracking_Context context)
        {
            _context = context;
        }

        // GET: UserTrackers
        public async Task<IActionResult> Index()
        {
            var proiectMedii_Anime___Manga_Tracking_Context = _context.UserTracker.Include(u => u.MediaItem);
            return View(await proiectMedii_Anime___Manga_Tracking_Context.ToListAsync());
        }

        // GET: UserTrackers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTracker = await _context.UserTracker
                .Include(u => u.MediaItem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userTracker == null)
            {
                return NotFound();
            }

            return View(userTracker);
        }

        // GET: UserTrackers/Create
        public IActionResult Create()
        {
            ViewData["MediaItemId"] = new SelectList(_context.MediaItem, "Id", "Title");
            return View();
        }

        // POST: UserTrackers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MediaItemId,UserId,Status,CurrentVolume,Rating")] UserTracker userTracker)
        {
            // Pasul 1: Ștergem manual erorile care blochează salvarea
            ModelState.Remove("User");
            ModelState.Remove("MediaItem");
            ModelState.Remove("UserId"); // Uneori IdentityUser cere validare și pe ID ca string

            
            if (userTracker.MediaItemId != 0)
            { 
                
                var emailErrors = ModelState["User.Id"];
                if (emailErrors != null) emailErrors.Errors.Clear();

                var userErrors = ModelState["User"];
                if (userErrors != null) userErrors.Errors.Clear();
            }

            if (ModelState.IsValid)
            {
                _context.Add(userTracker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Dacă tot ajunge aici (eșuează), reconstruim listele
            ViewData["MediaItemId"] = new SelectList(_context.MediaItem, "Id", "Title", userTracker.MediaItemId);
            return View(userTracker);
        }
        // GET: UserTrackers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTracker = await _context.UserTracker.FindAsync(id);
            if (userTracker == null)
            {
                return NotFound();
            }
            ViewData["MediaItemId"] = new SelectList(_context.MediaItem, "Id", "Title", userTracker.MediaItemId);
            return View(userTracker);
        }

        // POST: UserTrackers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MediaItemId,UserId,CurrentProgress,Status")] UserTracker userTracker)
        {
            if (id != userTracker.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userTracker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserTrackerExists(userTracker.Id))
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
            ViewData["MediaItemId"] = new SelectList(_context.MediaItem, "Id", "Title", userTracker.MediaItemId);
            return View(userTracker);
        }

        // GET: UserTrackers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTracker = await _context.UserTracker
                .Include(u => u.MediaItem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userTracker == null)
            {
                return NotFound();
            }

            return View(userTracker);
        }

        // POST: UserTrackers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userTracker = await _context.UserTracker.FindAsync(id);
            if (userTracker != null)
            {
                _context.UserTracker.Remove(userTracker);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserTrackerExists(int id)
        {
            return _context.UserTracker.Any(e => e.Id == id);
        }
    }
}
