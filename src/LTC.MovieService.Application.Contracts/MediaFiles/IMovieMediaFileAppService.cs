using System.Threading.Tasks;
using LTC.MovieService.MediaFiles.Dtos.Input;
using LTC.MovieService.MediaFiles.Dtos.Output;
using Volo.Abp.Application.Services;

namespace LTC.MovieService.MediaFiles
{
    public interface IMovieMediaFileAppService : IApplicationService
    {
        Task<UploadMoviePosterOutputDto> UploadPosterAsync(UploadMoviePosterInputDto input);
    }
}
