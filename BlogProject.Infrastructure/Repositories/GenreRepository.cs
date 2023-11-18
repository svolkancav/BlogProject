using BlogProject.Domain.Entities;
using BlogProject.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Infrastructure.Repositories
{
    public class GenreRepository : BaseRepository<Genre>,IGenreRepository
    {
        public GenreRepository(AppDbContext context) : base(context)
        {
            
        }
    }
}
