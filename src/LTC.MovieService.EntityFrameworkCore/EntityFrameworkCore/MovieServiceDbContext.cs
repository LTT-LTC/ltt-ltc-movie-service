using LTC.MovieService.Entities;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
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

    public MovieServiceDbContext(DbContextOptions<MovieServiceDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema(MovieServiceConsts.DbSchema);


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
            b.ToTable("Roles", MovieServiceConsts.DbSchema);
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
            b.HasOne<Movie>().WithMany().HasForeignKey(x => x.MovieId);
            b.HasOne<Format>().WithMany().HasForeignKey(x => x.FormatId);
        });

        builder.Entity<Format>(b =>
        {
            b.ToTable("Formats", MovieServiceConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired();
        });

        builder.Entity<MovieDistribution>(b =>
        {
            b.ToTable("MovieDistributions", MovieServiceConsts.DbSchema);
            b.ConfigureByConvention();
        });
    }
}

