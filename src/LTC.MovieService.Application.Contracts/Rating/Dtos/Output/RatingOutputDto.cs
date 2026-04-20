using LTC.MovieService.Dtos.Input;
using System;

namespace LTC.MovieService.Ratings.Dtos.Output
{
    public class GetRatingListInputDto : PaginationInputDto
    {
    }
    public class RatingOutputDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
