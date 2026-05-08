using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LTC.MovieService.Entities;
using LTC.MovieService.Roles;
using LTC.MovieService.Roles.Dtos.Input;
using LTC.MovieService.Roles.Dtos.Output;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp;

namespace LTC.MovieService.Roles
{
    public class RoleAppService : MovieServiceAppService, IRoleAppService
    {
        private readonly IRepository<MovieRole, Guid> _repository;

        public RoleAppService(IRepository<MovieRole, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<PagedResultDto<RoleOutputDto>> GetRoleListAsync(GetRoleListInputDto input)
        {
            var query = await _repository.GetQueryableAsync();
            
            var maxCount = input.Fetch > 0 ? input.Fetch : 10;
            var skipCount = (input.Page > 1 ? input.Page - 1 : 0) * maxCount;
            
            var totalCount = await AsyncExecuter.CountAsync(query);
            
            var entities = await AsyncExecuter.ToListAsync(
                query.OrderBy(x => x.Name).Skip(skipCount).Take(maxCount)
            );

            var items = entities.Select(e => new RoleOutputDto 
            { 
                Id = e.Id, 
                Name = e.Name
            }).ToList();

            return new PagedResultDto<RoleOutputDto>(totalCount, items);
        }

        public async Task<RoleDetailOutputDto> GetRoleAsync(Guid id)
        {
            var e = await _repository.GetAsync(id);
            return new RoleDetailOutputDto { Id = e.Id, Name = e.Name };
        }

        public async Task<RoleOutputDto> CreateRoleAsync(CreateRoleInputDto input)
        {
            var normalizedName = NormalizeComparableText(input.Name);
            if (string.IsNullOrWhiteSpace(normalizedName))
            {
                throw new UserFriendlyException("Role name is required.");
            }

            await EnsureRoleNameUniqueAsync(normalizedName);

            var e = new MovieRole { Name = input.Name.Trim() };
            await _repository.InsertAsync(e);
            return new RoleOutputDto { Id = e.Id, Name = e.Name };
        }

        public async Task<RoleOutputDto> UpdateRoleAsync(Guid id, UpdateRoleInputDto input)
        {
            var e = await _repository.GetAsync(id);
            var normalizedName = NormalizeComparableText(input.Name);
            if (string.IsNullOrWhiteSpace(normalizedName))
            {
                throw new UserFriendlyException("Role name is required.");
            }

            await EnsureRoleNameUniqueAsync(normalizedName, id);

            e.Name = input.Name.Trim();
            await _repository.UpdateAsync(e);
            return new RoleOutputDto { Id = e.Id, Name = e.Name };
        }

        public async Task DeleteRoleAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        private async Task EnsureRoleNameUniqueAsync(string normalizedName, Guid? excludeId = null)
        {
            var query = await _repository.GetQueryableAsync();
            var entities = await AsyncExecuter.ToListAsync(query);
            var hasDuplicate = entities.Any(item =>
                (!excludeId.HasValue || item.Id != excludeId.Value) &&
                NormalizeComparableText(item.Name) == normalizedName);

            if (hasDuplicate)
            {
                throw new UserFriendlyException("Role name already exists.");
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
