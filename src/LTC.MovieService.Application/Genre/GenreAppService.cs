using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using LTC.MovieService.Entities;
using LTC.MovieService.Genres;
using LTC.MovieService.Genres.Dtos.Input;
using LTC.MovieService.Genres.Dtos.Output;
using GenreEntity = LTC.MovieService.Entities.Genre;

namespace LTC.MovieService.Genres
{
    public class GenreAppService : MovieServiceAppService, IGenreAppService
    {
        private readonly IRepository<Genre, Guid> _repository;

        public GenreAppService(IRepository<Genre, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<PagedResultDto<GenreOutputDto>> GetGenreListAsync(GetGenreListInputDto input)
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
            var items = entities.Select(e => new GenreOutputDto { Id = e.Id, Name = e.Name }).ToList();
            return new PagedResultDto<GenreOutputDto>(totalCount, items);
        }

        public async Task<GenreDetailOutputDto> GetGenreAsync(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            return new GenreDetailOutputDto { Id = entity.Id, Name = entity.Name };
        }

        public async Task<GenreOutputDto> CreateGenreAsync(CreateGenreInputDto input)
        {
            // Dummy creation for boilerplate. Update with proper mapping.
            var entity = new Genre() { Name = input.Name };
            await _repository.InsertAsync(entity);
            return new GenreOutputDto { Id = entity.Id, Name = entity.Name };
        }

        public async Task<GenreOutputDto> UpdateGenreAsync(Guid id, UpdateGenreInputDto input)
        {
            var entity = await _repository.GetAsync(id);
            entity.Name = input.Name;
            await _repository.UpdateAsync(entity);
            return new GenreOutputDto { Id = entity.Id, Name = entity.Name };
        }

        public async Task DeleteGenreAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
