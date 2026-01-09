namespace ProiectMedii_Anime___Manga_Tracking_.Models
{
    public class UserTracker
    {
        public int Id { get; set; }
        public int MediaItemId { get; set; }
        public MediaItem MediaItem { get; set; }
        public string UserEmail { get; set; }
        public int CurrentProgress { get; set; } // episodul sau capitolul la care a ajuns utilizatorul
        public string Status { get; set; }//daca l-a terminat, l-a inceput, l-a abandonat 
    }
}
