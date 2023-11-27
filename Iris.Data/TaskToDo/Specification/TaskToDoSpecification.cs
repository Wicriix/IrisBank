using Iris.Commons.Storage.Repository;

namespace Iris.Data.Specification
{
    public class TaskToDoSpecification : BaseSpecification<TaskToDo>
    {
        public TaskToDoSpecification(int uid) : base(f => f.UserId.Equals(uid))
        {
            Includes.Add(x => x.TypeOfTask);
        }
    }
}