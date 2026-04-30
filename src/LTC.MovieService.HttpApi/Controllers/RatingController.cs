using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using LTC.MovieService.Ratings;
using LTC.MovieService.Ratings.Dtos.Input;
using LTC.MovieService.Ratings.Dtos.Output;

namespace LTC.MovieService.Controllers;

public abstract class RatingController : AbpControllerBase
{
    private readonly IRatingAppService _appService;
    public RatingController(IRatingAppService appService) 
    { 
        _appService = appService; 
    }

    [HttpGet("rating-all")]
    public async Task<PagedResultDto<RatingOutputDto>> GetRatingListAsync([FromQuery] GetRatingListInputDto input) 
    { 
        /// <summary>
        /// Get all ratings.
        /// </summary>
        return await _appService.GetRatingListAsync(input); 
    }

    [HttpGet("rating/{id}")]
    public async Task<RatingOutputDto> GetRatingAsync(Guid id) 
    { 
        /// <summary>
        /// Get rating details by id.
        /// </summary>
        return await _appService.GetRatingAsync(id); 
    }

}
