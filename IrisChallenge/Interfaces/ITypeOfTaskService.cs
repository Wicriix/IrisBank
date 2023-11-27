using Iris.Data;

namespace IrisChallenge.Interfaces
{
    public interface ITypeOfTaskService
    {
        Task<IEnumerable<TypeOfTask>> GetTypeOfTaskbyUser(int userId);
        Task<TypeOfTask> SetTypeOfTaskbyUser(int userId, string name);
        Task<TypeOfTask> UpdateTypeOfTaskbyUser(int id, int userId, string name);
        Task<TypeOfTask> DeleteTypeOfTaskbyUser(int id, int userId);
    }
}
