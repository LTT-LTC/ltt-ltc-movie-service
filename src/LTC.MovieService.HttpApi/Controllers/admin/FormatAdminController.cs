using LTC.MovieService.Formats;
using LTC.MovieService.Formats.Dtos.Input;
using LTC.MovieService.Formats.Dtos.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LTC.MovieService.Controllers.Admin;

[Route(MovieServiceSettingNames.DefaultRoute + "/admin/format")]
[Authorize(Roles = "Admin,admin")]
public class FormatAdminController : FormatController
{
    private readonly IFormatAppService _appService;

    public FormatAdminController(IFormatAppService appService) : base(appService)
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
