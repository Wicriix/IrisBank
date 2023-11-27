using AutoMapper;
using Iris.Data.Repository;
using Iris.Data.Specification;
using Iris.Data;
using Iris.Shared.InputModels;
using IrisChallenge.Interfaces;

namespace IrisChallenge.Services
{
    public class UserService:IUserServices
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> SearchUserbyEmailAndPassword(UserInputModel userInputModel)
        {
            var passwordHashed = MD5Hasher.CalculateMD5Hash(userInputModel.password);
            var users = (await _userRepository.ListSelectAsync(new UserSpecification(userInputModel.email, passwordHashed), x => x)).ToList();
            return users;
        }
    }
}
