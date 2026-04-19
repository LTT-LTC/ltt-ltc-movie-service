using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using LTC.MovieService;
using LTC.MovieService.Dtos.Input;
using LTC.MovieService.Dtos.Output;

namespace LTC.MovieService.Controllers;

[Route(MovieServiceSettingNames.DefaultRoute)]
public class RatingController : AbpControllerBase
{
    private readonly IRatingAppService _appService;
    public RatingController(IRatingAppService appService) 
    { 
        _appService = appService; 
    }

    [HttpGet("rating-all")]
    public async Task<PagedResultDto<RatingOutputDto>> GetAllAsync([FromQuery] GetRatingListInputDto input) 
    { 
        /// <summary>
        /// Get all ratings.
        /// </summary>
        return await _appService.GetAllAsync(input); 
    }

    [HttpGet("rating/{id}")]
    public async Task<RatingOutputDto> GetAsync(Guid id) 
    { 
        /// <summary>
        /// Get rating details by id.
        /// </summary>
        return await _appService.GetAsync(id); 
    }

    [HttpPost("rating")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<RatingOutputDto> CreateAsync(CreateRatingInputDto input) 
    { 
        /// <summary>
        /// Create a new rating (Admin/Manager only).
        /// </summary>
        return await _appService.CreateAsync(input); 
    }

    [HttpPut("rating/{id}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<RatingOutputDto> UpdateAsync(Guid id, UpdateRatingInputDto input) 
    { 
        /// <summary>
        /// Update a rating (Admin/Manager only).
        /// </summary>
        return await _appService.UpdateAsync(id, input); 
    }

    [HttpDelete("rating/{id}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task DeleteAsync(Guid id) 
    { 
        /// <summary>
        /// Delete a rating (Admin/Manager only).
        /// </summary>
        await _appService.DeleteAsync(id); 
    }
}
