using Localization.Resources.AbpUi;
using LTC.MovieService.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace LTC.MovieService;

[DependsOn(
    typeof(MovieServiceApplicationContractsModule)
    )]
public class MovieServiceHttpApiModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ConfigureLocalization();
    }

    private void ConfigureLocalization()
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<MovieServiceResource>()
                .AddBaseTypes(
                    typeof(AbpUiResource)
                );
        });
    }
}
