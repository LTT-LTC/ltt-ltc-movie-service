using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LTC.MovieService.Entities;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace LTC.MovieService.Admin
{
    public class DistributionAppService(
        IRepository<MovieDistribution, Guid> distributionRepository,
        IRepository<Movie, Guid> movieRepository) 
        : MovieServiceAppService, IDistributionAppService
    {
        public async Task<PagedResultDto<DistributionOutputDto>> GetListAsync(GetDistributionListInputDto input)
        {
            var queryable = await distributionRepository.GetQueryableAsync();
            var moviesQueryable = await movieRepository.GetQueryableAsync();

            var query = from dist in queryable
                        join movie in moviesQueryable on dist.MovieId equals movie.Id
                        select new { dist, movie };

            var totalCount = await query.CountAsync();
            
            var result = await query
                .OrderByDescending(x => x.dist.CreatedAt)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToListAsync();

            var items = result.Select(x => new DistributionOutputDto
            {
                Id = x.dist.Id,
                MovieId = x.movie.Id,
                MovieTitle = x.movie.Title,
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
                CreatedAt = DateTime.UtcNow
            };

            await distributionRepository.InsertAsync(dist);
            
            var movie = await movieRepository.GetAsync(dist.MovieId);
            return new DistributionOutputDto
            {
                Id = dist.Id,
                MovieId = movie.Id,
                MovieTitle = movie.Title,
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
            dist.UpdatedAt = DateTime.UtcNow;

            await distributionRepository.UpdateAsync(dist);

            var movie = await movieRepository.GetAsync(dist.MovieId);
            return new DistributionOutputDto
            {
                Id = dist.Id,
                MovieId = movie.Id,
                MovieTitle = movie.Title,
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
