using BlogProject.Application.Models.VMs;
using BlogProject.Domain.Entities;
using BlogProject.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Application.Models.DTOs
{
    public class LikePostDTO
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public AppUser User { get; set; }
        public DateTime CreateDate => DateTime.Now;
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
