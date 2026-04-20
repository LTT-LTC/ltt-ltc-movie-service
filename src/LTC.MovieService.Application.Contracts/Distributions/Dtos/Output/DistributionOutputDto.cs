using LTC.MovieService.Dtos.Output;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace LTC.MovieService.Distributions.Dtos.Output
{
    public class GetDistributionListInputDto : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
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
