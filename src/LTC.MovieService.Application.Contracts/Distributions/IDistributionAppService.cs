using System;
using System.Threading.Tasks;
using LTC.MovieService.Dtos.Output;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace LTC.MovieService.Admin
{
    public interface IDistributionAppService : IApplicationService
    {
        Task<PagedResultDto<DistributionOutputDto>> GetListAsync(GetDistributionListInputDto input);
        Task<DistributionOutputDto> CreateAsync(CreateDistributionInputDto input);
        Task<DistributionOutputDto> UpdateAsync(Guid id, UpdateDistributionInputDto input);
        Task DeleteAsync(Guid id);
    }

    public class GetDistributionListInputDto : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
    }

    public class CreateDistributionInputDto
    {
        public Guid MovieId { get; set; }
        public DateTime? LicenseStartDate { get; set; }
        public DateTime? LicenseEndDate { get; set; }
        public bool IsExclusive { get; set; }
    }

    public class UpdateDistributionInputDto
    {
        public DateTime? LicenseStartDate { get; set; }
        public DateTime? LicenseEndDate { get; set; }
        public bool IsExclusive { get; set; }
    }

    public class DistributionOutputDto
    {
        public Guid Id { get; set; }
        public Guid MovieId { get; set; }
        public string MovieTitle { get; set; }
        public MovieOutputDto? Movie { get; set; }
        public DateTime? LicenseStartDate { get; set; }
        public DateTime? LicenseEndDate { get; set; }
        public bool IsExclusive { get; set; }
    }
}