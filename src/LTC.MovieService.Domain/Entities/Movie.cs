using System;
using Volo.Abp.Domain.Entities;

namespace LTC.MovieService.Entities
{
    public class Movie : Entity<Guid>
    {
        public Guid? StudioId { get; set; }
        public Guid? RatingId { get; set; }
        public string Title { get; set; }
        public string? OriginalTitle { get; set; }
        public int? DurationMins { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public DateTime? PremiereDate { get; set; }
        public string? Status { get; set; }
        public string? Description { get; set; }
        public string? PosterUrl { get; set; }
        public string? TrailerUrl { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
