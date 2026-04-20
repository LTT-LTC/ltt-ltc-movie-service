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
        Task<PagedResultDto<RatingOutputDto>> GetAllAsync(GetRatingListInputDto input);
        Task<RatingOutputDto> GetAsync(Guid id);
        Task<RatingOutputDto> CreateAsync(CreateRatingInputDto input);
        Task<RatingOutputDto> UpdateAsync(Guid id, UpdateRatingInputDto input);
        Task DeleteAsync(Guid id);
    }
}
