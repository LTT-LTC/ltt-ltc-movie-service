using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using LTC.MovieService.Entities;
using LTC.MovieService.Actors;
using LTC.MovieService.Actors.Dtos.Input;
using LTC.MovieService.Actors.Dtos.Output;
using Volo.Abp;

namespace LTC.MovieService.Actors
{
    public class ActorAppService : MovieServiceAppService, IActorAppService
    {
        private readonly IRepository<Actor, Guid> _repository;

        public ActorAppService(IRepository<Actor, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<PagedResultDto<ActorOutputDto>> GetActorListAsync(GetActorListInputDto input)
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

        public async Task<ActorDetailOutputDto> GetActorAsync(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            return new ActorDetailOutputDto { Id = entity.Id, Name = entity.Name };
        }

        public async Task<ActorOutputDto> CreateActorAsync(CreateActorInputDto input)
        {
            var normalizedName = NormalizeComparableText(input.Name);
            if (string.IsNullOrWhiteSpace(normalizedName))
            {
                throw new UserFriendlyException("Actor name is required.");
            }

            await EnsureActorNameUniqueAsync(normalizedName);

            var entity = new Actor() { Name = input.Name.Trim() };
            await _repository.InsertAsync(entity);
            return new ActorOutputDto { Id = entity.Id, Name = entity.Name };
        }

        public async Task<ActorOutputDto> UpdateActorAsync(Guid id, UpdateActorInputDto input)
        {
            var entity = await _repository.GetAsync(id);
            var normalizedName = NormalizeComparableText(input.Name);
            if (string.IsNullOrWhiteSpace(normalizedName))
            {
                throw new UserFriendlyException("Actor name is required.");
            }

            await EnsureActorNameUniqueAsync(normalizedName, id);

            entity.Name = input.Name.Trim();
            await _repository.UpdateAsync(entity);
            return new ActorOutputDto { Id = entity.Id, Name = entity.Name };
        }

        public async Task DeleteActorAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        private async Task EnsureActorNameUniqueAsync(string normalizedName, Guid? excludeId = null)
        {
            var query = await _repository.GetQueryableAsync();
            var entities = await AsyncExecuter.ToListAsync(query);
            var hasDuplicate = entities.Any(item =>
                (!excludeId.HasValue || item.Id != excludeId.Value) &&
                NormalizeComparableText(item.Name) == normalizedName);

            if (hasDuplicate)
            {
                throw new UserFriendlyException("Actor name already exists.");
            }
        }

        private static string NormalizeComparableText(string? value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? string.Empty
                : value.Trim().Replace(" ", string.Empty).Replace("-", string.Empty).ToLowerInvariant();
        }
    }
}
