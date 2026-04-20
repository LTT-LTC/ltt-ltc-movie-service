using LTC.MovieService.Actor.Dtos.Output;
using LTC.MovieService.Genre.Dtos.Output;
using LTC.MovieService.Role.Dtos.Output;
using LTC.MovieService.Studio.Dtos.Output;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace LTC.MovieService.Dtos.Output
{
    public class MovieOutputDto : EntityDto<Guid>
    {
        public Guid MovieId { get; set; }

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

        public StudioOutputDto? Studio { get; set; }
        public List<GenreOutputDto> Genres { get; set; } = new();
        public List<MovieCastOutputDto> Cast { get; set; } = new();
    }

    public class MovieDetailOutputDto : MovieOutputDto
    {
    }

    public class MovieCastOutputDto
    {
        public ActorOutputDto Actor { get; set; }
        public RoleOutputDto? Role { get; set; }
        public string? CharacterName { get; set; }
    }
}