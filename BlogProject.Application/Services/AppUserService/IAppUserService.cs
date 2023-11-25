using BlogProject.Application.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Application.Services.AppUserService
{
    public interface IAppUserService
    {
        Task<IdentityResult> Register(RegisterDTO model);
        Task<SignInResult> Login(LoginDTO model);
        Task<UpdateProfileDTO> GetByUserName(string userName);
        Task UpdateUser(UpdateProfileDTO model);
        Task<List<UpdateProfileDTO>> GetUsers();
        Task<UpdateProfileDTO> GetUserByID(int id);
        Task DeleteUser(int id);
        Task Logout();
    }
}
