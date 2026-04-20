using System;

namespace LTC.MovieService.Dtos.Input
{
    public class GetMovieListInputDto : PaginationInputDto
    {
        public Guid? GenreId { get; set; }
        public Guid? StudioId { get; set; }
        public Guid? FormatId { get; set; }
    }
}