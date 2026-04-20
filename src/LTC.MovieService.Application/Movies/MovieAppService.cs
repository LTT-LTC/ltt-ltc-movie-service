using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using LTC.MovieService.Dtos.Input;
using LTC.MovieService.Dtos.Output;
using LTC.MovieService.Entities;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using LTC.MovieService.Actors.Dtos.Output;
using LTC.MovieService.Genres.Dtos.Output;
using LTC.MovieService.Ratings.Dtos.Output;
using LTC.MovieService.Roles.Dtos.Output;
using LTC.MovieService.Studios.Dtos.Output;

namespace LTC.MovieService.Movies
{
    public class MovieAppService : MovieServiceAppService, IMovieAppService
    {
        private readonly IRepository<Movie, Guid> _repository;
        private readonly IRepository<Studio, Guid> _studioRepository;
        private readonly IRepository<Genre, Guid> _genreRepository;
        private readonly IRepository<MovieGenre, Guid> _movieGenreRepository;
        private readonly IRepository<Actor, Guid> _actorRepository;
        private readonly IRepository<MovieActor, Guid> _movieActorRepository;
        private readonly IRepository<MovieActorRole, Guid> _movieActorRoleRepository;
        private readonly IRepository<MovieRole, Guid> _movieRoleRepository;
        private readonly IRepository<Rating, Guid> _ratingRepository;

        public MovieAppService(
            IRepository<Movie, Guid> repository,
            IRepository<Studio, Guid> studioRepository,
            IRepository<Genre, Guid> genreRepository,
            IRepository<MovieGenre, Guid> movieGenreRepository,
            IRepository<Actor, Guid> actorRepository,
            IRepository<MovieActor, Guid> movieActorRepository,
            IRepository<MovieActorRole, Guid> movieActorRoleRepository,
            IRepository<MovieRole, Guid> movieRoleRepository,
            IRepository<Rating, Guid> ratingRepository)
        {
            _repository = repository;
            _studioRepository = studioRepository;
            _genreRepository = genreRepository;
            _movieGenreRepository = movieGenreRepository;
            _actorRepository = actorRepository;
            _movieActorRepository = movieActorRepository;
            _movieActorRoleRepository = movieActorRoleRepository;
            _movieRoleRepository = movieRoleRepository;
            _ratingRepository = ratingRepository;
        }

        public async Task<PagedResultDto<MovieOutputDto>> GetAllAsync(GetMovieListInputDto input)
        {
            var query = await _repository.GetQueryableAsync();
            var maxCount = input.Fetch > 0 ? input.Fetch : 10;
            var skipCount = (input.Page > 1 ? input.Page - 1 : 0) * maxCount;
            
            var totalCount = await AsyncExecuter.CountAsync(query);
            var entities = await AsyncExecuter.ToListAsync(
                query.OrderBy(x => x.Id).Skip(skipCount).Take(maxCount)
            );

            var items = await BuildMovieOutputsAsync(entities);
            return new PagedResultDto<MovieOutputDto>(totalCount, items);
        }

        public async Task<MovieDetailOutputDto> GetAsync(Guid id)
        {
            var movie = await _repository.FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                throw new UserFriendlyException(L["NotFound"]);
            }
            return await BuildMovieDetailOutputAsync(movie);
        }

        public async Task<MovieOutputDto> CreateAsync(CreateMovieInputDto input)
        {
            if (CurrentUser == null || !CurrentUser.IsInRole("admin"))
            {
                throw new UserFriendlyException("Only admins can perform this action.");
            }

            var studioId = await ResolveStudioIdAsync(input.StudioId, input.StudioName);

            var entity = new Movie() { 
                Title = input.Title,
                OriginalTitle = input.OriginalTitle,
                DurationMins = input.DurationMins,
                ReleaseDate = input.ReleaseDate,
                PremiereDate = input.PremiereDate,
                Status = input.Status,
                Description = input.Description,
                PosterUrl = input.PosterUrl,
                TrailerUrl = input.TrailerUrl,
                StudioId = studioId,
                RatingId = input.RatingId,
                CreatedAt = DateTime.UtcNow,
            };
            await _repository.InsertAsync(entity);

            await CreateMovieGenresAsync(entity.Id, input.GenreListId);
            await CreateMovieActorRolesAsync(entity.Id, input.ActorRoles);
            return await BuildMovieOutputAsync(entity);
        }

