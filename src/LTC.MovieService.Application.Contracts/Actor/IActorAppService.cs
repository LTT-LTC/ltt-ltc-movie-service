using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;
using LTC.MovieService.Actors.Dtos.Input;
using LTC.MovieService.Actors.Dtos.Output;

namespace LTC.MovieService.Actors;
public interface IActorAppService : IApplicationService
{
Task<PagedResultDto<ActorOutputDto>> GetAllAsync(GetActorListInputDto input);
Task<ActorDetailOutputDto> GetAsync(Guid id);
Task<ActorOutputDto> CreateAsync(CreateActorInputDto input);
Task<ActorOutputDto> UpdateAsync(Guid id, UpdateActorInputDto input);
Task DeleteAsync(Guid id);
}
