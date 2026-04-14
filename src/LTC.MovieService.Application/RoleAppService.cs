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
    public class RoleAppService : MovieServiceAppService, IRoleAppService
    {
        private readonly IRepository<MovieRole, Guid> _repository;

        public RoleAppService(IRepository<MovieRole, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<PagedResultDto<RoleOutputDto>> GetAllAsync(GetRoleListInputDto input)
        {
            var query = await _repository.GetQueryableAsync();
            var maxCount = input.Fetch > 0 ? input.Fetch : 10;
            var skipCount = (input.Page > 1 ? input.Page - 1 : 0) * maxCount;
            
            var totalCount = await AsyncExecuter.CountAsync(query);
            var entities = await AsyncExecuter.ToListAsync(
                query.OrderBy(x => x.Id).Skip(skipCount).Take(maxCount)
            );

            var items = entities.Select(e => new RoleOutputDto { Id = e.Id, Name = e.Name }).ToList();
            return new PagedResultDto<RoleOutputDto>(totalCount, items);
        }

        public async Task<RoleDetailOutputDto> GetAsync(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            return new RoleDetailOutputDto { Id = entity.Id, Name = entity.Name };
        }

        public async Task<RoleOutputDto> CreateAsync(CreateRoleInputDto input)
        {
            var entity = new MovieRole() { Name = input.Name };
            await _repository.InsertAsync(entity);
            return new RoleOutputDto { Id = entity.Id, Name = entity.Name };
        }

        public async Task<RoleOutputDto> UpdateAsync(Guid id, UpdateRoleInputDto input)
        {
            var entity = await _repository.GetAsync(id);
            entity.Name = input.Name;
            await _repository.UpdateAsync(entity);
            return new RoleOutputDto { Id = entity.Id, Name = entity.Name };
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
