using Microsoft.Extensions.Localization;
using LTC.MovieService.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace LTC.MovieService;

[Dependency(ReplaceServices = true)]
public class MovieServiceBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<MovieServiceResource> _localizer;

    public MovieServiceBrandingProvider(IStringLocalizer<MovieServiceResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
