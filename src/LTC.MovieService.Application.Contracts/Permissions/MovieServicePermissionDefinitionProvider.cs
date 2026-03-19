using LTC.MovieService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace LTC.MovieService.Permissions;

public class MovieServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(MovieServicePermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(MovieServicePermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MovieServiceResource>(name);
    }
}
