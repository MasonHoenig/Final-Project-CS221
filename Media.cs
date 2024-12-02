namespace Final_Project_CS221
{
    public class Media
    {
        private Track[] track;
        private AudioBook[] book;
        private Podcast[] podcast;

        public Media(Track[] Song, AudioBook[] Book, Podcast[] Podcast)
        {
            track = Song;
            book = Book;
            podcast = Podcast;
        }

        public Track[] Song { get; set; }
        public AudioBook[] Book { get; set; }
        public Podcast[] Podcast { get; set; }
    }
}