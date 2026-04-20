using System;
using System.Collections.Generic;
using System.Text;

namespace LTC.MovieService.Distributions.Dtos.Input
{
    public class CreateDistributionInputDto
    {
        public Guid MovieId { get; set; }
        public DateTime? LicenseStartDate { get; set; }
        public DateTime? LicenseEndDate { get; set; }
        public bool IsExclusive { get; set; }
    }
}
