using Microsoft.Build.Framework;

namespace ProiectMedii_Anime___Manga_Tracking_.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required] public string Name { get; set; }
        public List<MediaItem> MediaItems { get; set; }
    }
}
