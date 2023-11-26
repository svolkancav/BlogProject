using BlogProject.Application.Models.DTOs;


namespace BlogProject.Application.Services.GenreServices
{

    public interface IGenreService
    {
        Task Register(GenreDTO model);
        Task Update(GenreDTO model);
        Task Delete(int id);
        Task<GenreDTO> GetByID(int id);
        Task<List<GenreDTO>> GetGenres();
        Task<GenreDTO> CreateGenre();
    }
}