using LTC.MovieService.Entities;
using LTC.MovieService.MultiTenancy;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace LTC.MovieService.EntityFrameworkCore;

[ConnectionStringName("Default")]
public class MovieServiceDbContext : AbpDbContext<MovieServiceDbContext>
{
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

    private readonly ITenantSchemaResolver? _tenantSchemaResolver;

    public MovieServiceDbContext(
        DbContextOptions<MovieServiceDbContext> options,
        ITenantSchemaResolver? tenantSchemaResolver = null)
        : base(options)
    {
        _tenantSchemaResolver = tenantSchemaResolver;
    }

    public string GetCurrentSchema() => _tenantSchemaResolver?.GetSchemaName() ?? "dbo";

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        var schema = GetCurrentSchema();
        builder.HasDefaultSchema(schema);


        /* Configure Movie entities */

        builder.Entity<Movie>(b =>
        {
            b.ToTable("Movies");
            b.ConfigureByConvention();
        });

        builder.Entity<Studio>(b =>
        {
            b.ToTable("Studios");
            b.ConfigureByConvention();
        });

        builder.Entity<Rating>(b =>
        {
            b.ToTable("Ratings");
            b.ConfigureByConvention();
        });

        builder.Entity<Genre>(b =>
        {
            b.ToTable("Genres");
            b.ConfigureByConvention();
        });

        builder.Entity<MovieGenre>(b =>
        {
            b.ToTable("MovieGenres");
            b.ConfigureByConvention();
        });

        builder.Entity<Actor>(b =>
        {
            b.ToTable("Actors");
            b.ConfigureByConvention();
        });

        builder.Entity<MovieActor>(b =>
        {
            b.ToTable("MovieActors");
            b.ConfigureByConvention();
        });

        builder.Entity<MovieRole>(b =>
        {
            b.ToTable("Roles");
            b.ConfigureByConvention();
        });

        builder.Entity<MovieActorRole>(b =>
        {
            b.ToTable("MovieActorRoles");
            b.ConfigureByConvention();
        });

        builder.Entity<MovieFormat>(b =>
        {
            b.ToTable("MovieFormats");
            b.ConfigureByConvention();
            b.HasOne<Movie>().WithMany().HasForeignKey(x => x.MovieId);
            b.HasOne<Format>().WithMany().HasForeignKey(x => x.FormatId);
        });

        builder.Entity<Format>(b =>
        {
            b.ToTable("Formats");
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired();
        });

        builder.Entity<MovieDistribution>(b =>
        {
            b.ToTable("MovieDistributions");
            b.ConfigureByConvention();
        });
    }
}

