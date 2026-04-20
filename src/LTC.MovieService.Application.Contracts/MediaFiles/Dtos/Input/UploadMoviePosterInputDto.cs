using Microsoft.AspNetCore.Http;

namespace LTC.MovieService.MediaFiles.Dtos.Input
{
    public class UploadMoviePosterInputDto
    {
        public IFormFile ImageFile { get; set; } = default!;
    }
}
