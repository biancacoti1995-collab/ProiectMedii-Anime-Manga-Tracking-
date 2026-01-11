using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ProiectMedii_Anime___Manga_Tracking_.Models
{
    public class UserTracker
    {
        public int Id { get; set; }

        public int MediaItemId { get; set; }
        public virtual MediaItem? MediaItem { get; set; } // semnul ? oprește eroarea la validare

        public string? UserId { get; set; }
        public virtual IdentityUser? User { get; set; }

        [Display(Name = "Progres curent")]
        public int CurrentProgress { get; set; }

        [Required]
        public string Status { get; set; } = "In Progress"; // Valoare default ca să nu fie null
    }
}