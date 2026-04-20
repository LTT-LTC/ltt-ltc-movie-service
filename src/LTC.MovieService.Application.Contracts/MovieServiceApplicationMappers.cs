using System.Collections.Generic;
using System.Linq;

namespace LTC.MovieService;

public class MovieServiceApplicationMappers
{
    public Dtos.Output.MovieOutputDto MapToMovieOutputDto(Entities.Movie movie)
    {
        return new Dtos.Output.MovieOutputDto
        {
            Id = movie.Id,
            MovieId = movie.Id,
            StudioId = movie.StudioId,
            RatingId = movie.RatingId,
            Title = movie.Title,
            OriginalTitle = movie.OriginalTitle,
            DurationMins = movie.DurationMins,
            ReleaseDate = movie.ReleaseDate,
            PremiereDate = movie.PremiereDate,
            Status = movie.Status,
            Description = movie.Description,
            PosterUrl = movie.PosterUrl,
            TrailerUrl = movie.TrailerUrl,
            CreatedAt = movie.CreatedAt,
            UpdatedAt = movie.UpdatedAt,
        };
    }

    public List<Dtos.Output.MovieOutputDto> MapToMovieOutputDtoList(List<Entities.Movie> movies)
    {
        return movies.Select(MapToMovieOutputDto).ToList();
    }
}