using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Iris.Data.EntityMapping
{
    public class TypeOfTaskMapping : IEntityTypeConfiguration<TypeOfTask>
    {
        public void Configure(EntityTypeBuilder<TypeOfTask> entity)
        {

            entity.HasKey(e => e.IdTypeOfTask).HasName("TypeOfTask_PK");

            entity.ToTable("TypeOfTask");

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.TypeOfTasks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("TypeOfTask_User_FK");
        }
    }
}
