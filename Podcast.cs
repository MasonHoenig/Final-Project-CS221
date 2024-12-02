namespace Final_Project_CS221
{
    public class Podcast : Content
    {
        private string? creator;
        private int rating;

        public Podcast() { }

        public Podcast(string? title, int year, double duration, string? creator, int rating, MediaType type, string image) : base(title, year, duration, type, image)
        {
            this.creator = creator;
            Rating = rating;
        }

        public override string ToString()
        {
            return $"{Title}({Duration}) by {Creator}({Year}), Rating: {Rating}";
        }
        public string ToCSV()
        {
            return $"{Type},{Title},{Year},{Duration},{Creator},{null},{Rating},{Image}";
        }

        public string Creator
        {
            get
            {
                if (string.IsNullOrWhiteSpace(creator)) { throw new ArgumentOutOfRangeException(nameof(creator)); }
                return creator;
            }
        }
        public int Rating
        {
            get => rating;
            set
            {
                if (value < 0 || value > 10) { throw new ArgumentOutOfRangeException(nameof(rating)); }
                rating = value;
            }
        }
    }
}