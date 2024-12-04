namespace Final_Project_CS221
{
    public class Track : Content
    {
        private string? artist;
        private string? album;
        private double rating;

        public Track(string title, int year, double duration, string artist, string album, double rating, MediaType type, string image) : base(title, year, duration, type, image)
        {
            this.artist = artist;
            Album = album;
            Rating = rating;
        }

        public override string ToString()
        {
            return $"{Title} ({Duration} minutes) by {Artist} ({Year}) Album: {Album}, Rating: {Rating}";
        }
        public string ToCSV()
        {
            return $"{Type},{Title},{Year},{Duration},{Artist},{Album},{Rating},{Image}";
        }

        public string Artist 
        {
            get
            {
                if (string.IsNullOrWhiteSpace(artist)) { throw new ArgumentOutOfRangeException(nameof(artist)); }
                return artist;
            }
        }
        public string? Album { get; set; }
        public double Rating
        {
            get => rating;
            private set
            {
                if (value < 0 || value > 5) { throw new ArgumentOutOfRangeException(nameof(rating)); }
                rating = value;
            }
        }
    }
}