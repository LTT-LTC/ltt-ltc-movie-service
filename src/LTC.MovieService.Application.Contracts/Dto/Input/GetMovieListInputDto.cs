using System;
using LTC.MovieService.Dto.Common;

namespace LTC.MovieService.Dto.Input
{
    public class GetMovieListInputDto : PaginationInputDto
    {
        public Guid? GenreId { get; set; }
        public Guid? StudioId { get; set; }
    }
}