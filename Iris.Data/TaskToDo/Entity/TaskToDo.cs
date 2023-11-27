using Iris.Commons.Storage.Repository;

namespace Iris.Data;

public partial class TaskToDo : EntityBase
{
    public int IdTaskToDo { get; set; }

    public int UserId { get; set; }

    public int TypeOfTaskId { get; set; }

    public string TextData { get; set; } = null!;

    public bool Done { get; set; }

    public virtual TypeOfTask TypeOfTask { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
