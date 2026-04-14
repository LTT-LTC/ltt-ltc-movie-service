namespace LTC.MovieService.Dtos.Input
{
    public class PaginationInputDto
    {
        public int Fetch { get; set; } = 10;
        public int Page { get; set; } = 1;
        public string? OrderBy { get; set; }
        public bool IsSortDesc { get; set; }
        public string? Keyword { get; set; }
    }
}
