using BlogProject.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Domain.Entities
{
    public class Post : IBaseEntity
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }

        public int AuthorID { get; set; }
        public Author Author { get; set; }

        public int GenreID { get; set; }
        public Genre Genre { get; set; }

        public List<Comment> Comments { get; set; }
        public List<Like> Likes { get; set; }


    }
}