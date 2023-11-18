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
    public class PostConfig : BaseEntityConfig<Post>
    {
        public override void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x=>x.ImagePath).IsRequired(false);
            builder.Property(x=>x.Title).IsRequired();
            builder.HasOne(x => x.Author)
                .WithMany(x => x.Posts)
                .HasForeignKey(x => x.AuthorID)
                .OnDelete(DeleteBehavior.Restrict);
            base.Configure(builder);
        }
    }
}
