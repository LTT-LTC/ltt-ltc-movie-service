using LTC.MovieService.Actors.Dtos.Output;
using LTC.MovieService.Genres.Dtos.Output;
using LTC.MovieService.Roles.Dtos.Output;
using LTC.MovieService.Ratings.Dtos.Output;
using LTC.MovieService.Studios.Dtos.Output;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace LTC.MovieService.Dtos.Output
{
    public class MovieOutputDto : EntityDto<Guid>
    {
        public Guid MovieId { get; set; }

        public Guid? StudioId { get; set; }
        public string? StudioName { get; set; }
        public Guid? RatingId { get; set; }
        public string? RatingCode { get; set; }
        public string? RatingName { get; set; }
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
        public List<string> GenreNames { get; set; } = new();
        public List<GenreOutputDto> Genres { get; set; } = new();
        public List<MovieCastNameOutputDto> ActorRoles { get; set; } = new();
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
        public string? ActorName { get; set; }
        public string? RoleName { get; set; }
    }

    public class MovieCastNameOutputDto
    {
        public string ActorName { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
    }
}