using System;

namespace LTC.MovieService.Dtos.Input
{
    public class GetMovieListInputDto : PaginationInputDto
    {
        public Guid? GenreId { get; set; }
        public Guid? StudioId { get; set; }
        public Guid? FormatId { get; set; }
    }

    public class CreateMovieInputDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public int DurationMinutes { get; set; }
        public Guid StudioId { get; set; }
    }

    public class UpdateMovieInputDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public int DurationMinutes { get; set; }
        public Guid StudioId { get; set; }
    }
}
