using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;
using LTC.MovieService.Actors.Dtos.Input;
using LTC.MovieService.Actors.Dtos.Output;

namespace LTC.MovieService.Actors;
public interface IActorAppService : IApplicationService
{
Task<PagedResultDto<ActorOutputDto>> GetActorListAsync(GetActorListInputDto input);
Task<ActorDetailOutputDto> GetActorAsync(Guid id);
Task<ActorOutputDto> CreateActorAsync(CreateActorInputDto input);
Task<ActorOutputDto> UpdateActorAsync(Guid id, UpdateActorInputDto input);
Task DeleteActorAsync(Guid id);
}
