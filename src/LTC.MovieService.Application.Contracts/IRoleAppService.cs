using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;
using LTC.MovieService.Dtos.Input;
using LTC.MovieService.Dtos.Output;
namespace LTC.MovieService;
public interface IRoleAppService : IApplicationService
{
Task<PagedResultDto<RoleOutputDto>> GetAllAsync(GetRoleListInputDto input);
Task<RoleDetailOutputDto> GetAsync(Guid id);
Task<RoleOutputDto> CreateAsync(CreateRoleInputDto input);
Task<RoleOutputDto> UpdateAsync(Guid id, UpdateRoleInputDto input);
Task DeleteAsync(Guid id);
}
