﻿using AutoMapper;
using BlogProject.Application.Models.DTOs;
using BlogProject.Application.Models.VMs;
using BlogProject.Domain.Entities;
using BlogProject.Domain.Enum;
using BlogProject.Domain.Repositories;
using BlogProject.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Application.Services.PostServices
{

    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepository, IGenreRepository genreRepository, IAuthorRepository authorRepository, IAppUserRepository appUserRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _genreRepository = genreRepository;
            _authorRepository = authorRepository;
            _appUserRepository = appUserRepository;
            _mapper = mapper;
        }

        public async Task Delete(int id)
        {
            Post post = await _postRepository.GetDefault(x => x.ID == id);
            await _postRepository.Delete(post);
        }

        public async Task<UpdatePostDTO> GetByID(int id)
        {
            Post post = await _postRepository.GetDefault(x => x.ID == id);

            var model = _mapper.Map<UpdatePostDTO>(post);
            model.Authors = await _authorRepository.GetFilteredList(
                    select: x => new AuthorVM()
                    {
                        Id = x.ID,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                    },
                    where: x => x.Status != Status.Passive,
                    orderBy: x => x.OrderBy(x => x.FirstName)

                    );

            model.Genres = await _genreRepository.GetFilteredList(
                    select: x => new GenreVM()
                    {
                        Id = x.ID,
                        Name = x.Name,
                    },
                    where: x => x.Status != Status.Passive,
                    orderBy: x => x.OrderBy(x => x.Name)
                    );

            return model;


            #region MappingOncesi
            //UpdatePostDTO dto = new UpdatePostDTO()
            //{
            //    Authors = await _authorRepository.GetFilteredList(
            //        select: x => new AuthorVM()
            //        {
            //            Id = x.ID,
            //            FirstName = x.FirstName,
            //            LastName = x.LastName,
            //        },
            //        where: x => x.Status != Status.Passive,
            //        orderBy: x => x.OrderBy(x => x.FirstName)

            //        ),
            //    Genres = await _genreRepository.GetFilteredList(
            //        select: x => new GenreVM()
            //        {
            //            Id = x.ID,
            //            Name = x.Name,
            //        },
            //        where: x => x.Status != Status.Passive,
            //        orderBy: x => x.OrderBy(x => x.Name)
            //        )
            //};
            //return dto;
            #endregion



        }

        public Task<List<PostVM>> GetPosts()
        {
            var post = _postRepository.GetFilteredList(
                 select: x => new PostVM()
                 {
                     AuthorFirstName = x.Author.FirstName,
                     AuthorLastName = x.Author.LastName,
                     GenreName = x.Genre.Name,
                     Title = x.Title,
                     ImagePath = x.ImagePath,
                     ID = x.ID

                 },
                 where: x => x.Status == Status.Active,
                 orderBy: x => x.OrderBy(x => x.Title),
                 include: x => x.Include(x => x.Genre).Include(x => x.Author)
                 );
            return post;
        }

        public async Task Register(CreatePostDTO model)
        {
            //Post post = new Post()
            //{
            //    AuthorID = model.AuthorId,
            //    GenreID = model.GenreId,
            //    Content = model.Content,
            //    Title = model.Title

            //};

            Post post = _mapper.Map<Post>(model);

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
            var post = _mapper.Map<Post>(model);
           

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
                    select: x => new AuthorVM()
                    {
                        Id = x.ID,
                        FirstName = x.FirstName,
                        LastName = x.LastName
                    },
                    where: x => x.Status != Status.Passive,
                    orderBy: x => x.OrderBy(x => x.FirstName)
                    )
            };

            return model;

        }



        public async Task<PostDetailsVM> GetPostDetails(int id)
        {
            var post = await _postRepository.GetFilteredFirstOrDefault(
                select: x => new PostDetailsVM()
                {
                    AuthorFirstName = x.Author.FirstName,
                    AuthorLastName = x.Author.LastName,
                    AuthorImagePath = x.Author.ImagePath,
                    Content = x.Content,
                    CreateDate = x.CreateDate,
                    ImagePath = x.ImagePath,
                    Title = x.Title
                },
                where: (x => x.ID == id),
                orderBy: null,
                include: x => x.Include(x => x.Author)

                );
            return post;
        }

    }
}
