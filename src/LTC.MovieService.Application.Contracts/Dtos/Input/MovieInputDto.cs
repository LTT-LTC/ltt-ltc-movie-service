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
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? OriginalTitle { get; set; } = string.Empty;
        public int? DurationMins { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public DateTime? PremiereDate { get; set; }
        public string? Status { get; set; }
        public string? Description { get; set; }
        public string? PosterUrl { get; set; }
        public string? TrailerUrl { get; set; }
        public Guid StudioId { get; set; }
    }

    public class UpdateMovieInputDto
    {
        public Guid? StudioId { get; set; }
        public Guid? RatingId { get; set; }
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? OriginalTitle { get; set; } = string.Empty;
        public int? DurationMins { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public DateTime? PremiereDate { get; set; }
        public string? Status { get; set; }
        public string? Description { get; set; }
        public string? PosterUrl { get; set; }
        public string? TrailerUrl { get; set; }
    }
}
