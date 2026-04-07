using LTC.MovieService.Entities;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace LTC.MovieService.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class MovieServiceDbContext :
    AbpDbContext<MovieServiceDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    #region Entities from the modules

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
    public DbSet<IdentitySession> Sessions { get; set; }
    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    #region Movie Entities

    public DbSet<Movie> Movies { get; set; }
    public DbSet<Studio> Studios { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<MovieGenre> MovieGenres { get; set; }
    public DbSet<Actor> Actors { get; set; }
    public DbSet<MovieActor> MovieActors { get; set; }
    public DbSet<MovieRole> MovieRoles { get; set; }
    public DbSet<MovieActorRole> MovieActorRoles { get; set; }
    public DbSet<MovieFormat> MovieFormats { get; set; }
    public DbSet<Format> Formats { get; set; }
    public DbSet<MovieDistribution> MovieDistributions { get; set; }

    #endregion

    public MovieServiceDbContext(DbContextOptions<MovieServiceDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema(MovieServiceConsts.DbSchema);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure Movie entities */

        builder.Entity<Movie>(b =>
        {
            b.ToTable("Movies", MovieServiceConsts.DbSchema);
            b.ConfigureByConvention();
        });

        builder.Entity<Studio>(b =>
        {
            b.ToTable("Studios", MovieServiceConsts.DbSchema);
            b.ConfigureByConvention();
        });

        builder.Entity<Rating>(b =>
        {
            b.ToTable("Ratings", MovieServiceConsts.DbSchema);
            b.ConfigureByConvention();
        });

        builder.Entity<Genre>(b =>
        {
            b.ToTable("Genres", MovieServiceConsts.DbSchema);
            b.ConfigureByConvention();
        });

        builder.Entity<MovieGenre>(b =>
        {
            b.ToTable("MovieGenres", MovieServiceConsts.DbSchema);
            b.ConfigureByConvention();
        });

        builder.Entity<Actor>(b =>
        {
            b.ToTable("Actors", MovieServiceConsts.DbSchema);
            b.ConfigureByConvention();
        });

        builder.Entity<MovieActor>(b =>
        {
            b.ToTable("MovieActors", MovieServiceConsts.DbSchema);
            b.ConfigureByConvention();
        });

        builder.Entity<MovieRole>(b =>
        {
            b.ToTable("MovieRoles", MovieServiceConsts.DbSchema);
            b.ConfigureByConvention();
        });

        builder.Entity<MovieActorRole>(b =>
        {
            b.ToTable("MovieActorRoles", MovieServiceConsts.DbSchema);
            b.ConfigureByConvention();
        });

        builder.Entity<MovieFormat>(b =>
        {
            b.ToTable("MovieFormats", MovieServiceConsts.DbSchema);
            b.ConfigureByConvention();
        });

        builder.Entity<Format>(b =>
        {
            b.ToTable("Formats", MovieServiceConsts.DbSchema);
            b.ConfigureByConvention();
        });

        builder.Entity<MovieDistribution>(b =>
        {
            b.ToTable("MovieDistributions", MovieServiceConsts.DbSchema);
            b.ConfigureByConvention();
        });
    }
}