        public async Task<MovieOutputDto> UpdateAsync(Guid id, UpdateMovieInputDto input)
        {
            if (CurrentUser == null || !CurrentUser.IsInRole("admin"))
            {
                throw new UserFriendlyException("Only admins can perform this action.");
            }

            var entity = await _repository.GetAsync(id);
            
            if (!string.IsNullOrWhiteSpace(input.Title)) entity.Title = input.Title;
            if (input.OriginalTitle != null && input.OriginalTitle != string.Empty) entity.OriginalTitle = input.OriginalTitle;
            if (input.DurationMins.HasValue) entity.DurationMins = input.DurationMins.Value;
            if (input.ReleaseDate.HasValue) entity.ReleaseDate = input.ReleaseDate.Value;
            if (input.PremiereDate.HasValue) entity.PremiereDate = input.PremiereDate.Value;
            if (!string.IsNullOrWhiteSpace(input.Status)) entity.Status = input.Status;
            if (input.Description != null) entity.Description = input.Description;
            if (input.PosterUrl != null) entity.PosterUrl = input.PosterUrl;
            if (input.TrailerUrl != null) entity.TrailerUrl = input.TrailerUrl;
            if (input.StudioId.HasValue && input.StudioId.Value != Guid.Empty)
            {
                entity.StudioId = input.StudioId.Value;
            }
            else if (!string.IsNullOrWhiteSpace(input.StudioName))
            {
                entity.StudioId = await ResolveStudioIdAsync(null, input.StudioName);
            }
            if (input.RatingId.HasValue)
            {
                entity.RatingId = input.RatingId.Value == Guid.Empty ? null : input.RatingId;
            }

            entity.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(entity);

            if (input.GenreListId != null)
            {
                await ReplaceMovieGenresAsync(entity.Id, input.GenreListId);
            }

            if (input.ActorRoles != null)
            {
                await ReplaceMovieActorRolesAsync(entity.Id, input.ActorRoles);
            }
            
            return await BuildMovieOutputAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            if (CurrentUser == null || !CurrentUser.IsInRole("admin"))
            {
                throw new UserFriendlyException("Only admins can perform this action.");
            }

            await _repository.DeleteAsync(id);
        }

        public async Task<MovieOutputDto> BuildMovieOutputAsync(Movie movie)
        {
            var items = await BuildMovieOutputsAsync(new[] { movie });
            return items[0];
        }

        public async Task<MovieDetailOutputDto> BuildMovieDetailOutputAsync(Movie movie)
        {
            var output = await BuildMovieOutputAsync(movie);

            return new MovieDetailOutputDto
            {
                Id = output.Id,
                MovieId = output.MovieId,
                StudioId = output.StudioId,
                StudioName = output.StudioName,
                RatingId = output.RatingId,
                RatingCode = output.RatingCode,
                RatingName = output.RatingName,
                Title = output.Title,
                OriginalTitle = output.OriginalTitle,
                DurationMins = output.DurationMins,
                ReleaseDate = output.ReleaseDate,
                PremiereDate = output.PremiereDate,
                Status = output.Status,
                Description = output.Description,
                PosterUrl = output.PosterUrl,
                TrailerUrl = output.TrailerUrl,
                CreatedAt = output.CreatedAt,
                UpdatedAt = output.UpdatedAt,
                Studio = output.Studio,
                GenreNames = output.GenreNames,
                Genres = output.Genres,
                ActorRoles = output.ActorRoles,
                Cast = output.Cast,
            };
        }

