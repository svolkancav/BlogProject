using BlogProject.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BlogProject.Infrastructure.EntityTypeConfig
{
    public class AuthorConfig : BaseEntityConfig <Author>
    {
        public override void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x=>x.FirstName).IsRequired();
            builder.Property(x=>x.LastName).IsRequired();
            builder.Property(x=>x.ImagePath).IsRequired(false);

            base.Configure(builder);
        }
    }
}
