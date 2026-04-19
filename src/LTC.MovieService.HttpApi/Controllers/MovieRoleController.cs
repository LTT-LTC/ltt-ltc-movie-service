using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using LTC.MovieService;
using LTC.MovieService.Dtos.Input;
using LTC.MovieService.Dtos.Output;

namespace LTC.MovieService.Controllers
{
    [Route(MovieServiceSettingNames.DefaultRoute)]
    [Authorize(Roles = "Admin,Manager")]
    public class MovieRoleController : AbpControllerBase 
    {
        private readonly IRoleAppService _appService;

        public MovieRoleController(IRoleAppService appService) 
        { 
            _appService = appService; 
        }

        [HttpGet("role-all")]
        public async Task<PagedResultDto<RoleOutputDto>> GetAllAsync([FromQuery] GetRoleListInputDto input) 
        { 
            /// <summary>
            /// Get all movie roles (Admin/Manager only).
            /// </summary>
            return await _appService.GetAllAsync(input); 
        }

        [HttpGet("role/{id}")]
        public async Task<RoleDetailOutputDto> GetAsync(Guid id) 
        { 
            /// <summary>
            /// Get movie role details (Admin/Manager only).
            /// </summary>
            return await _appService.GetAsync(id); 
        }

        [HttpPost("role")]
        public async Task<RoleOutputDto> CreateAsync(CreateRoleInputDto input) 
        { 
            /// <summary>
            /// Create a new movie role (Admin/Manager only).
            /// </summary>
            return await _appService.CreateAsync(input); 
        }

        [HttpPut("role/{id}")]
        public async Task<RoleOutputDto> UpdateAsync(Guid id, UpdateRoleInputDto input) 
        { 
            /// <summary>
            /// Update a movie role (Admin/Manager only).
            /// </summary>
            return await _appService.UpdateAsync(id, input); 
        }

        [HttpDelete("role/{id}")]
        public async Task DeleteAsync(Guid id) 
        { 
            /// <summary>
            /// Delete a movie role (Admin/Manager only).
            /// </summary>
            await _appService.DeleteAsync(id); 
        }
    }
}
