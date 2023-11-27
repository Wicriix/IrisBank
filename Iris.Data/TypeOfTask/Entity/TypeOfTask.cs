using Iris.Commons.Storage.Repository;

namespace Iris.Data;

public partial class TypeOfTask : EntityBase
{
    public int? UserId { get; set; }

    public string Name { get; set; } = null!;

    public int IdTypeOfTask { get; set; }

    public virtual User? User { get; set; }
}
