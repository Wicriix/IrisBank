using Iris.Data;
using Iris.Shared.CommonsModels;
using Iris.Shared.InputModels;

namespace IrisChallenge.Interfaces
{
    public interface IAuthServices
    {
        Task<List<User>> Authentication(UserInputModel userInputModel);
        string createJWT(UserAuthModel user);
        bool ValidateJWT(string token);
        (bool, string) getUserIdfromToken(HttpContext httpContext);

    }
}
