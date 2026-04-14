using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using LTC.MovieService.Dtos.Input;
using LTC.MovieService.Dtos.Output;
using LTC.MovieService.Entities;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace LTC.MovieService
{
    public class MovieAppService : MovieServiceAppService, IMovieAppService
    {
        private readonly IRepository<Movie, Guid> _repository;

        public MovieAppService(IRepository<Movie, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<PagedResultDto<MovieOutputDto>> GetAllAsync(GetMovieListInputDto input)
        {
            var query = await _repository.GetQueryableAsync();
            var maxCount = input.Fetch > 0 ? input.Fetch : 10;
            var skipCount = (input.Page > 1 ? input.Page - 1 : 0) * maxCount;
            
            var totalCount = await AsyncExecuter.CountAsync(query);
            var entities = await AsyncExecuter.ToListAsync(
                query.OrderBy(x => x.Id).Skip(skipCount).Take(maxCount)
            );

            // Dummy manual map to pass compile.
            var items = entities.Select(e => new MovieOutputDto { Id = e.Id, Title = e.Title }).ToList();
            return new PagedResultDto<MovieOutputDto>(totalCount, items);
        }

        public async Task<MovieDetailOutputDto> GetAsync(Guid id)
        {
            var movie = await _repository.FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                throw new UserFriendlyException(L["NotFound"]);
            }
            return new MovieDetailOutputDto { Id = movie.Id, Title = movie.Title };
        }

        public async Task<MovieOutputDto> CreateAsync(CreateMovieInputDto input)
        {
            var entity = new Movie() { Title = input.Title };
            await _repository.InsertAsync(entity);
            return new MovieOutputDto { Id = entity.Id, Title = entity.Title };
        }

        public async Task<MovieOutputDto> UpdateAsync(Guid id, UpdateMovieInputDto input)
        {
            var entity = await _repository.GetAsync(id);
            entity.Title = input.Title;
            await _repository.UpdateAsync(entity);
            return new MovieOutputDto { Id = entity.Id, Title = entity.Title };
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
