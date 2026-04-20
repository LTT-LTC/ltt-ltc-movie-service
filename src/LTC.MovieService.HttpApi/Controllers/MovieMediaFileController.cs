using System.Threading.Tasks;
using LTC.MovieService.MediaFiles;
using LTC.MovieService.MediaFiles.Dtos.Input;
using LTC.MovieService.MediaFiles.Dtos.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace LTC.MovieService.Controllers
{
    [Area(MovieServiceRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = MovieServiceRemoteServiceConsts.RemoteServiceName)]
    [Route(MovieServiceSettingNames.DefaultRoute + "/media-files")]
    [Authorize(Roles = "Admin,Manager")]
    public class MovieMediaFileController : MovieServiceController
    {
        private readonly IMovieMediaFileAppService _movieMediaFileAppService;

        public MovieMediaFileController(IMovieMediaFileAppService movieMediaFileAppService)
        {
            _movieMediaFileAppService = movieMediaFileAppService;
        }

        [HttpPost("poster")]
        [Consumes("multipart/form-data")]
        public virtual Task<UploadMoviePosterOutputDto> UploadPosterAsync([FromForm] UploadMoviePosterInputDto input)
        {
            return _movieMediaFileAppService.UploadPosterAsync(input);
        }
    }
}
