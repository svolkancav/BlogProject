using BlogProject.Application.Models.DTOs;
using BlogProject.Application.Models.VMs;
using BlogProject.Domain.Entities;
using BlogProject.Domain.Enum;
using BlogProject.Domain.Repositories;
using BlogProject.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Application.Services.GenreServices
{

    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IAuthorRepository _authorRepository;

        public PostService(IPostRepository postRepository, IGenreRepository genreRepository, IAuthorRepository authorRepository)
        {
            _postRepository = postRepository;
            _genreRepository = genreRepository;
            _authorRepository = authorRepository;
        }

        public async Task Delete(int id)
        {
            Post post = await _postRepository.GetDefault(x => x.ID == id);
            await _postRepository.Delete(post);
        }

        public async Task<UpdatePostDTO> GetByID(int id)
        {
            Post post = await _postRepository.GetDefault(x => x.ID == id);
            UpdatePostDTO dto = new UpdatePostDTO()
            {
                ID = id,
                Content = post.Content,
                ImagePath = post.ImagePath,
                Title = post.Title

            };
            return dto;
        }

        public Task<List<PostVM>> GetPosts()
        {
            var post = _postRepository.GetFilteredList(
                 select: x => new PostVM()
                 {
                     Content = x.Content,
                     Title = x.Title,


                 },
                 where: x => x.Status == Domain.Enum.Status.Active,
                 orderBy: x => x.OrderBy(x => x.Title)
                 );
            return post;
        }

        public async Task Register(CreatePostDTO model)
        {
            Post post = new Post()
            {
                AuthorID = model.AuthorId,
                GenreID = model.GenreId,
                Content = model.Content,
                Title = model.Title

            };

            //Post'un resmi varsa veritabanına yolu yazılmalı. Server üzerindeki bir klasöre de resmin kendisi eklenmeli.

            if (model.UploadPath != null)
            {
                // using SixLabors.ImageSharp;
                Image image = Image.Load(model.UploadPath.OpenReadStream());
                image.Mutate(x => x.Resize(600, 560));

                Guid guid = new Guid();
                image.Save($"wwwroot/images/{guid}"); //foler'ın altına kaydettim.

                post.ImagePath = $"/images/{guid}.jpg";
            }
            else
                post.ImagePath = $"/images/defaultpost.jpg";

            await _postRepository.Create(post);
        }

        public async Task Update(UpdatePostDTO model)
        {
            Post post = new Post()
            {
                AuthorID = model.AuthorId,
                Content = model.Content,
                ID = model.ID,
                Title = model.Title
            };

            if (model.UploadPath != null)
            {
                // using SixLabors.ImageSharp;
                Image image = Image.Load(model.UploadPath.OpenReadStream());
                image.Mutate(x => x.Resize(600, 560));

                Guid guid = new Guid();
                image.Save($"wwwroot/images/{guid}"); //foler'ın altına kaydettim.

                post.ImagePath = $"/images/{guid}.jpg";
            }
            else
                post.ImagePath = $"/images/defaultpost.jpg";
            await _postRepository.Update(post);
        }
        public async Task<CreatePostDTO> CreatePost()
        {
            CreatePostDTO model = new CreatePostDTO()
            {
                Genres = await _genreRepository.GetFilteredList(
                    select: x => new GenreVM()
                    {
                        Id = x.ID,
                        Name = x.Name
                    },
                    where: x => x.Status != Status.Passive,
                    orderBy: x => x.OrderBy(x => x.Name)
                    ),
                Authors = await _authorRepository.GetFilteredList(
                    select: x=> new AuthorVM()
                    {
                        Id = x.ID,
                        FirstName = x.FirstName,
                        LastName = x.LastName
                    },
                    where: x=> x.Status != Status.Passive,
                    orderBy: x=>x.OrderBy(x=>x.FirstName)
                    )
            };

            return model;
            
        }
    }
}