        public async Task<List<MovieOutputDto>> BuildMovieOutputsAsync(IReadOnlyCollection<Movie> movies)
        {
            if (movies.Count == 0)
            {
                return new List<MovieOutputDto>();
            }

            var movieIds = movies.Select(movie => movie.Id).ToList();
            var studioLookup = await LoadStudiosAsync(movies);
            var movieGenres = await LoadMovieGenresAsync(movieIds);
            var genreLookup = await LoadGenresAsync(movieGenres);
            var movieActors = await LoadMovieActorsAsync(movieIds);
            var actorLookup = await LoadActorsAsync(movieActors);
            var movieActorRoles = await LoadMovieActorRolesAsync(movieActors);
            var roleLookup = await LoadRolesAsync(movieActorRoles);
            var ratingLookup = await LoadRatingsAsync(movies);

            var movieGenresByMovieId = movieGenres
                .GroupBy(item => item.MovieId)
                .ToDictionary(group => group.Key, group => group.Select(item => item.GenreId).Distinct().ToList());

            var movieActorsByMovieId = movieActors
                .GroupBy(item => item.MovieId)
                .ToDictionary(group => group.Key, group => group.ToList());

            var movieActorRolesByMovieActorId = movieActorRoles
                .GroupBy(item => item.MovieActorId)
                .ToDictionary(group => group.Key, group => group.ToList());

            return movies
                .Select(movie => new MovieOutputDto
                {
                    Id = movie.Id,
                    MovieId = movie.Id,
                    Title = movie.Title,
                    OriginalTitle = movie.OriginalTitle,
                    DurationMins = movie.DurationMins,
                    ReleaseDate = movie.ReleaseDate,
                    PremiereDate = movie.PremiereDate,
                    Status = movie.Status,
                    Description = movie.Description,
                    PosterUrl = movie.PosterUrl,
                    TrailerUrl = movie.TrailerUrl,
                    StudioId = movie.StudioId,
                    StudioName = movie.StudioId.HasValue && studioLookup.TryGetValue(movie.StudioId.Value, out var studioNameLookup)
                        ? studioNameLookup.Name
                        : null,
                    RatingId = movie.RatingId,
                    RatingCode = movie.RatingId.HasValue && ratingLookup.TryGetValue(movie.RatingId.Value, out var rating)
                        ? rating.Code
                        : null,
                    RatingName = movie.RatingId.HasValue && ratingLookup.TryGetValue(movie.RatingId.Value, out var ratingNameLookup)
                        ? ratingNameLookup.Name
                        : null,
                    CreatedAt = movie.CreatedAt,
                    UpdatedAt = movie.UpdatedAt,
                    Studio = movie.StudioId.HasValue && studioLookup.TryGetValue(movie.StudioId.Value, out var studio)
                        ? MapStudio(studio)
                        : null,
                    GenreNames = BuildGenreNames(movie.Id, movieGenresByMovieId, genreLookup),
                    Genres = BuildGenres(movie.Id, movieGenresByMovieId, genreLookup),
                    ActorRoles = BuildActorRoleNames(movie.Id, movieActorsByMovieId, actorLookup, movieActorRolesByMovieActorId, roleLookup),
                    Cast = BuildCast(movie.Id, movieActorsByMovieId, actorLookup, movieActorRolesByMovieActorId, roleLookup),
                })
                .ToList();
        }

        private async Task<Dictionary<Guid, Studio>> LoadStudiosAsync(IReadOnlyCollection<Movie> movies)
        {
            var studioIds = movies
                .Where(movie => movie.StudioId.HasValue)
                .Select(movie => movie.StudioId!.Value)
                .Distinct()
                .ToList();

            if (studioIds.Count == 0)
            {
                return new Dictionary<Guid, Studio>();
            }

            var query = await _studioRepository.GetQueryableAsync();
            var studios = await AsyncExecuter.ToListAsync(query.Where(item => studioIds.Contains(item.Id)));
            return studios.ToDictionary(item => item.Id, item => item);
        }

        private async Task<List<MovieGenre>> LoadMovieGenresAsync(IReadOnlyCollection<Guid> movieIds)
        {
            if (movieIds.Count == 0)
            {
                return new List<MovieGenre>();
            }

            var query = await _movieGenreRepository.GetQueryableAsync();
            return await AsyncExecuter.ToListAsync(query.Where(item => movieIds.Contains(item.MovieId)));
        }

        private async Task CreateMovieGenresAsync(Guid movieId, IReadOnlyCollection<Guid>? genreIds)
        {
            if (genreIds == null || genreIds.Count == 0)
            {
                return;
            }

            var normalizedGenreIds = genreIds
                .Where(genreId => genreId != Guid.Empty)
                .Distinct()
                .ToList();

            if (normalizedGenreIds.Count == 0)
            {
                return;
            }

            var newMovieGenres = normalizedGenreIds
                .Select(genreId => new MovieGenre
                {
                    MovieId = movieId,
                    GenreId = genreId,
                    TenantId = CurrentTenant.Id,
                })
                .ToList();

            await _movieGenreRepository.InsertManyAsync(newMovieGenres, autoSave: true);
        }

        private async Task ReplaceMovieGenresAsync(Guid movieId, IReadOnlyCollection<Guid>? genreIds)
        {
            var query = await _movieGenreRepository.GetQueryableAsync();
            var existingMovieGenres = await AsyncExecuter.ToListAsync(query.Where(item => item.MovieId == movieId));

            if (existingMovieGenres.Count > 0)
            {
                await _movieGenreRepository.DeleteManyAsync(existingMovieGenres);
            }

            if (genreIds == null || genreIds.Count == 0)
            {
                return;
            }

            await CreateMovieGenresAsync(movieId, genreIds);
        }

