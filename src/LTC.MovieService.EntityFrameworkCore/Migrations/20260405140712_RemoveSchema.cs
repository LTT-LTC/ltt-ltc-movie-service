using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LTC.MovieService.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Studios",
                schema: "LTC",
                newName: "Studios");

            migrationBuilder.RenameTable(
                name: "Ratings",
                schema: "LTC",
                newName: "Ratings");

            migrationBuilder.RenameTable(
                name: "OpenIddictTokens",
                schema: "LTC",
                newName: "OpenIddictTokens");

            migrationBuilder.RenameTable(
                name: "OpenIddictScopes",
                schema: "LTC",
                newName: "OpenIddictScopes");

            migrationBuilder.RenameTable(
                name: "OpenIddictAuthorizations",
                schema: "LTC",
                newName: "OpenIddictAuthorizations");

            migrationBuilder.RenameTable(
                name: "OpenIddictApplications",
                schema: "LTC",
                newName: "OpenIddictApplications");

            migrationBuilder.RenameTable(
                name: "Movies",
                schema: "LTC",
                newName: "Movies");

            migrationBuilder.RenameTable(
                name: "MovieRoles",
                schema: "LTC",
                newName: "MovieRoles");

            migrationBuilder.RenameTable(
                name: "MovieGenres",
                schema: "LTC",
                newName: "MovieGenres");

            migrationBuilder.RenameTable(
                name: "MovieFormats",
                schema: "LTC",
                newName: "MovieFormats");

            migrationBuilder.RenameTable(
                name: "MovieDistributions",
                schema: "LTC",
                newName: "MovieDistributions");

            migrationBuilder.RenameTable(
                name: "MovieActors",
                schema: "LTC",
                newName: "MovieActors");

            migrationBuilder.RenameTable(
                name: "MovieActorRoles",
                schema: "LTC",
                newName: "MovieActorRoles");

            migrationBuilder.RenameTable(
                name: "Genres",
                schema: "LTC",
                newName: "Genres");

            migrationBuilder.RenameTable(
                name: "Formats",
                schema: "LTC",
                newName: "Formats");

            migrationBuilder.RenameTable(
                name: "Actors",
                schema: "LTC",
                newName: "Actors");

            migrationBuilder.RenameTable(
                name: "AbpUserTokens",
                schema: "LTC",
                newName: "AbpUserTokens");

            migrationBuilder.RenameTable(
                name: "AbpUsers",
                schema: "LTC",
                newName: "AbpUsers");

            migrationBuilder.RenameTable(
                name: "AbpUserRoles",
                schema: "LTC",
                newName: "AbpUserRoles");

            migrationBuilder.RenameTable(
                name: "AbpUserPasswordHistories",
                schema: "LTC",
                newName: "AbpUserPasswordHistories");

            migrationBuilder.RenameTable(
                name: "AbpUserPasskeys",
                schema: "LTC",
                newName: "AbpUserPasskeys");

            migrationBuilder.RenameTable(
                name: "AbpUserOrganizationUnits",
                schema: "LTC",
                newName: "AbpUserOrganizationUnits");

            migrationBuilder.RenameTable(
                name: "AbpUserLogins",
                schema: "LTC",
                newName: "AbpUserLogins");

            migrationBuilder.RenameTable(
                name: "AbpUserDelegations",
                schema: "LTC",
                newName: "AbpUserDelegations");

            migrationBuilder.RenameTable(
                name: "AbpUserClaims",
                schema: "LTC",
                newName: "AbpUserClaims");

            migrationBuilder.RenameTable(
                name: "AbpTenants",
                schema: "LTC",
                newName: "AbpTenants");

            migrationBuilder.RenameTable(
                name: "AbpTenantConnectionStrings",
                schema: "LTC",
                newName: "AbpTenantConnectionStrings");

            migrationBuilder.RenameTable(
                name: "AbpSettings",
                schema: "LTC",
                newName: "AbpSettings");

            migrationBuilder.RenameTable(
                name: "AbpSettingDefinitions",
                schema: "LTC",
                newName: "AbpSettingDefinitions");

            migrationBuilder.RenameTable(
                name: "AbpSessions",
                schema: "LTC",
                newName: "AbpSessions");

            migrationBuilder.RenameTable(
                name: "AbpSecurityLogs",
                schema: "LTC",
                newName: "AbpSecurityLogs");

            migrationBuilder.RenameTable(
                name: "AbpRoles",
                schema: "LTC",
                newName: "AbpRoles");

            migrationBuilder.RenameTable(
                name: "AbpRoleClaims",
                schema: "LTC",
                newName: "AbpRoleClaims");

            migrationBuilder.RenameTable(
                name: "AbpResourcePermissionGrants",
                schema: "LTC",
                newName: "AbpResourcePermissionGrants");

            migrationBuilder.RenameTable(
                name: "AbpPermissions",
                schema: "LTC",
                newName: "AbpPermissions");

            migrationBuilder.RenameTable(
                name: "AbpPermissionGroups",
                schema: "LTC",
                newName: "AbpPermissionGroups");

            migrationBuilder.RenameTable(
                name: "AbpPermissionGrants",
                schema: "LTC",
                newName: "AbpPermissionGrants");

            migrationBuilder.RenameTable(
                name: "AbpOrganizationUnits",
                schema: "LTC",
                newName: "AbpOrganizationUnits");

            migrationBuilder.RenameTable(
                name: "AbpOrganizationUnitRoles",
                schema: "LTC",
                newName: "AbpOrganizationUnitRoles");

            migrationBuilder.RenameTable(
                name: "AbpLinkUsers",
                schema: "LTC",
                newName: "AbpLinkUsers");

            migrationBuilder.RenameTable(
                name: "AbpFeatureValues",
                schema: "LTC",
                newName: "AbpFeatureValues");

            migrationBuilder.RenameTable(
                name: "AbpFeatures",
                schema: "LTC",
                newName: "AbpFeatures");

            migrationBuilder.RenameTable(
                name: "AbpFeatureGroups",
                schema: "LTC",
                newName: "AbpFeatureGroups");

            migrationBuilder.RenameTable(
                name: "AbpEntityPropertyChanges",
                schema: "LTC",
                newName: "AbpEntityPropertyChanges");

            migrationBuilder.RenameTable(
                name: "AbpEntityChanges",
                schema: "LTC",
                newName: "AbpEntityChanges");

            migrationBuilder.RenameTable(
                name: "AbpClaimTypes",
                schema: "LTC",
                newName: "AbpClaimTypes");

            migrationBuilder.RenameTable(
                name: "AbpBackgroundJobs",
                schema: "LTC",
                newName: "AbpBackgroundJobs");

            migrationBuilder.RenameTable(
                name: "AbpAuditLogs",
                schema: "LTC",
                newName: "AbpAuditLogs");

            migrationBuilder.RenameTable(
                name: "AbpAuditLogExcelFiles",
                schema: "LTC",
                newName: "AbpAuditLogExcelFiles");

            migrationBuilder.RenameTable(
                name: "AbpAuditLogActions",
                schema: "LTC",
                newName: "AbpAuditLogActions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "LTC");

            migrationBuilder.RenameTable(
                name: "Studios",
                newName: "Studios",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "Ratings",
                newName: "Ratings",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "OpenIddictTokens",
                newName: "OpenIddictTokens",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "OpenIddictScopes",
                newName: "OpenIddictScopes",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "OpenIddictAuthorizations",
                newName: "OpenIddictAuthorizations",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "OpenIddictApplications",
                newName: "OpenIddictApplications",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "Movies",
                newName: "Movies",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "MovieRoles",
                newName: "MovieRoles",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "MovieGenres",
                newName: "MovieGenres",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "MovieFormats",
                newName: "MovieFormats",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "MovieDistributions",
                newName: "MovieDistributions",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "MovieActors",
                newName: "MovieActors",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "MovieActorRoles",
                newName: "MovieActorRoles",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "Genres",
                newName: "Genres",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "Formats",
                newName: "Formats",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "Actors",
                newName: "Actors",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "AbpUserTokens",
                newName: "AbpUserTokens",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "AbpUsers",
                newName: "AbpUsers",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "AbpUserRoles",
                newName: "AbpUserRoles",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "AbpUserPasswordHistories",
                newName: "AbpUserPasswordHistories",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "AbpUserPasskeys",
                newName: "AbpUserPasskeys",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "AbpUserOrganizationUnits",
                newName: "AbpUserOrganizationUnits",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "AbpUserLogins",
                newName: "AbpUserLogins",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "AbpUserDelegations",
                newName: "AbpUserDelegations",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "AbpUserClaims",
                newName: "AbpUserClaims",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "AbpTenants",
                newName: "AbpTenants",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "AbpTenantConnectionStrings",
                newName: "AbpTenantConnectionStrings",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "AbpSettings",
                newName: "AbpSettings",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "AbpSettingDefinitions",
                newName: "AbpSettingDefinitions",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "AbpSessions",
                newName: "AbpSessions",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "AbpSecurityLogs",
                newName: "AbpSecurityLogs",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "AbpRoles",
                newName: "AbpRoles",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "AbpRoleClaims",
                newName: "AbpRoleClaims",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "AbpResourcePermissionGrants",
                newName: "AbpResourcePermissionGrants",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "AbpPermissions",
                newName: "AbpPermissions",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "AbpPermissionGroups",
                newName: "AbpPermissionGroups",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "AbpPermissionGrants",
                newName: "AbpPermissionGrants",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "AbpOrganizationUnits",
                newName: "AbpOrganizationUnits",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "AbpOrganizationUnitRoles",
                newName: "AbpOrganizationUnitRoles",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "AbpLinkUsers",
                newName: "AbpLinkUsers",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "AbpFeatureValues",
                newName: "AbpFeatureValues",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "AbpFeatures",
                newName: "AbpFeatures",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "AbpFeatureGroups",
                newName: "AbpFeatureGroups",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "AbpEntityPropertyChanges",
                newName: "AbpEntityPropertyChanges",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "AbpEntityChanges",
                newName: "AbpEntityChanges",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "AbpClaimTypes",
                newName: "AbpClaimTypes",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "AbpBackgroundJobs",
                newName: "AbpBackgroundJobs",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "AbpAuditLogs",
                newName: "AbpAuditLogs",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "AbpAuditLogExcelFiles",
                newName: "AbpAuditLogExcelFiles",
                newSchema: "LTC");

            migrationBuilder.RenameTable(
                name: "AbpAuditLogActions",
                newName: "AbpAuditLogActions",
                newSchema: "LTC");
        }
    }
}
