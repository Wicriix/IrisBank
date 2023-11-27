using Iris.Commons.Storage.Repository;
using Iris.Data;

namespace Iris.Data.Specification
{
    public class TypeOfTaskSpecification : BaseSpecification<TypeOfTask>
    {
        public TypeOfTaskSpecification(int uid):base(f=> f.UserId.Equals(uid) || f.UserId == null)
        {
                
        }
    }
}