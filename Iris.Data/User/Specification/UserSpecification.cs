using Iris.Commons.Storage.Repository;

namespace Iris.Data.Specification
{
    public class UserSpecification: BaseSpecification<User>
    {
        public UserSpecification(string email, string password)
            : base(f => !string.IsNullOrEmpty(email) && f.Email == email
            && !string.IsNullOrEmpty(password) && f.Password == password)
        {

        }
    }
}