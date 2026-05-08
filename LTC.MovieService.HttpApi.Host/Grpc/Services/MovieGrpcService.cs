using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using LTC.MovieService.Entities;
using Volo.Abp.Domain.Repositories;

namespace LTC.MovieService.Grpc;

public class MovieGrpcService : MovieGrpc.MovieGrpcBase
{
    private readonly IRepository<Movie, Guid> _movieRepository;
    private readonly IRepository<MovieDistribution, Guid> _distributionRepository;
    private readonly IRepository<Rating, Guid> _ratingRepository;
    private readonly IRepository<Genre, Guid> _genreRepository;
    private readonly IRepository<Format, Guid> _formatRepository;
    private readonly IRepository<Actor, Guid> _actorRepository;
    private readonly IRepository<Studio, Guid> _studioRepository;

    public MovieGrpcService(
        IRepository<Movie, Guid> movieRepository,
        IRepository<MovieDistribution, Guid> distributionRepository,
        IRepository<Rating, Guid> ratingRepository,
        IRepository<Genre, Guid> genreRepository,
        IRepository<Format, Guid> formatRepository,
        IRepository<Actor, Guid> actorRepository,
        IRepository<Studio, Guid> studioRepository)
    {
        _movieRepository = movieRepository;
        _distributionRepository = distributionRepository;
        _ratingRepository = ratingRepository;
        _genreRepository = genreRepository;
        _formatRepository = formatRepository;
        _actorRepository = actorRepository;
        _studioRepository = studioRepository;
    }

    public override async Task<GetPaginatedMoviesResponse> GetPaginatedMovies(GetPaginatedMoviesRequest request, ServerCallContext context)
    {
        var queryable = await _movieRepository.GetQueryableAsync();
        if (!string.IsNullOrWhiteSpace(request.Filter))
        {
            queryable = queryable.Where(x => x.Title.Contains(request.Filter) || x.OriginalTitle.Contains(request.Filter));
        }

        var totalCount = await _movieRepository.GetCountAsync();
        var movies = await _movieRepository.GetPagedListAsync(
            request.SkipCount,
            request.MaxResultCount,
            string.IsNullOrWhiteSpace(request.Sorting) ? "Title" : request.Sorting
        );

        var response = new GetPaginatedMoviesResponse
        {
            TotalCount = totalCount
        };

        foreach (var movie in movies)
        {
            response.Items.Add(MapToMessage(movie));
        }

        return response;
    }

    public override async Task<MovieMessage> GetMovieById(GetMovieByIdRequest request, ServerCallContext context)
    {
        if (!Guid.TryParse(request.Id, out var id))
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Movie ID"));
        }

        var movie = await _movieRepository.GetAsync(id);
        return MapToMessage(movie);
    }

    public override async Task<MoviesByDistributionIdsResponse> GetMoviesByDistributionIds(GetMoviesByDistributionIdsRequest request, ServerCallContext context)
    {
        var distributionIds = request.DistributionIds
            .Select(x => Guid.TryParse(x, out var id) ? id : (Guid?)null)
            .Where(x => x.HasValue)
            .Select(x => x!.Value)
            .Distinct()
            .ToList();

        var response = new MoviesByDistributionIdsResponse();
        if (distributionIds.Count == 0)
        {
            return response;
        }

        var distributions = await _distributionRepository.GetListAsync(x => distributionIds.Contains(x.Id));
        var movieIds = distributions.Select(x => x.MovieId).Distinct().ToList();
        var movies = movieIds.Count > 0
            ? await _movieRepository.GetListAsync(x => movieIds.Contains(x.Id))
            : new List<Movie>();

        var ratingIds = movies
            .Where(x => x.RatingId.HasValue)
            .Select(x => x.RatingId!.Value)
            .Distinct()
            .ToList();
        var ratings = ratingIds.Count > 0
            ? await _ratingRepository.GetListAsync(x => ratingIds.Contains(x.Id))
            : new List<Rating>();

        var movieById = movies.ToDictionary(x => x.Id, x => x);
        var ratingCodeById = ratings.ToDictionary(x => x.Id, x => x.Code ?? string.Empty);

        foreach (var distribution in distributions)
        {
            if (!movieById.TryGetValue(distribution.MovieId, out var movie))
            {
                continue;
            }

            var ratingCode = string.Empty;
            if (movie.RatingId.HasValue)
            {
                ratingCodeById.TryGetValue(movie.RatingId.Value, out ratingCode);
            }

            response.Items.Add(new DistributionMovieMessage
            {
                DistributionId = distribution.Id.ToString(),
                MovieId = movie.Id.ToString(),
                Title = movie.Title ?? string.Empty,
                OriginalTitle = movie.OriginalTitle ?? string.Empty,
                PosterUrl = movie.PosterUrl ?? string.Empty,
                DurationMins = movie.DurationMins ?? 0,
                RatingCode = ratingCode ?? string.Empty
            });
        }

        return response;
    }

    public override async Task<GenresResponse> GetAllGenres(Empty request, ServerCallContext context)
    {
        var genres = await _genreRepository.GetListAsync();
        var response = new GenresResponse();
        foreach (var genre in genres)
        {
            response.Items.Add(new GenreMessage { Id = genre.Id.ToString(), Name = genre.Name });
        }
        return response;
    }

    public override async Task<FormatsResponse> GetAllFormats(Empty request, ServerCallContext context)
    {
        var formats = await _formatRepository.GetListAsync();
        var response = new FormatsResponse();
        foreach (var format in formats)
        {
            response.Items.Add(new FormatMessage { Id = format.Id.ToString(), Name = format.Name });
        }
        return response;
    }

    public override async Task<ActorsResponse> GetAllActors(Empty request, ServerCallContext context)
    {
        var actors = await _actorRepository.GetListAsync();
        var response = new ActorsResponse();
        foreach (var actor in actors)
        {
            response.Items.Add(new ActorMessage { Id = actor.Id.ToString(), Name = actor.Name, Bio = actor.Bio ?? string.Empty });
        }
        return response;
    }

    public override async Task<StudiosResponse> GetAllStudios(Empty request, ServerCallContext context)
    {
        var studios = await _studioRepository.GetListAsync();
        var response = new StudiosResponse();
        foreach (var studio in studios)
        {
            response.Items.Add(new StudioMessage { Id = studio.Id.ToString(), Name = studio.Name });
        }
        return response;
    }

    private MovieMessage MapToMessage(Movie movie)
    {
        return new MovieMessage
        {
            Id = movie.Id.ToString(),
            StudioId = movie.StudioId?.ToString() ?? string.Empty,
            RatingId = movie.RatingId?.ToString() ?? string.Empty,
            Title = movie.Title ?? string.Empty,
            OriginalTitle = movie.OriginalTitle ?? string.Empty,
            DurationMins = movie.DurationMins ?? 0,
            ReleaseDate = movie.ReleaseDate?.ToString("o") ?? string.Empty,
            PremiereDate = movie.PremiereDate?.ToString("o") ?? string.Empty,
            Status = movie.Status ?? string.Empty,
            Description = movie.Description ?? string.Empty,
            PosterUrl = movie.PosterUrl ?? string.Empty,
            TrailerUrl = movie.TrailerUrl ?? string.Empty,
            CreatedAt = movie.CreatedAt?.ToString("o") ?? string.Empty,
            UpdatedAt = movie.UpdatedAt?.ToString("o") ?? string.Empty
        };
    }
}