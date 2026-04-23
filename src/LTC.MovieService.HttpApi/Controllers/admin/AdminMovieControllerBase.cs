using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace LTC.MovieService.Controllers.Admin
{
    /// <summary>
    /// Base controller for Admin-only operations in MovieService.
    /// </summary>
    [RemoteService]
    [Area("admin")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public abstract class AdminMovieControllerBase : AbpControllerBase
    {
    }
}
