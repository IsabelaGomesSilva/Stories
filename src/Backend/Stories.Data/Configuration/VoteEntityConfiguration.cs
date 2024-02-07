using Microsoft.EntityFrameworkCore;
using Stories.Data.Models;

namespace Stories.Data.Configuration
{
    public class VoteEntityConfiguration : IEntityTypeConfiguration<Vote>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Vote> builder)
        {
            builder.ToTable("Vote");

            builder.Property(v => v.Id)
                   .ValueGeneratedOnAdd();
           
            builder.Property(v => v.Voted)
                   .IsRequired();
            
            builder.Property(v => v.UserId)
                   .IsRequired();

            builder.Property(v => v.StoryId)
                   .IsRequired();

        }
    }
}