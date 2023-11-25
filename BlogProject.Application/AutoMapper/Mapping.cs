using AutoMapper;
using BlogProject.Application.Models.DTOs;
using BlogProject.Application.Models.VMs;
using BlogProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Application.AutoMapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Post, CreatePostDTO>().ReverseMap();
            CreateMap<Post, UpdatePostDTO>().ReverseMap();
            CreateMap<Post, PostVM>().ReverseMap();
            CreateMap<Post, PostDetailsVM>().ReverseMap();
            CreateMap<Genre, GenreDTO>().ReverseMap();
            CreateMap<Genre, GenreVM>().ReverseMap();
            CreateMap<Author, AuthorDTO>().ReverseMap();
            CreateMap<Author, CreateAuthorDTO>().ReverseMap();
            CreateMap <Author, UpdateAuthorDTO>().ReverseMap();
            CreateMap<Author, AuthorVM>().ReverseMap();
            CreateMap<Like, LikeVM>().ReverseMap();
            

        }
    }
}
