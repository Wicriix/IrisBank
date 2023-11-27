using Iris.Commons.Storage.Repository;

namespace Iris.Data;

public partial class User:EntityBase
{
    public int IdUser { get; set; }

    public string FirtsName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public bool? Enabled { get; set; }

    public bool Eliminated { get; set; }

    public virtual ICollection<TaskToDo> TaskToDos { get; set; } = new List<TaskToDo>();

    public virtual ICollection<TypeOfTask> TypeOfTasks { get; set; } = new List<TypeOfTask>();
}
