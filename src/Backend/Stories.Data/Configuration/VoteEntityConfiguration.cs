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
            


            builder.HasOne(v => v.User)
                  .WithMany()
                  .HasForeignKey(v => v.UserId)
                  .HasConstraintName("FK_Vote_User");

            builder.Property(v => v.StoryId)
                   .IsRequired();
            
            builder.HasOne(v => v.Story)
                  .WithMany()
                  .HasForeignKey(v => v.StoryId)
                  .HasConstraintName("FK_Vote_Story");
                   


        }
    }
}