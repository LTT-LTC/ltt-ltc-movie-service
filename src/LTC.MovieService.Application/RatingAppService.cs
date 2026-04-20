using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LTC.MovieService.Entities;
using LTC.MovieService.Rating;
using LTC.MovieService.Rating.Dtos.Input;
using LTC.MovieService.Rating.Dtos.Output;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace LTC.MovieService
{
    public class RatingAppService : MovieServiceAppService, IRatingAppService
    {
        private readonly IRepository<Rating, Guid> _repository;

        public RatingAppService(IRepository<Rating, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<PagedResultDto<RatingOutputDto>> GetAllAsync(GetRatingListInputDto input)
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

        public async Task<RatingOutputDto> GetAsync(Guid id)
        {
            var e = await _repository.GetAsync(id);
            return new RatingOutputDto { Id = e.Id, Code = e.Code, Name = e.Name, Description = e.Description };
        }

        public async Task<RatingOutputDto> CreateAsync(CreateRatingInputDto input)
        {
            var e = new Rating { Code = input.Code, Name = input.Name, Description = input.Description };
            await _repository.InsertAsync(e);
            return new RatingOutputDto { Id = e.Id, Code = e.Code, Name = e.Name, Description = e.Description };
        }

        public async Task<RatingOutputDto> UpdateAsync(Guid id, UpdateRatingInputDto input)
        {
            var e = await _repository.GetAsync(id);
            e.Code = input.Code;
            e.Name = input.Name;
            e.Description = input.Description;
            await _repository.UpdateAsync(e);
            return new RatingOutputDto { Id = e.Id, Code = e.Code, Name = e.Name, Description = e.Description };
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
