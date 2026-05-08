using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using LTC.MovieService.Entities;
using LTC.MovieService.Formats;
using LTC.MovieService.Formats.Dtos.Input;
using LTC.MovieService.Formats.Dtos.Output;
using Volo.Abp;

namespace LTC.MovieService.Formats
{
    public class FormatAppService : MovieServiceAppService, IFormatAppService
    {
        private readonly IRepository<Format, Guid> _repository;

        public FormatAppService(IRepository<Format, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<PagedResultDto<FormatOutputDto>> GetFormatListAsync(GetFormatListInputDto input)
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
            var items = entities.Select(e => new FormatOutputDto { Id = e.Id, Name = e.Name }).ToList();
            return new PagedResultDto<FormatOutputDto>(totalCount, items);
        }

        public async Task<FormatDetailOutputDto> GetFormatAsync(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            return new FormatDetailOutputDto { Id = entity.Id, Name = entity.Name };
        }

        public async Task<FormatOutputDto> CreateFormatAsync(CreateFormatInputDto input)
        {
            var normalizedName = NormalizeComparableText(input.Name);
            if (string.IsNullOrWhiteSpace(normalizedName))
            {
                throw new UserFriendlyException("Format name is required.");
            }

            await EnsureFormatNameUniqueAsync(normalizedName);

            var entity = new Format() { Name = input.Name.Trim() };
            await _repository.InsertAsync(entity);
            return new FormatOutputDto { Id = entity.Id, Name = entity.Name };
        }

        public async Task<FormatOutputDto> UpdateFormatAsync(Guid id, UpdateFormatInputDto input)
        {
            var entity = await _repository.GetAsync(id);
            var normalizedName = NormalizeComparableText(input.Name);
            if (string.IsNullOrWhiteSpace(normalizedName))
            {
                throw new UserFriendlyException("Format name is required.");
            }

            await EnsureFormatNameUniqueAsync(normalizedName, id);

            entity.Name = input.Name.Trim();
            await _repository.UpdateAsync(entity);
            return new FormatOutputDto { Id = entity.Id, Name = entity.Name };
        }

        public async Task DeleteFormatAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        private async Task EnsureFormatNameUniqueAsync(string normalizedName, Guid? excludeId = null)
        {
            var query = await _repository.GetQueryableAsync();
            var entities = await AsyncExecuter.ToListAsync(query);
            var hasDuplicate = entities.Any(item =>
                (!excludeId.HasValue || item.Id != excludeId.Value) &&
                NormalizeComparableText(item.Name) == normalizedName);

            if (hasDuplicate)
            {
                throw new UserFriendlyException("Format name already exists.");
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
