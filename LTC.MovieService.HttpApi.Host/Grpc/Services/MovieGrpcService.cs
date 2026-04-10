using System;
using System.Threading.Tasks;
using Grpc.Core;
using LTC.MovieService.Entities;
using Volo.Abp.Domain.Repositories;

namespace LTC.MovieService.Grpc;

public class MovieGrpcService : MovieGrpc.MovieGrpcBase
{
    private readonly IRepository<Movie, Guid> _movieRepository;

    public MovieGrpcService(IRepository<Movie, Guid> movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public override async Task<GetPaginatedMoviesResponse> GetPaginatedMovies(GetPaginatedMoviesRequest request, ServerCallContext context)
    {
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
            response.Items.Add(new MovieMessage
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
            });
        }

        return response;
    }
}