        private async Task<Guid> ResolveStudioIdAsync(Guid? studioId, string? studioName)
        {
            if (studioId.HasValue && studioId.Value != Guid.Empty)
            {
                return studioId.Value;
            }

            if (string.IsNullOrWhiteSpace(studioName))
            {
                throw new UserFriendlyException("Studio is required.");
            }

            var normalizedStudioName = studioName.Trim();
            var query = await _studioRepository.GetQueryableAsync();
            var existingStudio = await AsyncExecuter.FirstOrDefaultAsync(query.Where(item => item.Name == normalizedStudioName));

            if (existingStudio != null)
            {
                return existingStudio.Id;
            }

            var newStudio = new Studio
            {
                Name = normalizedStudioName,
                TenantId = CurrentTenant.Id,
            };

            await _studioRepository.InsertAsync(newStudio);
            return newStudio.Id;
        }

        private async Task CreateMovieActorRolesAsync(Guid movieId, IReadOnlyCollection<CreateMovieActorRoleInputDto>? actorRoles)
        {
            if (actorRoles == null || actorRoles.Count == 0)
            {
                return;
            }

            var normalizedActorRoles = actorRoles
                .Where(item => !string.IsNullOrWhiteSpace(item.ActorName) && !string.IsNullOrWhiteSpace(item.RoleName))
                .Select(item => new CreateMovieActorRoleInputDto
                {
                    ActorName = item.ActorName.Trim(),
                    RoleName = item.RoleName.Trim(),
                })
                .DistinctBy(item => (item.ActorName, item.RoleName))
                .ToList();

            if (normalizedActorRoles.Count == 0)
            {
                return;
            }

            foreach (var actorRole in normalizedActorRoles)
            {
                var actor = await ResolveActorAsync(actorRole.ActorName);
                var role = await ResolveMovieRoleAsync(actorRole.RoleName);

                var movieActor = new MovieActor
                {
                    MovieId = movieId,
                    ActorId = actor.Id,
                    TenantId = CurrentTenant.Id,
                };

                await _movieActorRepository.InsertAsync(movieActor);

                var movieActorRole = new MovieActorRole
                {
                    MovieActorId = movieActor.Id,
                    RoleId = role.Id,
                    TenantId = CurrentTenant.Id,
                };

                await _movieActorRoleRepository.InsertAsync(movieActorRole);
            }
        }

        private async Task ReplaceMovieActorRolesAsync(Guid movieId, IReadOnlyCollection<CreateMovieActorRoleInputDto>? actorRoles)
        {
            var query = await _movieActorRepository.GetQueryableAsync();
            var existingMovieActors = await AsyncExecuter.ToListAsync(query.Where(item => item.MovieId == movieId));

            if (existingMovieActors.Count > 0)
            {
                var movieActorIds = existingMovieActors.Select(item => item.Id).ToList();
                var movieActorRoleQuery = await _movieActorRoleRepository.GetQueryableAsync();
                var existingMovieActorRoles = await AsyncExecuter.ToListAsync(movieActorRoleQuery.Where(item => movieActorIds.Contains(item.MovieActorId)));

                if (existingMovieActorRoles.Count > 0)
                {
                    await _movieActorRoleRepository.DeleteManyAsync(existingMovieActorRoles);
                }

                await _movieActorRepository.DeleteManyAsync(existingMovieActors);
            }

            await CreateMovieActorRolesAsync(movieId, actorRoles);
        }

        private async Task<Actor> ResolveActorAsync(string actorName)
        {
            var query = await _actorRepository.GetQueryableAsync();
            var existingActor = await AsyncExecuter.FirstOrDefaultAsync(query.Where(item => item.Name == actorName));

            if (existingActor != null)
            {
                return existingActor;
            }

            var actor = new Actor
            {
                Name = actorName,
                TenantId = CurrentTenant.Id,
            };

            await _actorRepository.InsertAsync(actor);
            return actor;
        }

        private async Task<MovieRole> ResolveMovieRoleAsync(string roleName)
        {
            var query = await _movieRoleRepository.GetQueryableAsync();
            var existingRole = await AsyncExecuter.FirstOrDefaultAsync(query.Where(item => item.Name == roleName));

            if (existingRole != null)
            {
                return existingRole;
            }

            var role = new MovieRole
            {
                Name = roleName,
                TenantId = CurrentTenant.Id,
            };

            await _movieRoleRepository.InsertAsync(role);
            return role;
        }

