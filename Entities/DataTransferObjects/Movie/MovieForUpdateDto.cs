namespace Entities.DataTransferObjects.Movie
{
    public class MovieForUpdateDto : MovieForManipulationDto
    {
        public Guid Id { get; set; }
    }
}
