using Iris.Commons.Storage.Repository;
using Iris.Data.DataContex;

namespace Iris.Data.Repository
{
    public class TypeOfTaskRepository : BaseSqlRepository<TypeOfTask>, ITypeOfTaskRepository
    {
        public TypeOfTaskRepository(IrisDbContext irisDbContext) : base(irisDbContext)
        {

        }
    }
}
    