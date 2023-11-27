using Iris.Data;
using Iris.Data.Repository;
using Iris.Data.Specification;
using IrisChallenge.Interfaces;

namespace IrisChallenge.Services
{
    public class TypeOfTaskService : ITypeOfTaskService
    {
        private readonly ITypeOfTaskRepository _typeOfTaskRepository;
        public TypeOfTaskService(ITypeOfTaskRepository typeOfTaskRepository)
        {
            _typeOfTaskRepository = typeOfTaskRepository;
        }

        public async Task<IEnumerable<TypeOfTask>> GetTypeOfTaskbyUser(int userId)
        {
            return await _typeOfTaskRepository.ListAsync(new TypeOfTaskSpecification(userId));
        }

        public async Task<TypeOfTask> SetTypeOfTaskbyUser(int userId, string name)
        {
            TypeOfTask typeOfTask = new TypeOfTask
            {
                UserId = userId,
                Name = name
            };
            return await _typeOfTaskRepository.AddAsync(typeOfTask);
        }

        public async Task<TypeOfTask> UpdateTypeOfTaskbyUser(int id, int userId, string name)
        {
            TypeOfTask typeOfTask = await _typeOfTaskRepository.GetByIdAsync(id);
            typeOfTask.Name = name;

            if (typeOfTask.UserId    != userId)
            {
                throw new Exception("datos no coinciden.");
            }

            return await _typeOfTaskRepository.UpdateAsync(typeOfTask);
        }

        public async Task<TypeOfTask> DeleteTypeOfTaskbyUser(int id, int userId)
        {
            TypeOfTask typeOfTask = await _typeOfTaskRepository.GetByIdAsync(id);

            if (typeOfTask.UserId != userId)
            {
                throw new Exception("datos no coinciden.");
            }
            return await _typeOfTaskRepository.DeleteAsync(typeOfTask);
        }
    }
}
