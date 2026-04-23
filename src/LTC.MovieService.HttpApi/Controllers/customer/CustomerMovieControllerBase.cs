using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace LTC.MovieService.Controllers.Customer
{
    [RemoteService]
    [Area("customer")]
    [ApiController]
    [Authorize]
    public abstract class CustomerMovieControllerBase : AbpControllerBase
    {
    }
}
