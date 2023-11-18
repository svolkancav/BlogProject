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
    public class CommentConfig : BaseEntityConfig<Comment>
    {
        public override void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(x => x.ID);
            builder.HasOne(c => c.AppUser)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.AppUserID)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(x => x.Post)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.PostID)
                .OnDelete(DeleteBehavior.Restrict);



            base.Configure(builder);
        }
    }
}
