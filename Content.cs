namespace Final_Project_CS221
{
    public abstract class Content
    {
        protected string? title;
        protected int year;
        protected double duration;
        protected MediaType type;
        protected string image;

        public Content(string? title, int year, double duration, MediaType type, string image)
        {
            Title = title;
            this.year = year;
            Duration = duration;
            Type = type;
            Image = image;
        }

        public string? Title
        {
            get => title;
            private set
            {
                if (string.IsNullOrWhiteSpace(value)) { throw new ArgumentNullException(nameof(title)); }
                title = value;
            }
        }

        public int Year
        {
            get
            {
                if (year < 0 || year > DateTime.Now.Year + 1) { throw new ArgumentOutOfRangeException(nameof(year)); }
                return year;
            }
        }

        public double Duration
        {
            get => duration;
            private set
            {
                if (value < 0 || value > 999) { throw new ArgumentOutOfRangeException(nameof(duration)); }
                duration = value;
            }
        }
        public enum MediaType : byte
        {
            Track,
            Podcast,
            Audiobook
        }

        public MediaType Type
        {
            get;
            set;
        }

        public string Image
        {
            get => image;
            private set
            {
                image = value;
            }
        }
    }
}