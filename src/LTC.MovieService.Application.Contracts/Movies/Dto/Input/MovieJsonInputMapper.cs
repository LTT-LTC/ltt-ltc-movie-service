namespace LTC.MovieService.Dtos.Input
{
    public static class MovieJsonInputMapper
    {
        public static CreateMovieInputDto ToCreateMovieInputDto(CreateMovieJsonInputDto input)
        {
            return new CreateMovieInputDto
            {
                Id = input.Id,
                StudioId = input.StudioId,
                StudioName = input.StudioName,
                RatingId = input.RatingId,
                RatingNumber = input.RatingNumber,
                GenreListId = input.GenreListId,
                ActorRoles = input.ActorRoles,
                Title = input.Title,
                OriginalTitle = input.OriginalTitle,
                DurationMins = input.DurationMins,
                ReleaseDate = input.ReleaseDate,
                PremiereDate = input.PremiereDate,
                Status = input.Status,
                Description = input.Description,
                PosterUrl = input.PosterUrl,
                TrailerUrl = input.TrailerUrl
            };
        }

        public static UpdateMovieInputDto ToUpdateMovieInputDto(UpdateMovieJsonInputDto input)
        {
            return new UpdateMovieInputDto
            {
                Id = input.Id,
                StudioId = input.StudioId,
                StudioName = input.StudioName,
                RatingId = input.RatingId,
                RatingNumber = input.RatingNumber,
                GenreListId = input.GenreListId,
                ActorRoles = input.ActorRoles,
                Title = input.Title,
                OriginalTitle = input.OriginalTitle,
                DurationMins = input.DurationMins,
                ReleaseDate = input.ReleaseDate,
                PremiereDate = input.PremiereDate,
                Status = input.Status,
                Description = input.Description,
                PosterUrl = input.PosterUrl,
                TrailerUrl = input.TrailerUrl
            };
        }
    }
}
