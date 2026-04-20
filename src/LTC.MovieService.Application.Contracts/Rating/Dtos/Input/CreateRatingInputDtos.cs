
namespace LTC.MovieService.Ratings.Dtos.Input
{
    public class CreateRatingInputDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
