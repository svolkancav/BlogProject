using BlogProject.Application.Models.DTOs;
using BlogProject.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Application.Services.GenreServices
{

    public interface IGenreService
    {
        Task Register(UpdateGenreDTO model);
        Task Update(UpdateGenreDTO model);
        Task Delete(int id);
        Task<UpdateGenreDTO> GetByID(int id);
        Task<List<UpdateGenreDTO>> GetGenres();
    }
}
