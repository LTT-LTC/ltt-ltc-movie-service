using LTC.MovieService.Roles;
using LTC.MovieService.Roles.Dtos.Input;
using LTC.MovieService.Roles.Dtos.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LTC.MovieService.Controllers.Admin;

[Route(MovieServiceSettingNames.DefaultRoute + "/admin/movie-role")]
[Authorize(Roles = "Admin,admin")]
public class MovieRoleAdminController : MovieRoleController
{
    private readonly IRoleAppService _appService;

    public MovieRoleAdminController(IRoleAppService appService) : base(appService)
    {
        _appService = appService;
    }

    [HttpPost("role")]
    public Task<RoleOutputDto> CreateRoleAsync(CreateRoleInputDto input) => _appService.CreateRoleAsync(input);

    [HttpPut("role/{id}")]
    public Task<RoleOutputDto> UpdateRoleAsync(Guid id, UpdateRoleInputDto input) => _appService.UpdateRoleAsync(id, input);

    [HttpDelete("role/{id}")]
    public Task DeleteRoleAsync(Guid id) => _appService.DeleteRoleAsync(id);
}
