using AutoMapper;
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
        private readonly IMapper _mapper;


        public GenreService(IGenreRepository repository, IMapper mapper)
        {
            _genreRepository = repository;
            _mapper = mapper;
        }

        public async Task Register(GenreDTO model)
        {
            var genre = _mapper.Map<Genre>(model);
            
            await _genreRepository.Create(genre);
        }

        public async Task Delete(int id)
        {
            Genre genre = await _genreRepository.GetDefault(x => x.ID == id);
            genre.DeleteDate = DateTime.Now;
            genre.Status = Status.Passive;
            await _genreRepository.Delete(genre);
        }

        public async Task<List<GenreDTO>> GetGenres()
        {
            var genre = await _genreRepository.GetFilteredList(
                select: x => new GenreDTO { 
                    ID = x.ID,
                    Name = x.Name
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.Name)

                );
            return genre;
        }

        public async Task<GenreDTO> GetByID(int id)
        {
            Genre genre = await _genreRepository.GetDefault(x => x.ID == id);
            var model = _mapper.Map<GenreDTO>(genre);
           
            return model;
        }

        public async Task Update(GenreDTO model)
        {
            bool isExist = await _genreRepository.Any(x=>x.ID == model.ID);

            if (isExist)
            {
                var genre = _mapper.Map<Genre>(model);
                await _genreRepository.Update(genre);
            }
           
        }

        public async Task<GenreDTO> CreateGenre()
        {
            GenreDTO genreDTO = new GenreDTO();
            return genreDTO;
        }
    }
}
