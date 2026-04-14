using System;
using Volo.Abp.Application.Dtos;

namespace LTC.MovieService.Dtos.Output
{
    public class MovieOutputDto : EntityDto<Guid>
    {
        
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public int DurationMinutes { get; set; }
        public Guid StudioId { get; set; }
        public string StudioName { get; set; } = string.Empty;
    }

    public class MovieDetailOutputDto : MovieOutputDto
    {
        // Add detailed fields like associated genres, actors, roles
    }
}
