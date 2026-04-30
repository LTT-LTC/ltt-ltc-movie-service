using System.Threading.Tasks;
using LTC.MovieService.MediaFiles;
using LTC.MovieService.MediaFiles.Dtos.Input;
using LTC.MovieService.MediaFiles.Dtos.Output;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace LTC.MovieService.Controllers
{
    [Area(MovieServiceRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = MovieServiceRemoteServiceConsts.RemoteServiceName)]
    public abstract class MovieMediaFileController : MovieServiceController
    {
        private readonly IMovieMediaFileAppService _movieMediaFileAppService;

        public MovieMediaFileController(IMovieMediaFileAppService movieMediaFileAppService)
        {
            _movieMediaFileAppService = movieMediaFileAppService;
        }

        protected Task<UploadMoviePosterOutputDto> UploadPosterInternalAsync(UploadMoviePosterInputDto input)
        {
            return _movieMediaFileAppService.UploadPosterAsync(input);
        }
    }
}
