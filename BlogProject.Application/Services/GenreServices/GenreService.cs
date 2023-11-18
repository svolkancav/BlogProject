using BlogProject.Application.Models.DTOs;
using BlogProject.Domain.Entities;
using BlogProject.Domain.Enum;
using BlogProject.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Application.Services.GenreServices
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;


        public GenreService(IGenreRepository repository)
        {
            _genreRepository = repository;
        }

        public async Task Register(UpdateGenreDTO model)
        {
            Genre genre = new Genre()
            {
                Name = model.Name
            };
            await _genreRepository.Create(genre);
        }

        public async Task Delete(int id)
        {
            Genre genre = await _genreRepository.GetDefault(x => x.Equals(id));
            genre.DeleteDate = DateTime.Now;
            genre.Status = Status.Passive;
            await _genreRepository.Delete(genre);
        }

        public async Task<List<UpdateGenreDTO>> GetGenres()
        {
            var genre = await _genreRepository.GetFilteredList(
                select: x => new UpdateGenreDTO
                {
                    ID = x.ID,
                    Name = x.Name
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.Name)

                );
            return genre;
        }

        public async Task<UpdateGenreDTO> GetByID(int id)
        {
            Genre genre = await _genreRepository.GetDefault(x => x.ID == id);
            UpdateGenreDTO genreDTO = new UpdateGenreDTO()
            {
                Name = genre.Name,
                ID = genre.ID
            };
            return genreDTO;
        }

        public async Task Update(UpdateGenreDTO model)
        {
            Genre genre = await _genreRepository.GetDefault(x => x.ID == model.ID);

            if (genre != null)
            {
                await _genreRepository.Update(genre);
            }
        }
    }
}
