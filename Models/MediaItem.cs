using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.ComponentModel.DataAnnotations;

namespace ProiectMedii_Anime___Manga_Tracking_.Models
{
    public class MediaItem
    {
        public int Id { get; set; }
        [cite_start]
        [Required(ErrorMessage = "Titlul este obligatoriu!")] 
        public string Title { get; set; }
        public string Type { get; set; } 
        public int TotalVolumes { get; set; }
        public List<Review> Reviews { get; set; }
    }

    
}
