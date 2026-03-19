using System;
using System.Collections.Generic;
using System.Text;
using LTC.MovieService.Localization;
using Volo.Abp.Application.Services;

namespace LTC.MovieService;

/* Inherit your application services from this class.
 */
public abstract class MovieServiceAppService : ApplicationService
{
    protected MovieServiceAppService()
    {
        LocalizationResource = typeof(MovieServiceResource);
    }
}