        private async Task<Dictionary<Guid, Genre>> LoadGenresAsync(IReadOnlyCollection<MovieGenre> movieGenres)
        {
            var genreIds = movieGenres.Select(item => item.GenreId).Distinct().ToList();
            if (genreIds.Count == 0)
            {
                return new Dictionary<Guid, Genre>();
            }

            var query = await _genreRepository.GetQueryableAsync();
            var genres = await AsyncExecuter.ToListAsync(query.Where(item => genreIds.Contains(item.Id)));
            return genres.ToDictionary(item => item.Id, item => item);
        }

        private async Task<List<MovieActor>> LoadMovieActorsAsync(IReadOnlyCollection<Guid> movieIds)
        {
            if (movieIds.Count == 0)
            {
                return new List<MovieActor>();
            }

            var query = await _movieActorRepository.GetQueryableAsync();
            return await AsyncExecuter.ToListAsync(query.Where(item => movieIds.Contains(item.MovieId)));
        }

        private async Task<Dictionary<Guid, Actor>> LoadActorsAsync(IReadOnlyCollection<MovieActor> movieActors)
        {
            var actorIds = movieActors.Select(item => item.ActorId).Distinct().ToList();
            if (actorIds.Count == 0)
            {
                return new Dictionary<Guid, Actor>();
            }

            var query = await _actorRepository.GetQueryableAsync();
            var actors = await AsyncExecuter.ToListAsync(query.Where(item => actorIds.Contains(item.Id)));
            return actors.ToDictionary(item => item.Id, item => item);
        }

        private async Task<List<MovieActorRole>> LoadMovieActorRolesAsync(IReadOnlyCollection<MovieActor> movieActors)
        {
            var movieActorIds = movieActors.Select(item => item.Id).Distinct().ToList();
            if (movieActorIds.Count == 0)
            {
                return new List<MovieActorRole>();
            }

            var query = await _movieActorRoleRepository.GetQueryableAsync();
            return await AsyncExecuter.ToListAsync(query.Where(item => movieActorIds.Contains(item.MovieActorId)));
        }

        private async Task<Dictionary<Guid, MovieRole>> LoadRolesAsync(IReadOnlyCollection<MovieActorRole> movieActorRoles)
        {
            var roleIds = movieActorRoles.Select(item => item.RoleId).Distinct().ToList();
            if (roleIds.Count == 0)
            {
                return new Dictionary<Guid, MovieRole>();
            }

            var query = await _movieRoleRepository.GetQueryableAsync();
            var roles = await AsyncExecuter.ToListAsync(query.Where(item => roleIds.Contains(item.Id)));
            return roles.ToDictionary(item => item.Id, item => item);
        }

        private async Task<Dictionary<Guid, Rating>> LoadRatingsAsync(IReadOnlyCollection<Movie> movies)
        {
            var ratingIds = movies
                .Where(movie => movie.RatingId.HasValue)
                .Select(movie => movie.RatingId!.Value)
                .Distinct()
                .ToList();

            if (ratingIds.Count == 0)
            {
                return new Dictionary<Guid, Rating>();
            }

            var query = await _ratingRepository.GetQueryableAsync();
            var ratings = await AsyncExecuter.ToListAsync(query.Where(item => ratingIds.Contains(item.Id)));
            return ratings.ToDictionary(item => item.Id, item => item);
        }

        private static List<string> BuildGenreNames(Guid movieId, IReadOnlyDictionary<Guid, List<Guid>> movieGenresByMovieId, IReadOnlyDictionary<Guid, Genre> genreLookup)
        {
            if (!movieGenresByMovieId.TryGetValue(movieId, out var genreIds))
            {
                return new List<string>();
            }

            return genreIds
                .Select(genreId => genreLookup.TryGetValue(genreId, out var genre) ? genre.Name : null)
                .Where(name => !string.IsNullOrWhiteSpace(name))
                .Select(name => name!)
                .ToList();
        }

