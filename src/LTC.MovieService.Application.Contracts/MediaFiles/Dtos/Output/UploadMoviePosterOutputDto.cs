namespace LTC.MovieService.MediaFiles.Dtos.Output
{
    public class UploadMoviePosterOutputDto
    {
        public string SecureUrl { get; set; } = string.Empty;
        public string PublicId { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string? Format { get; set; }
        public long Size { get; set; }
    }
}
