using BlogProject.Domain.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Domain.Entities
{
    public class AppUser : IdentityUser, IBaseEntity
    {

        [NotMapped]
        public IFormFile UploadPath { get; set; }
        public string ImagePath { get; set; }

        //IBaseEntity
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }

        public List<Comment> Comments { get; set; }
        public List<Like> Likes { get; set; }


    }

}
