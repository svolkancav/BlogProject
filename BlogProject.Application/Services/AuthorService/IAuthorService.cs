using BlogProject.Application.Models.DTOs;
using BlogProject.Application.Models.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Application.Services.AuthorService
{
    public interface IAuthorService
    {
        Task Register(CreateAuthorDTO model);
        Task Update(UpdateAuthorDTO model);
        Task Delete(int id);
        Task<UpdateAuthorDTO> GetByID(int id);
        Task<List<AuthorVM>> GetAuthors();
        Task<CreateAuthorDTO> CreateAuthor();
    }
}
