using Iris.Data;
using Iris.Shared.InputModels;

namespace IrisChallenge.Interfaces
{
    public interface IUserServices
    {
        public Task<List<User>> SearchUserbyEmailAndPassword(UserInputModel userInputModel);
    }
}
