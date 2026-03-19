using Volo.Abp.Settings;

namespace LTC.MovieService.Settings;

public class MovieServiceSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(MovieServiceSettings.MySetting1));
    }
}
