using Iris.Commons.Storage.Repository;
using Iris.Data.DataContex;

namespace Iris.Data.Repository
{
    public class TaskToDoRepository : BaseSqlRepository<TaskToDo>, ITaskToDoRepository
    {
        public TaskToDoRepository(IrisDbContext irisDbContext) : base(irisDbContext)
        {

        }
    }
}
    