using System.ComponentModel.DataAnnotations;

namespace ProiectMedii_Anime___Manga_Tracking_.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int MediaItemId { get; set; }
        [cite_start]
        [Range(1, 10, ErrorMessage = "Rating-ul trebuie să fie între 1 și 10")]  
        public int Rating { get; set; }
        public string Comment { get; set; }
        public string UserEmail { get; set; } 
    }

    internal class cite_startAttribute : Attribute
    {
    }
}
