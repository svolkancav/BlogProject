using BlogProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Infrastructure.EntityTypeConfig
{
    public class GenreConfig : BaseEntityConfig<Genre>
    {
        public override void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.Name).IsRequired();
            builder.HasMany(x => x.Posts)
                .WithOne(x => x.Genre)
                .HasForeignKey(x => x.GenreID)
                .OnDelete(DeleteBehavior.Restrict);


            base.Configure(builder);
        }
    }
}
