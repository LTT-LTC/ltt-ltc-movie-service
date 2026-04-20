using System;
using System.Collections.Generic;

namespace LTC.MovieService.Dtos.Input
{
    public class CreateMovieJsonInputDto
    {
        public Guid Id { get; set; }
        public Guid? StudioId { get; set; }
        public string? StudioName { get; set; }
        public Guid? RatingId { get; set; }
        public int? RatingNumber { get; set; }
        public List<Guid>? GenreListId { get; set; }
        public List<CreateMovieActorRoleInputDto>? ActorRoles { get; set; }
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
