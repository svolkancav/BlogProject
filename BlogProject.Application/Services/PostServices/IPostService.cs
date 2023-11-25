using BlogProject.Application.Models.DTOs;
using BlogProject.Application.Models.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Application.Services.PostServices
{
    public interface IPostService
    {
        Task Register(CreatePostDTO model);
        Task Delete(int id);
        Task Update(UpdatePostDTO model);
        Task<UpdatePostDTO> GetByID(int id);
        Task<List<PostVM>> GetPosts();
        Task<CreatePostDTO> CreatePost();

        Task<PostDetailsVM> GetPostDetails(int id);

    }
}
