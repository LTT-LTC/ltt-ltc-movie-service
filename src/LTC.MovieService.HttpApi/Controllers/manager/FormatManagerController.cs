using LTC.MovieService.Formats;
using LTC.MovieService.Formats.Dtos.Input;
using LTC.MovieService.Formats.Dtos.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LTC.MovieService.Controllers.Manager;

[Route(MovieServiceSettingNames.DefaultRoute + "/manager/format")]
[Authorize(Roles = "Manager,manager")]
public class FormatManagerController : FormatController
{
    private readonly IFormatAppService _appService;

    public FormatManagerController(IFormatAppService appService) : base(appService)
    {
        _appService = appService;
    }

    [HttpPost("format")]
    public Task<FormatOutputDto> CreateFormatAsync(CreateFormatInputDto input) => _appService.CreateFormatAsync(input);

    [HttpPut("format/{id}")]
    public Task<FormatOutputDto> UpdateFormatAsync(Guid id, UpdateFormatInputDto input) => _appService.UpdateFormatAsync(id, input);

    [HttpDelete("format/{id}")]
    public Task DeleteFormatAsync(Guid id) => _appService.DeleteFormatAsync(id);
}
