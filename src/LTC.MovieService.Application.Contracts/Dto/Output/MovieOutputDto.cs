using System;

namespace LTC.MovieService.Dto.Output
{
    public class MovieOutputDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int DurationInMinutes { get; set; }
        public string PosterUrl { get; set; }
        public string TrailerUrl { get; set; }
        public string Language { get; set; }
    }
}