using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using LTC.MovieService.Entities;
using LTC.MovieService.Studios;
using LTC.MovieService.Studios.Dtos.Input;
using LTC.MovieService.Studios.Dtos.Output;
using Volo.Abp;

namespace LTC.MovieService.Studios
{
    public class StudioAppService : MovieServiceAppService, IStudioAppService
    {
        private readonly IRepository<Studio, Guid> _repository;

        public StudioAppService(IRepository<Studio, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<PagedResultDto<StudioOutputDto>> GetStudioListAsync(GetStudioListInputDto input)
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
            var items = entities.Select(e => new StudioOutputDto { Id = e.Id, Name = e.Name }).ToList();
            return new PagedResultDto<StudioOutputDto>(totalCount, items);
        }

        public async Task<StudioDetailOutputDto> GetStudioAsync(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            return new StudioDetailOutputDto { Id = entity.Id, Name = entity.Name };
        }

        public async Task<StudioOutputDto> CreateStudioAsync(CreateStudioInputDto input)
        {
            var normalizedName = NormalizeComparableText(input.Name);
            if (string.IsNullOrWhiteSpace(normalizedName))
            {
                throw new UserFriendlyException("Studio name is required.");
            }

            await EnsureStudioNameUniqueAsync(normalizedName);

            var entity = new Studio() { Name = input.Name.Trim() };
            await _repository.InsertAsync(entity);
            return new StudioOutputDto { Id = entity.Id, Name = entity.Name };
        }

        public async Task<StudioOutputDto> UpdateStudioAsync(Guid id, UpdateStudioInputDto input)
        {
            var entity = await _repository.GetAsync(id);
            var normalizedName = NormalizeComparableText(input.Name);
            if (string.IsNullOrWhiteSpace(normalizedName))
            {
                throw new UserFriendlyException("Studio name is required.");
            }

            await EnsureStudioNameUniqueAsync(normalizedName, id);

            entity.Name = input.Name.Trim();
            await _repository.UpdateAsync(entity);
            return new StudioOutputDto { Id = entity.Id, Name = entity.Name };
        }

        public async Task DeleteStudioAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        private async Task EnsureStudioNameUniqueAsync(string normalizedName, Guid? excludeId = null)
        {
            var query = await _repository.GetQueryableAsync();
            var entities = await AsyncExecuter.ToListAsync(query);
            var hasDuplicate = entities.Any(item =>
                (!excludeId.HasValue || item.Id != excludeId.Value) &&
                NormalizeComparableText(item.Name) == normalizedName);

            if (hasDuplicate)
            {
                throw new UserFriendlyException("Studio name already exists.");
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
