namespace Final_Project_CS221
{
    public class AudioBook : Content
    {
        private string? author;
        private bool rating;

        public AudioBook() { }

        public AudioBook(string? title, int year, double duration, string? author, bool rating, MediaType type, string image) : base(title, year, duration, type, image)
        {
            this.author = author;
            Rating = rating;
        }

        public override string ToString()
        {
            string ratingDisplay = @"\/";
            if (Rating) { ratingDisplay = @"/\"; }
            return $"{Title}({Duration}) by {Author}({Year}), Rating: {ratingDisplay}";
        }
        public string ToCSV()
        {
            return $"{Type},{Title},{Year},{Duration},{Author},{null},{Rating},{Image}";
        }

        public string Author
        {
            get
            {
                if (string.IsNullOrWhiteSpace(author)) { throw new ArgumentOutOfRangeException(nameof(author)); }
                return author;
            }
        }
        public bool Rating { get; set; }
    }
}