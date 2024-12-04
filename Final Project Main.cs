using System.Diagnostics;
using static Final_Project_CS221.Content;

namespace Final_Project_CS221
{
    class Final_Project_Main
    {
        private static void Main()
        {
            int choice = 0;

            Console.WriteLine("Welcome to the Media Database!");

            string path = @"Content-CSV-File.csv";
            int lineCount = GetLineCount(path);

            List<Track> tracks = new List<Track>();
            List<Podcast> podcasts = new List<Podcast>();
            List<AudioBook> books = new List<AudioBook>();
            List<Content> content = new List<Content>();

            ReadFile(content, tracks, podcasts, books, path, lineCount);

            while (choice != 13)
            {
                Console.WriteLine("" +
                    "[1] View All Entries\n" +
                    "[2] View Tracks\n" +
                    "[3] View Podcasts\n" +
                    "[4] View Audiobooks\n" +
                    "[5] Search by Artist/Author/Creator\n" +
                    "[6] Sort All Entries by Rating (decending)\n" +
                    "[7] Sort all Entries by Year (oldest to newest)\n" +
                    "[8] Sort all Entries by Lexicographical order of Title\n" +
                    "[9] Search by Year\n" +
                    "[10] Add Entry to Database \n" +
                    "[11] Remove Entry from Database \n" +
                    "[12] View Cover \n" +
                    "[13] Exit Program");

                Int32.TryParse(Console.ReadLine(), out choice);
                switch (choice)
                {
                    case 1:
                        foreach (Content i in content)
                        {
                            Console.WriteLine(i);
                        }
                        Console.WriteLine();
                        break;
                    case 2:
                        PrintType<Track>(content);
                        break;
                    case 3:
                        PrintType<Podcast>(content);
                        break;
                    case 4:
                        PrintType<AudioBook>(content);
                        break;
                    case 5:
                        SearchName(content);
                        break;
                    case 6:
                        SortRating(content);
                        break;
                    case 7:
                        SortYear(content);
                        break;
                    case 8:
                        LexSort(content);
                        break;
                    case 9:
                        SearchYear(content);
                        break;
                    case 10:
                        AddContent(content, tracks, podcasts, books, path);
                        break;
                    case 11:
                        RemoveContent(content, path);
                        break;
                    case 12:
                        ViewCover(content);
                        break;
                    case 13:
                        break;
                    default:
                        Console.WriteLine("INVALID REQUEST");
                        break;
                }
            }
            Console.WriteLine("Thanks for using this media database!");
        }
        private static int GetLineCount(string path)
        {
            using StreamReader reader = new(path);
            int lines = 0;
            while (!reader.EndOfStream)
            {
                reader.ReadLine();
                lines++;
            }
            return lines;
        }
        private static void ReadFile(List<Content> content, List<Track> tracks, List<Podcast> podcasts, List<AudioBook> books, string path, int LineCount)
        {
            try
            {
                using StreamReader reader = new(path);
                reader.ReadLine();

                for (int i = 0; i < LineCount - 1; i++)
                {
                    string? Line = reader.ReadLine();
                    string[] cols = Line.Split(",");

                    MediaType type = Enum.Parse<MediaType>(cols[0], true);

                    string name = cols[1];
                    int year = int.TryParse(cols[2], out int parsedYear) ? parsedYear : 0;
                    double duration = double.TryParse(cols[3], out double parsedDuration) ? parsedDuration : 0;
                    string creator = cols[4];
                    string album = cols.Length > 5 ? cols[5] : "";
                    string image = cols.Length > 7 ? cols[7] : "";

                    try
                    {
                        switch (type)
                        {
                            case MediaType.Track:
                                double.TryParse(cols[6], out double trackRating);
                                Track track = new Track(name, year, duration, creator, album, trackRating, type, image);
                                tracks.Add(track);
                                content.Add(track);
                                break;
                            case MediaType.Podcast:
                                int.TryParse(cols[6], out int podcastRating);
                                Podcast podcast = new Podcast(name, year, duration, creator, podcastRating, type, image);
                                podcasts.Add(podcast);
                                content.Add(podcast);
                                break;
                            case MediaType.Audiobook:
                                bool.TryParse(cols[6], out bool bookRating);
                                AudioBook book = new AudioBook(name, year, duration, creator, bookRating, type, image);
                                books.Add(book);
                                content.Add(book);
                                break;
                            default:
                                throw new ArgumentException("Unknown media type");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error parsing line: {Line}, Exception: {ex.Message}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error reading file: {e.Message}");
            }
        }
        private static void PrintType<T>(List<Content> content) where T : Content
        {
            foreach (var item in content)
            {
                if (item is T)
                {
                    Console.WriteLine(item);
                }
            }
            Console.WriteLine();
        }
        private static void SearchName(List<Content> content)
        {
            Console.WriteLine("Enter name you would like to search:");
            string? name = Console.ReadLine();
            var nameSearch = content.Where(item =>
            {
                if (item is Track track && track.Artist.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                else if (item is Podcast podcast && podcast.Creator.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                else if (item is AudioBook book && book.Author.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                return false;
            });
            if(nameSearch.Any())
            {
                foreach(Content item in nameSearch)
                {
                    Console.WriteLine(item);
                }
            }
            else { Console.WriteLine($"Artist '{name}' not found."); }
            Console.WriteLine();
        }
        private static void SortRating(List<Content> content)
        {
            content.OrderByDescending(item =>
            {
                if (item is Track track) return track.Rating;
                if (item is Podcast podcast) return podcast.Rating;
                if (item is AudioBook book) return book.Rating ? 1 : 0;
                return 0;
            })
                .ToList()
                .ForEach(Console.WriteLine);
        }
        private static void SortYear(List<Content> content)
        {
            content.OrderBy(item => item.Year)
                .ToList()
                .ForEach(Console.WriteLine);
            Console.WriteLine();
        }
        private static void LexSort(List<Content> content)
        {
             content.OrderBy(item => item.Title)
                .ToList ()
                .ForEach(Console.WriteLine);
            Console.WriteLine();
        }
        private static void SearchYear(List<Content> content)
        {
            Console.WriteLine("What year would you like to search:");
            Int32.TryParse(Console.ReadLine(), out int year);
            var yearSearch = content.Where(item => item.Year == year);
            if (yearSearch.Any())
            {
                foreach (Content item in yearSearch)
                {
                    Console.WriteLine(item);
                }
            }
            else { Console.WriteLine($"Year '{year}' not found."); }
            Console.WriteLine();
        }
        private static void AddContent(List<Content> content, List<Track> tracks, List<Podcast> podcasts, List <AudioBook> books, string path)
        {
            Console.WriteLine("Which type do you want to add: \n" +
                "[1] Track \n" +
                "[2] Audiobook \n" +
                "[3] Podcast");
            Int32.TryParse (Console.ReadLine(), out int typeChoice);

            Console.Write("Title: ");
            string? title = Console.ReadLine();

            Console.Write("Year: ");
            Int32.TryParse(Console.ReadLine(), out int year);

            Console.Write("Duration: ");
            Double.TryParse(Console.ReadLine(), out double duration);

            Console.Write("Image (optional): ");
            string? image = string.IsNullOrWhiteSpace(Console.ReadLine()) ? null : Console.ReadLine();

            string? creator;
            string? album;
            bool bookRating;

            switch (typeChoice)
            {
                case 1:
                    Console.Write("Artist: ");
                    creator = Console.ReadLine();

                    Console.Write("Album: ");
                    album = Console.ReadLine();

                    Console.Write("Rating(0-5): ");
                    Double.TryParse(Console.ReadLine(), out double rating);
                    try
                    {
                        Track track = new Track(title, year, duration, creator, album, rating, MediaType.Track, image);
                        tracks.Add(track);
                        content.Add(track);

                        using StreamWriter writer = new StreamWriter(path, append:true);
                        writer.WriteLine(track.ToCSV());

                        Console.WriteLine("Track added successfully! \n");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Failed to add Track: {e.Message}");
                    }
                    Console.WriteLine();
                    break;
                case 2:
                    Console.Write("Author: ");
                    creator = Console.ReadLine();

                    Console.Write("Rating('Up' or 'Down'): ");
                    string? ratingString = Console.ReadLine();

                    if (ratingString.Equals("UP", StringComparison.OrdinalIgnoreCase)) { bookRating = true; }
                    else if (ratingString.Equals("Down", StringComparison.OrdinalIgnoreCase)) { bookRating = false; }
                    else
                    {
                        Console.WriteLine("Invalid Entry");
                        break;
                    }

                    try
                    {
                        AudioBook book = new AudioBook(title, year, duration, creator, bookRating, MediaType.Audiobook, image);
                        books.Add(book);
                        content.Add(book);

                        using StreamWriter writer = new StreamWriter(path, append: true);
                        writer.WriteLine(book.ToCSV());

                        Console.WriteLine("Audiobook added successfully! \n");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Failed to add Audiobook: {e.Message}");
                    }
                    Console.WriteLine();
                    break;
                case 3:
                    Console.Write("Creator: ");
                    creator = Console.ReadLine();

                    Console.Write("Rating (0-10): ");
                    Int32.TryParse(Console.ReadLine(), out int podcastRating);

                    try
                    {
                        Podcast podcast = new Podcast(title, year, duration, creator, podcastRating, MediaType.Podcast, image);
                        podcasts.Add(podcast);
                        content.Add(podcast);

                        using StreamWriter writer = new StreamWriter(path, append: true);
                        writer.WriteLine(podcast.ToCSV());

                        Console.WriteLine("Podcast added successfully! \n");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Failed to add podcast: {e.Message}");
                    }
                    Console.WriteLine();
                    break;
            }
        }
        private static void AddToFile(List<Content> content, string path, int lineCount)
        {
            using StreamWriter writer = new StreamWriter(path);
            try
            {
                writer.WriteLine("Type, Name, Year, Duration, Artist, Album, Rating");
                foreach (Content item in content)
                {
                    if (item is Track track)
                    {
                        writer.WriteLine(track.ToCSV());
                    }
                    else if (item is AudioBook book)
                    {
                        writer.WriteLine(book.ToCSV());
                    }
                    else if (item is Podcast podcast)
                    {
                        writer.WriteLine(podcast.ToCSV());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            writer.Close();
        }
        private static void RemoveContent(List<Content> content, string path)
        {
            Console.WriteLine("Enter the 'Title' the item to Remove:");
            string? itemRemove = Console.ReadLine();
            List<Content> toRemove = content.FindAll(a => a.Title.Equals(itemRemove, StringComparison.OrdinalIgnoreCase))
                            .ToList();
            if (toRemove.Any())
            {
                foreach (var item in toRemove)
                {
                    content.Remove(item);
                }
                AddToFile(content, path, content.Count + 1);
                Console.WriteLine("Item(s) removed successfully.");
            }
            else
            {
                Console.WriteLine("Item not found. \n");
            }
        }
        private static void ViewCover(List<Content> content)
        {
            Console.WriteLine("Enter the 'Title' of an entry to view its cover");
            string? titleImage = Console.ReadLine();
            string image = content.FirstOrDefault(a => a.Title.Equals(titleImage, StringComparison.OrdinalIgnoreCase))?.Image;
            string ImageLocation = $@"{AppDomain.CurrentDomain.BaseDirectory}\albumCovers\{image}";
            if (File.Exists(ImageLocation))
            {
                ProcessStartInfo ProcessInfo = new ProcessStartInfo(ImageLocation, @"%SystemRoot%\System32\rundll32.exe % ProgramFiles %\Windows Photo Viewer\PhotoViewer.dll, ImageView_Fullscreen %1")
                {
                    UseShellExecute = true,
                    WorkingDirectory = Path.GetDirectoryName(ImageLocation),
                    FileName = ImageLocation,
                    Verb = "OPEN"
                };
                Process.Start(ProcessInfo);
                Console.WriteLine("Displaying cover... \n");
            }
            else
            {
                Console.WriteLine("Item not found. \n");
            }
        }
    }
}