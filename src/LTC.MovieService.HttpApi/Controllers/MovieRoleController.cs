using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using LTC.MovieService.Roles;
using LTC.MovieService.Roles.Dtos.Input;
using LTC.MovieService.Roles.Dtos.Output;

namespace LTC.MovieService.Controllers
{
    public abstract class MovieRoleController : AbpControllerBase 
    {
        private readonly IRoleAppService _appService;

        public MovieRoleController(IRoleAppService appService) 
        { 
            _appService = appService; 
        }

        [HttpGet("role-all")]
        public async Task<PagedResultDto<RoleOutputDto>> GetRoleListAsync([FromQuery] GetRoleListInputDto input) 
        { 
            /// <summary>
            /// Get all movie roles (Admin/Manager only).
            /// </summary>
            return await _appService.GetRoleListAsync(input); 
        }

        [HttpGet("role/{id}")]
        public async Task<RoleDetailOutputDto> GetRoleAsync(Guid id) 
        { 
            /// <summary>
            /// Get movie role details (Admin/Manager only).
            /// </summary>
            return await _appService.GetRoleAsync(id); 
        }

    }
}
