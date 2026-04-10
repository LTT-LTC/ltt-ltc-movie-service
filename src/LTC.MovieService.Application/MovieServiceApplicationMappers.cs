using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace LTC.MovieService;

[Mapper]
public partial class MovieServiceApplicationMappers
{
    /* You can configure your Mapperly mapping configuration here.
     * Alternatively, you can split your mapping configurations
     * into multiple mapper classes for a better organization. */
     
    public partial Dto.MovieOutputDto MapToMovieOutputDto(Entities.Movie movie);
    public partial System.Collections.Generic.List<Dto.MovieOutputDto> MapToMovieOutputDtoList(System.Collections.Generic.List<Entities.Movie> movies);
}
