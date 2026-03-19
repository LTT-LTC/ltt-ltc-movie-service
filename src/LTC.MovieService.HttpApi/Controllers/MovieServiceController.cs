using LTC.MovieService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace LTC.MovieService.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class MovieServiceController : AbpControllerBase
{
    protected MovieServiceController()
    {
        LocalizationResource = typeof(MovieServiceResource);
    }
}
