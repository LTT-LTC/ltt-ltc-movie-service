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

namespace LTC.MovieService.Formats
{
    public class FormatAppService : MovieServiceAppService, IFormatAppService
    {
        private readonly IRepository<Format, Guid> _repository;

        public FormatAppService(IRepository<Format, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<PagedResultDto<FormatOutputDto>> GetAllAsync(GetFormatListInputDto input)
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

        public async Task<FormatDetailOutputDto> GetAsync(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            return new FormatDetailOutputDto { Id = entity.Id, Name = entity.Name };
        }

        public async Task<FormatOutputDto> CreateAsync(CreateFormatInputDto input)
        {
            // Dummy creation for boilerplate. Update with proper mapping.
            var entity = new Format() { Name = input.Name };
            await _repository.InsertAsync(entity);
            return new FormatOutputDto { Id = entity.Id, Name = entity.Name };
        }

        public async Task<FormatOutputDto> UpdateAsync(Guid id, UpdateFormatInputDto input)
        {
            var entity = await _repository.GetAsync(id);
            entity.Name = input.Name;
            await _repository.UpdateAsync(entity);
            return new FormatOutputDto { Id = entity.Id, Name = entity.Name };
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
