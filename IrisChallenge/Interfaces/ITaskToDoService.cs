using Iris.Data;

namespace IrisChallenge.Interfaces
{
    public interface ITaskToDoService
    {
        Task<IEnumerable<TaskToDo>> GetTaskToDobyUser(int userId);
        Task<TaskToDo> SetTaskToDobyUser(int idtypetask, int userId, string text);
        Task<TaskToDo> UpdateTaskToDobyUser(int id, int userId, string text, bool done);
        Task<TaskToDo> DeleteTaskToDobyUser(int id, int userId);
    }
}
