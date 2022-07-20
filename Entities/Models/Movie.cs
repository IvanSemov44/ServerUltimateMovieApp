namespace Entities.Models
{
    public class Movie
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Creator { get; set; } = string.Empty;

        public string Actors { get; set; } = string.Empty;

        public string Descriptions { get; set; } = string.Empty;

        public DateTime? CreatedOn { get; set; }

        public DateTime? ChangeOn { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public string TrailerUrl { get; set; } = string.Empty;

        public string SubtitleUrl { get; set; } = string.Empty;

        public string DownloadUrl { get; set; } = string.Empty;

        public int? Year { get; set; }

        public string Country { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public string Comments { get; set; } = string.Empty;
    }
}
