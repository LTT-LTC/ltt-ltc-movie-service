namespace LTC.MovieService.Dtos.Input
{
    public class CreateRatingInputDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }

    public class UpdateRatingInputDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }

    public class GetRatingListInputDto : PaginationInputDto
    {
    }
}
