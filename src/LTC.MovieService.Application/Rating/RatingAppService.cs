using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LTC.MovieService.Entities;
using LTC.MovieService.Ratings;
using LTC.MovieService.Ratings.Dtos.Input;
using LTC.MovieService.Ratings.Dtos.Output;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace LTC.MovieService.Ratings
{
    public class RatingAppService : MovieServiceAppService, IRatingAppService
    {
        private readonly IRepository<Rating, Guid> _repository;

        public RatingAppService(IRepository<Rating, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<PagedResultDto<RatingOutputDto>> GetRatingListAsync(GetRatingListInputDto input)
        {
            var query = await _repository.GetQueryableAsync();
            
            var maxCount = input.Fetch > 0 ? input.Fetch : 10;
            var skipCount = (input.Page > 1 ? input.Page - 1 : 0) * maxCount;
            
            var totalCount = await AsyncExecuter.CountAsync(query);
            
            var entities = await AsyncExecuter.ToListAsync(
                query.OrderBy(x => x.Code).Skip(skipCount).Take(maxCount)
            );

            var items = entities.Select(e => new RatingOutputDto 
            { 
                Id = e.Id, 
                Code = e.Code, 
                Name = e.Name, 
                Description = e.Description 
            }).ToList();

            return new PagedResultDto<RatingOutputDto>(totalCount, items);
        }

        public async Task<RatingOutputDto> GetRatingAsync(Guid id)
        {
            var e = await _repository.GetAsync(id);
            return new RatingOutputDto { Id = e.Id, Code = e.Code, Name = e.Name, Description = e.Description };
        }

        public async Task<RatingOutputDto> CreateRatingAsync(CreateRatingInputDto input)
        {
            var normalizedCode = NormalizeComparableText(input.Code);
            var normalizedName = NormalizeComparableText(input.Name);
            if (string.IsNullOrWhiteSpace(normalizedCode) || string.IsNullOrWhiteSpace(normalizedName))
            {
                throw new UserFriendlyException("Rating code and name are required.");
            }

            await EnsureRatingUniqueAsync(normalizedCode, normalizedName);

            var e = new Rating { Code = input.Code.Trim(), Name = input.Name.Trim(), Description = input.Description };
            await _repository.InsertAsync(e);
            return new RatingOutputDto { Id = e.Id, Code = e.Code, Name = e.Name, Description = e.Description };
        }

        public async Task<RatingOutputDto> UpdateRatingAsync(Guid id, UpdateRatingInputDto input)
        {
            var e = await _repository.GetAsync(id);
            var normalizedCode = NormalizeComparableText(input.Code);
            var normalizedName = NormalizeComparableText(input.Name);
            if (string.IsNullOrWhiteSpace(normalizedCode) || string.IsNullOrWhiteSpace(normalizedName))
            {
                throw new UserFriendlyException("Rating code and name are required.");
            }

            await EnsureRatingUniqueAsync(normalizedCode, normalizedName, id);

            e.Code = input.Code.Trim();
            e.Name = input.Name.Trim();
            e.Description = input.Description;
            await _repository.UpdateAsync(e);
            return new RatingOutputDto { Id = e.Id, Code = e.Code, Name = e.Name, Description = e.Description };
        }

        public async Task DeleteRatingAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        private async Task EnsureRatingUniqueAsync(string normalizedCode, string normalizedName, Guid? excludeId = null)
        {
            var query = await _repository.GetQueryableAsync();
            var entities = await AsyncExecuter.ToListAsync(query);
            var duplicateCode = entities.Any(item =>
                (!excludeId.HasValue || item.Id != excludeId.Value) &&
                NormalizeComparableText(item.Code) == normalizedCode);
            if (duplicateCode)
            {
                throw new UserFriendlyException("Rating code already exists.");
            }

            var duplicateName = entities.Any(item =>
                (!excludeId.HasValue || item.Id != excludeId.Value) &&
                NormalizeComparableText(item.Name) == normalizedName);
            if (duplicateName)
            {
                throw new UserFriendlyException("Rating name already exists.");
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
