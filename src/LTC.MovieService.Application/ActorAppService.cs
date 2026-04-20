using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using LTC.MovieService.Entities;
using LTC.MovieService.Actor;
using LTC.MovieService.Actor.Dtos.Input;
using LTC.MovieService.Actor.Dtos.Output;

namespace LTC.MovieService
{
    public class ActorAppService : MovieServiceAppService, IActorAppService
    {
        private readonly IRepository<Actor, Guid> _repository;

        public ActorAppService(IRepository<Actor, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<PagedResultDto<ActorOutputDto>> GetAllAsync(GetActorListInputDto input)
        {
            // TODO: Ensure mapping works or mapping configured in AutoMapper profile
            var query = await _repository.GetQueryableAsync();
            
            // Base Pagination
            var maxCount = input.Fetch > 0 ? input.Fetch : 10;
            var skipCount = (input.Page > 1 ? input.Page - 1 : 0) * maxCount;
            
            var totalCount = await AsyncExecuter.CountAsync(query);
            var entities = await AsyncExecuter.ToListAsync(
                query.OrderBy(x => x.Id).Skip(skipCount).Take(maxCount)
            );

            // Dummy manual map to pass compile. Consider AutoMapper.
            var items = entities.Select(e => new ActorOutputDto { Id = e.Id, Name = e.Name }).ToList();
            return new PagedResultDto<ActorOutputDto>(totalCount, items);
        }

        public async Task<ActorDetailOutputDto> GetAsync(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            return new ActorDetailOutputDto { Id = entity.Id, Name = entity.Name };
        }

        public async Task<ActorOutputDto> CreateAsync(CreateActorInputDto input)
        {
            // Dummy creation for boilerplate. Update with proper mapping.
            var entity = new Actor() { Name = input.Name };
            await _repository.InsertAsync(entity);
            return new ActorOutputDto { Id = entity.Id, Name = entity.Name };
        }

        public async Task<ActorOutputDto> UpdateAsync(Guid id, UpdateActorInputDto input)
        {
            var entity = await _repository.GetAsync(id);
            entity.Name = input.Name;
            await _repository.UpdateAsync(entity);
            return new ActorOutputDto { Id = entity.Id, Name = entity.Name };
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
