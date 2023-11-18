using BlogProject.Domain.Entities;
using BlogProject.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Infrastructure.Repositories
{
    public class PostRepository : BaseRepository<Post>,IPostRepository
    {
        public PostRepository(AppDbContext context) : base(context)
        {

        }
    }
}
