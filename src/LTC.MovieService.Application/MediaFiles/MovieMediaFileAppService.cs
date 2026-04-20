using System;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using LTC.MovieService.MediaFiles;
using LTC.MovieService.MediaFiles.Dtos.Input;
using LTC.MovieService.MediaFiles.Dtos.Output;
using Volo.Abp;

namespace LTC.MovieService.MediaFiles
{
    public class MovieMediaFileAppService : MovieServiceAppService, IMovieMediaFileAppService
    {
        private const string PosterFolder = "ltt-ltc/movie/poster";
        private readonly Cloudinary _cloudinary;

        public MovieMediaFileAppService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task<UploadMoviePosterOutputDto> UploadPosterAsync(UploadMoviePosterInputDto input)
        {
            if (CurrentUser == null || (!CurrentUser.IsInRole("admin") && !CurrentUser.IsInRole("manager")))
            {
                throw new UserFriendlyException("Only admins and managers can perform this action.");
            }

            if (input?.ImageFile == null || input.ImageFile.Length == 0)
            {
                throw new UserFriendlyException("Image file is required.");
            }

            await using var stream = input.ImageFile.OpenReadStream();
            var fileDescription = new FileDescription(input.ImageFile.FileName, stream);
            var uploadParams = new ImageUploadParams
            {
                File = fileDescription,
                Folder = PosterFolder,
                PublicId = $"movie_poster_{Guid.CreateVersion7()}",
                Overwrite = true,
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            if (uploadResult == null || uploadResult.Error != null || uploadResult.SecureUrl == null)
            {
                var errorMessage = uploadResult?.Error?.Message ?? "Upload poster failed.";
                throw new UserFriendlyException(errorMessage);
            }

            return new UploadMoviePosterOutputDto
            {
                SecureUrl = uploadResult.SecureUrl.ToString(),
                PublicId = uploadResult.PublicId,
                DisplayName = uploadResult.OriginalFilename ?? input.ImageFile.FileName,
                Format = uploadResult.Format,
                Size = uploadResult.Bytes,
            };
        }
    }
}
