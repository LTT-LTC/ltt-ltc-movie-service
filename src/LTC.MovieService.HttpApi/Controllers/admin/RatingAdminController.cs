using LTC.MovieService.Ratings;
using LTC.MovieService.Ratings.Dtos.Input;
using LTC.MovieService.Ratings.Dtos.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LTC.MovieService.Controllers.Admin;

[Route(MovieServiceSettingNames.DefaultRoute + "/admin/rating")]
[Authorize(Roles = "Admin,admin")]
public class RatingAdminController : RatingController
{
    private readonly IRatingAppService _appService;

    public RatingAdminController(IRatingAppService appService) : base(appService)
    {
        _appService = appService;
    }

    [HttpPost("rating")]
    public Task<RatingOutputDto> CreateRatingAsync(CreateRatingInputDto input) => _appService.CreateRatingAsync(input);

    [HttpPut("rating/{id}")]
    public Task<RatingOutputDto> UpdateRatingAsync(Guid id, UpdateRatingInputDto input) => _appService.UpdateRatingAsync(id, input);

    [HttpDelete("rating/{id}")]
    public Task DeleteRatingAsync(Guid id) => _appService.DeleteRatingAsync(id);
}
