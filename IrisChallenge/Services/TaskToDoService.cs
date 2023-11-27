using Iris.Data.Specification;
using Iris.Data;
using IrisChallenge.Interfaces;
using Iris.Data.Repository;

namespace IrisChallenge.Services
{
    public class TaskToDoService : ITaskToDoService
    {
        private readonly ITaskToDoRepository _taskToDoRepository;

        public TaskToDoService(ITaskToDoRepository taskToDoRepository)
        {
            _taskToDoRepository = taskToDoRepository;
        }

        public async Task<IEnumerable<TaskToDo>> GetTaskToDobyUser(int userId)
        {
            return await _taskToDoRepository.ListAsync(new TaskToDoSpecification(userId));
        }

        public async Task<TaskToDo> SetTaskToDobyUser(int idtypetask, int userId, string text)
        {
            TaskToDo taskToDo = new TaskToDo
            {
                UserId = userId,
                TypeOfTaskId = idtypetask,
                TextData = text
            };
            return await _taskToDoRepository.AddAsync(taskToDo);
        }

        public async Task<TaskToDo> UpdateTaskToDobyUser(int id, int userId, string text, bool done = false)
        {
            TaskToDo taskToDo = await _taskToDoRepository.GetByIdAsync(id);
            taskToDo.TextData = (taskToDo.TextData != text) ? text : taskToDo.TextData;
            taskToDo.Done = done;

            if (taskToDo.UserId != userId)
            {
                throw new Exception("datos no coinciden.");
            }

            return await _taskToDoRepository.UpdateAsync(taskToDo);
        }

        public async Task<TaskToDo> DeleteTaskToDobyUser(int id, int userId)
        {
            TaskToDo taskToDo = await _taskToDoRepository.GetByIdAsync(id);

            if (taskToDo.UserId != userId)
            {
                throw new Exception("datos no coinciden.");
            }
            return await _taskToDoRepository.DeleteAsync(taskToDo);
        }
    }
}
