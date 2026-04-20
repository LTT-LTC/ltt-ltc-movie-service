using System;

namespace LTC.MovieService.Rating.Dtos.Output
{
    public class RatingOutputDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
