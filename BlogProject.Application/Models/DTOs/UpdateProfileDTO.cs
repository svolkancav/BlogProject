using BlogProject.Application.Extensions;
using BlogProject.Domain.Enum;
using Microsoft.AspNetCore.Http;

namespace BlogProject.Application.Models.DTOs
{
    public class UpdateProfileDTO
    {
        //Todo: DataAnnotaions, Validations
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmedPassword { get; set; }
        public string Email { get; set; }
        public DateTime UpdateDate => DateTime.Now;
        public Status Status => Status.Active;
        public string ImagePath { get; set; }

        [PictureFileExtension]
        public IFormFile UploadPath { get; set; }
    }
}