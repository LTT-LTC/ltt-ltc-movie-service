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

            var items = entities.Select(e => new MovieOutputDto {
                Id = e.Id,
                MovieId = e.Id,
                Title = e.Title,
                OriginalTitle = e.OriginalTitle,
                DurationMins = e.DurationMins,
                ReleaseDate = e.ReleaseDate,
                PremiereDate = e.PremiereDate,
                Status = e.Status,
                Description = e.Description,
                PosterUrl = e.PosterUrl,
                TrailerUrl = e.TrailerUrl,
                StudioId = e.StudioId,
                CreatedAt = e.CreatedAt,
                UpdatedAt = e.UpdatedAt,
            }).ToList();
            return new PagedResultDto<MovieOutputDto>(totalCount, items);
        }

        public async Task<MovieDetailOutputDto> GetAsync(Guid id)
        {
            var movie = await _repository.FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                throw new UserFriendlyException(L["NotFound"]);
            }
            return new MovieDetailOutputDto {
                Id = movie.Id,
                MovieId = movie.Id,
                Title = movie.Title,
                OriginalTitle = movie.OriginalTitle,
                DurationMins = movie.DurationMins,
                ReleaseDate = movie.ReleaseDate,
                PremiereDate = movie.PremiereDate,
                Status = movie.Status,
                Description = movie.Description,
                PosterUrl = movie.PosterUrl,
                TrailerUrl = movie.TrailerUrl,
                StudioId = movie.StudioId,
                CreatedAt = movie.CreatedAt,
                UpdatedAt = movie.UpdatedAt,
            };
        }

        public async Task<MovieOutputDto> CreateAsync(CreateMovieInputDto input)
        {
            if (CurrentUser == null || !CurrentUser.IsInRole("admin"))
            {
                throw new UserFriendlyException("Only admins can perform this action.");
            }

            var entity = new Movie() { 
                Title = input.Title,
                OriginalTitle = input.OriginalTitle,
                DurationMins = input.DurationMins,
                ReleaseDate = input.ReleaseDate,
                PremiereDate = input.PremiereDate,
                Status = input.Status,
                Description = input.Description,
                PosterUrl = input.PosterUrl,
                TrailerUrl = input.TrailerUrl,
                StudioId = input.StudioId,
                CreatedAt = DateTime.UtcNow,
            };
            await _repository.InsertAsync(entity);
            return new MovieOutputDto {
                Id = entity.Id,
                MovieId = entity.Id,
                Title = input.Title,
                OriginalTitle = input.OriginalTitle,
                DurationMins = input.DurationMins,
                ReleaseDate = input.ReleaseDate,
                PremiereDate = input.PremiereDate,
                Status = input.Status,
                Description = input.Description,
                PosterUrl = input.PosterUrl,
                TrailerUrl = input.TrailerUrl,
                StudioId = input.StudioId,
                CreatedAt = DateTime.UtcNow,
            };
        }

        public async Task<MovieOutputDto> UpdateAsync(Guid id, UpdateMovieInputDto input)
        {
            if (CurrentUser == null || !CurrentUser.IsInRole("admin"))
            {
                throw new UserFriendlyException("Only admins can perform this action.");
            }

            var entity = await _repository.GetAsync(id);
            
            if (!string.IsNullOrWhiteSpace(input.Title)) entity.Title = input.Title;
            if (input.OriginalTitle != null && input.OriginalTitle != string.Empty) entity.OriginalTitle = input.OriginalTitle;
            if (input.DurationMins.HasValue) entity.DurationMins = input.DurationMins.Value;
            if (input.ReleaseDate.HasValue) entity.ReleaseDate = input.ReleaseDate.Value;
            if (input.PremiereDate.HasValue) entity.PremiereDate = input.PremiereDate.Value;
            if (!string.IsNullOrWhiteSpace(input.Status)) entity.Status = input.Status;
            if (input.Description != null) entity.Description = input.Description;
            if (input.PosterUrl != null) entity.PosterUrl = input.PosterUrl;
            if (input.TrailerUrl != null) entity.TrailerUrl = input.TrailerUrl;
            if (input.StudioId != Guid.Empty) entity.StudioId = input.StudioId;

            entity.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(entity);
            
            return new MovieOutputDto {
                Id = entity.Id,
                MovieId = entity.Id,
                Title = entity.Title,
                OriginalTitle = entity.OriginalTitle,
                DurationMins = entity.DurationMins,
                ReleaseDate = entity.ReleaseDate,
                PremiereDate = entity.PremiereDate,
                Status = entity.Status,
                Description = entity.Description,
                PosterUrl = entity.PosterUrl,
                TrailerUrl = entity.TrailerUrl,
                StudioId = entity.StudioId,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public async Task DeleteAsync(Guid id)
        {
            if (CurrentUser == null || !CurrentUser.IsInRole("admin"))
            {
                throw new UserFriendlyException("Only admins can perform this action.");
            }

            await _repository.DeleteAsync(id);
        }
    }
}
