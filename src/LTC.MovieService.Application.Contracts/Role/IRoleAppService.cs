using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;
using LTC.MovieService.Roles.Dtos.Input;
using LTC.MovieService.Roles.Dtos.Output;

namespace LTC.MovieService.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task<PagedResultDto<RoleOutputDto>> GetRoleListAsync(GetRoleListInputDto input);
        Task<RoleDetailOutputDto> GetRoleAsync(Guid id);
        Task<RoleOutputDto> CreateRoleAsync(CreateRoleInputDto input);
        Task<RoleOutputDto> UpdateRoleAsync(Guid id, UpdateRoleInputDto input);
        Task DeleteRoleAsync(Guid id);
    }
}
