using Iris.Commons.Storage.Repository;
using Iris.Data.DataContex;

namespace Iris.Data.Repository
{
    public class UserRepository : BaseSqlRepository<User>, IUserRepository
    {
        public UserRepository(IrisDbContext irisDbContext) : base(irisDbContext)
        {

        }
    }
}
    