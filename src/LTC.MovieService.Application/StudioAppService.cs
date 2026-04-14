using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using LTC.MovieService.Dtos.Input;
using LTC.MovieService.Dtos.Output;
using LTC.MovieService.Entities;

namespace LTC.MovieService
{
    public class StudioAppService : MovieServiceAppService, IStudioAppService
    {
        private readonly IRepository<Studio, Guid> _repository;

        public StudioAppService(IRepository<Studio, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<PagedResultDto<StudioOutputDto>> GetAllAsync(GetStudioListInputDto input)
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

        public async Task<StudioDetailOutputDto> GetAsync(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            return new StudioDetailOutputDto { Id = entity.Id, Name = entity.Name };
        }

        public async Task<StudioOutputDto> CreateAsync(CreateStudioInputDto input)
        {
            // Dummy creation for boilerplate. Update with proper mapping.
            var entity = new Studio() { Name = input.Name };
            await _repository.InsertAsync(entity);
            return new StudioOutputDto { Id = entity.Id, Name = entity.Name };
        }

        public async Task<StudioOutputDto> UpdateAsync(Guid id, UpdateStudioInputDto input)
        {
            var entity = await _repository.GetAsync(id);
            entity.Name = input.Name;
            await _repository.UpdateAsync(entity);
            return new StudioOutputDto { Id = entity.Id, Name = entity.Name };
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
