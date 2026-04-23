using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LTC.MovieService.Distributions;
using LTC.MovieService.Distributions.Dtos.Input;
using LTC.MovieService.Distributions.Dtos.Output;
using LTC.MovieService.Entities;
using LTC.MovieService.Movies;
using LTC.Shared.Hosting.Microservices.Timing;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace LTC.MovieService.Distributions
{
    public class DistributionAppService(
        IRepository<MovieDistribution, Guid> distributionRepository,
        IRepository<Movie, Guid> movieRepository,
        MovieAppService movieAppService,
        IGmt7Clock gmt7Clock) 
        : MovieServiceAppService, IDistributionAppService
    {
        public async Task<PagedResultDto<DistributionOutputDto>> GetListAsync(GetDistributionListInputDto input)
        {
            var queryable = await distributionRepository.GetQueryableAsync();
            var moviesQueryable = await movieRepository.GetQueryableAsync();

            var skipCount = input.SkipCount < 0 ? 0 : input.SkipCount;
            var maxResultCount = input.MaxResultCount <= 0 ? 100 : input.MaxResultCount;

            var query = from dist in queryable
                        join movie in moviesQueryable on dist.MovieId equals movie.Id
                        select new { dist, movie };

            var totalCount = await query.CountAsync();
            
            var result = await query
                .OrderByDescending(x => x.dist.CreatedAt)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();

            var relatedMovies = await movieAppService.BuildMovieOutputsAsync(result.Select(item => item.movie).ToList());
            var movieLookup = relatedMovies.ToDictionary(item => item.MovieId, item => item);

            var items = result.Select(x => new DistributionOutputDto
            {
                Id = x.dist.Id,
                MovieId = x.movie.Id,
                MovieTitle = x.movie.Title,
                Movie = movieLookup.TryGetValue(x.movie.Id, out var movie) ? movie : null,
                LicenseStartDate = x.dist.LicenseStartDate,
                LicenseEndDate = x.dist.LicenseEndDate,
                IsExclusive = x.dist.IsExclusive
            }).ToList();

            return new PagedResultDto<DistributionOutputDto>(totalCount, items);
        }

        public async Task<DistributionOutputDto> CreateAsync(CreateDistributionInputDto input)
        {
            var dist = new MovieDistribution
            {
                MovieId = input.MovieId,
                LicenseStartDate = input.LicenseStartDate,
                LicenseEndDate = input.LicenseEndDate,
                IsExclusive = input.IsExclusive,
                CreatedAt = gmt7Clock.Gmt7Now
            };

            await distributionRepository.InsertAsync(dist);
            
            var movie = await movieRepository.GetAsync(dist.MovieId);
            var movieOutput = await movieAppService.BuildMovieOutputAsync(movie);
            return new DistributionOutputDto
            {
                Id = dist.Id,
                MovieId = movie.Id,
                MovieTitle = movie.Title,
                Movie = movieOutput,
                LicenseStartDate = dist.LicenseStartDate,
                LicenseEndDate = dist.LicenseEndDate,
                IsExclusive = dist.IsExclusive
            };
        }

        public async Task<DistributionOutputDto> UpdateAsync(Guid id, UpdateDistributionInputDto input)
        {
            var dist = await distributionRepository.GetAsync(id);
            dist.LicenseStartDate = input.LicenseStartDate;
            dist.LicenseEndDate = input.LicenseEndDate;
            dist.IsExclusive = input.IsExclusive;
            dist.UpdatedAt = gmt7Clock.Gmt7Now;

            await distributionRepository.UpdateAsync(dist);

            var movie = await movieRepository.GetAsync(dist.MovieId);
            var movieOutput = await movieAppService.BuildMovieOutputAsync(movie);
            return new DistributionOutputDto
            {
                Id = dist.Id,
                MovieId = movie.Id,
                MovieTitle = movie.Title,
                Movie = movieOutput,
                LicenseStartDate = dist.LicenseStartDate,
                LicenseEndDate = dist.LicenseEndDate,
                IsExclusive = dist.IsExclusive
            };
        }

        public async Task DeleteAsync(Guid id)
        {
            await distributionRepository.DeleteAsync(id);
        }
    }
}
