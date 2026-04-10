using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LTC.MovieService.Dto;
using LTC.MovieService.Entities;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;

namespace LTC.MovieService
{
    public class MovieAppService : MovieServiceAppService, IMovieAppService
    {
        private readonly IRepository<Movie, Guid> _movieRepository;

        public MovieAppService(IRepository<Movie, Guid> movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<List<MovieOutputDto>> GetAllAsync()
        {
            var movies = await _movieRepository.GetListAsync();
            var mapper = new MovieServiceApplicationMappers();
            return mapper.MapToMovieOutputDtoList(movies);
        }

        public async Task<MovieOutputDto> GetAsync(Guid id)
        {
            var movie = await _movieRepository.FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                throw new UserFriendlyException(L["NotFound"]);
            }
            var mapper = new MovieServiceApplicationMappers();
            return mapper.MapToMovieOutputDto(movie);
        }
    }
}