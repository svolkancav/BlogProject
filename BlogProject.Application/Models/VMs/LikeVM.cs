using BlogProject.Domain.Entities;
using BlogProject.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Application.Models.VMs
{
    public class LikeVM
    {
        public int ID { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }

        public string AppUserName { get; set; }
        public int AppUserID { get; set; }
        public AppUser AppUser { get; set; }
        public string PostTitle { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
