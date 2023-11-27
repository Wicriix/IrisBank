using Microsoft.EntityFrameworkCore;

namespace Iris.Data.DataContex;

public partial class IrisDbContext : DbContext
{
    public IrisDbContext()
    {
    }

    public IrisDbContext(DbContextOptions<IrisDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TaskToDo> TaskToDos { get; set; }

    public virtual DbSet<TypeOfTask> TypeOfTasks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IrisDbContext).Assembly);
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
