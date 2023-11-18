using BlogProject.Application.Extensions;
using BlogProject.Application.Models.VMs;
using BlogProject.Domain.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Application.Models.DTOs
{
    public class UpdatePostDTO
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Must to type title")]
        [MinLength(3, ErrorMessage = "Minimum Length is 3")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Must to type content")]
        [MinLength(3, ErrorMessage = "Minimum Length is 3")]
        public string Content { get; set; }
        public string ImagePath { get; set; }

        [PictureFileExtension]
        public IFormFile UploadPath { get; set; }
        public DateTime UpdateDate => DateTime.Now;
        public Status Status => Status.Active;

        [Required(ErrorMessage = "Must to type Author")]
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "Must to type Genre")]
        public int GenreId { get; set; }
        public List<GenreVM>? Genres { get; set; }
        public List<AuthorVM>? Authors { get; set; }
    }
}
