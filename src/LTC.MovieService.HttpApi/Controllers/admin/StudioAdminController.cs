using LTC.MovieService.Studios;
using LTC.MovieService.Studios.Dtos.Input;
using LTC.MovieService.Studios.Dtos.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LTC.MovieService.Controllers.Admin;

[Route(MovieServiceSettingNames.DefaultRoute + "/admin/studio")]
[Authorize(Roles = "Admin,admin")]
public class StudioAdminController : StudioController
{
    private readonly IStudioAppService _appService;

    public StudioAdminController(IStudioAppService appService) : base(appService)
    {
        _appService = appService;
    }

    [HttpPost("studio")]
    public Task<StudioOutputDto> CreateStudioAsync(CreateStudioInputDto input) => _appService.CreateStudioAsync(input);

    [HttpPut("studio/{id}")]
    public Task<StudioOutputDto> UpdateStudioAsync(Guid id, UpdateStudioInputDto input) => _appService.UpdateStudioAsync(id, input);

    [HttpDelete("studio/{id}")]
    public Task DeleteStudioAsync(Guid id) => _appService.DeleteStudioAsync(id);
}
