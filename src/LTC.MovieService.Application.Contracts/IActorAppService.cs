using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;
using LTC.MovieService.Dtos.Input;
using LTC.MovieService.Dtos.Output;
namespace LTC.MovieService;
public interface IActorAppService : IApplicationService
{
Task<PagedResultDto<ActorOutputDto>> GetAllAsync(GetActorListInputDto input);
Task<ActorDetailOutputDto> GetAsync(Guid id);
Task<ActorOutputDto> CreateAsync(CreateActorInputDto input);
Task<ActorOutputDto> UpdateAsync(Guid id, UpdateActorInputDto input);
Task DeleteAsync(Guid id);
}