        private static List<MovieCastNameOutputDto> BuildActorRoleNames(
            Guid movieId,
            IReadOnlyDictionary<Guid, List<MovieActor>> movieActorsByMovieId,
            IReadOnlyDictionary<Guid, Actor> actorLookup,
            IReadOnlyDictionary<Guid, List<MovieActorRole>> movieActorRolesByMovieActorId,
            IReadOnlyDictionary<Guid, MovieRole> roleLookup)
        {
            if (!movieActorsByMovieId.TryGetValue(movieId, out var movieActors))
            {
                return new List<MovieCastNameOutputDto>();
            }

            var castNames = new List<MovieCastNameOutputDto>();

            foreach (var movieActor in movieActors)
            {
                var actorName = actorLookup.TryGetValue(movieActor.ActorId, out var actor)
                    ? actor.Name
                    : string.Empty;

                if (!movieActorRolesByMovieActorId.TryGetValue(movieActor.Id, out var movieActorRoles))
                {
                    castNames.Add(new MovieCastNameOutputDto
                    {
                        ActorName = actorName,
                        RoleName = string.Empty,
                    });
                    continue;
                }

                castNames.AddRange(movieActorRoles.Select(movieActorRole => new MovieCastNameOutputDto
                {
                    ActorName = actorName,
                    RoleName = roleLookup.TryGetValue(movieActorRole.RoleId, out var role)
                        ? role.Name
                        : string.Empty,
                }));
            }

            return castNames;
        }

        private static List<GenreOutputDto> BuildGenres(Guid movieId, IReadOnlyDictionary<Guid, List<Guid>> movieGenresByMovieId, IReadOnlyDictionary<Guid, Genre> genreLookup)
        {
            if (!movieGenresByMovieId.TryGetValue(movieId, out var genreIds))
            {
                return new List<GenreOutputDto>();
            }

            return genreIds
                .Select(genreId => genreLookup.TryGetValue(genreId, out var genre)
                    ? MapGenre(genre)
                    : null)
                .Where(genre => genre != null)
                .Select(genre => genre!)
                .ToList();
        }

        private static List<MovieCastOutputDto> BuildCast(
            Guid movieId,
            IReadOnlyDictionary<Guid, List<MovieActor>> movieActorsByMovieId,
            IReadOnlyDictionary<Guid, Actor> actorLookup,
            IReadOnlyDictionary<Guid, List<MovieActorRole>> movieActorRolesByMovieActorId,
            IReadOnlyDictionary<Guid, MovieRole> roleLookup)
        {
            if (!movieActorsByMovieId.TryGetValue(movieId, out var movieActors))
            {
                return new List<MovieCastOutputDto>();
            }

            var cast = new List<MovieCastOutputDto>();

            foreach (var movieActor in movieActors)
            {
                var actorOutput = actorLookup.TryGetValue(movieActor.ActorId, out var actor)
                    ? MapActor(actor)
                    : new ActorOutputDto { Id = movieActor.ActorId, Name = string.Empty };

                if (!movieActorRolesByMovieActorId.TryGetValue(movieActor.Id, out var movieActorRoles))
                {
                    cast.Add(new MovieCastOutputDto
                    {
                        Actor = actorOutput,
                        Role = null,
                        CharacterName = null,
                        ActorName = actorOutput.Name,
                        RoleName = null,
                    });
                    continue;
                }

                cast.AddRange(movieActorRoles.Select(movieActorRole => new MovieCastOutputDto
                {
                    Actor = actorOutput,
                    Role = roleLookup.TryGetValue(movieActorRole.RoleId, out var roleEntity)
                        ? MapRole(roleEntity)
                        : new RoleOutputDto { Id = movieActorRole.RoleId, Name = string.Empty },
                    CharacterName = movieActorRole.CharacterName,
                    ActorName = actorOutput.Name,
                    RoleName = roleLookup.TryGetValue(movieActorRole.RoleId, out var roleEntityForName)
                        ? roleEntityForName.Name
                        : string.Empty,
                }));
            }

            return cast;
        }

        private static StudioOutputDto MapStudio(Studio studio)
        {
            return new StudioOutputDto
            {
                Id = studio.Id,
                Name = studio.Name,
            };
        }

        private static GenreOutputDto MapGenre(Genre genre)
        {
            return new GenreOutputDto
            {
                Id = genre.Id,
                Name = genre.Name,
            };
        }

        private static ActorOutputDto MapActor(Actor actor)
        {
            return new ActorOutputDto
            {
                Id = actor.Id,
                Name = actor.Name,
            };
        }

        private static RoleOutputDto MapRole(MovieRole role)
        {
            return new RoleOutputDto
            {
                Id = role.Id,
                Name = role.Name,
            };
        }
    }
}
