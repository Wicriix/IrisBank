using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Iris.Data.EntityMapping
{
    public class TaskToDoMapping : IEntityTypeConfiguration<TaskToDo>
    {
        public void Configure(EntityTypeBuilder<TaskToDo> entity)
        {
            entity.HasKey(e => e.IdTaskToDo).HasName("TaskToDo_PK");

            entity.ToTable("TaskToDo");

            entity.Property(e => e.TextData)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.TaskToDos)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TaskToDo_User_FK");
        }
    }
}
