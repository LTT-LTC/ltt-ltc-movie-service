using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;
using LTC.MovieService.Ratings.Dtos.Input;
using LTC.MovieService.Ratings.Dtos.Output;

namespace LTC.MovieService.Ratings
{
    public interface IRatingAppService : IApplicationService
    {
        Task<PagedResultDto<RatingOutputDto>> GetRatingListAsync(GetRatingListInputDto input);
        Task<RatingOutputDto> GetRatingAsync(Guid id);
        Task<RatingOutputDto> CreateRatingAsync(CreateRatingInputDto input);
        Task<RatingOutputDto> UpdateRatingAsync(Guid id, UpdateRatingInputDto input);
        Task DeleteRatingAsync(Guid id);
    }
}
