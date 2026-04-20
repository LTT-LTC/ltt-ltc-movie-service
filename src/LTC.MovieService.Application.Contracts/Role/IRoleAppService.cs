using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;
using LTC.MovieService.Role.Dtos.Input;
using LTC.MovieService.Role.Dtos.Output;

namespace LTC.MovieService.Role
{
    public interface IRoleAppService : IApplicationService
    {
        Task<PagedResultDto<RoleOutputDto>> GetAllAsync(GetRoleListInputDto input);
        Task<RoleDetailOutputDto> GetAsync(Guid id);
        Task<RoleOutputDto> CreateAsync(CreateRoleInputDto input);
        Task<RoleOutputDto> UpdateAsync(Guid id, UpdateRoleInputDto input);
        Task DeleteAsync(Guid id);
    }
}
