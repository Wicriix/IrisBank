using AutoMapper;
using Iris.Commons.Library.Base;
using Iris.Shared.CommonsModels;
using Iris.Shared.InputModels;
using Iris.Shared.OutputModel;
using IrisChallenge.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IrisChallenge.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authServices;
        private readonly IMapper _mapper;
        public AuthController(IAuthServices authServices, IMapper mapper)
        {
            _authServices = authServices;
            _mapper = mapper;
        }

        [HttpPost("aunthenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UserInputModel userInputModel)
        {
            if (userInputModel == null)
                return BadRequest();

            var countUser = await _authServices.Authentication(userInputModel);
            if (countUser.Count == 0)
            {
                return Ok(new GenericResponse
                {
                    OperationSuccess = false,
                    ErrorMessage = "User Not Found!"
                });
            }
            var user = countUser.First();
            UserAuthModel userAuth = new UserAuthModel
            {
                name = $"{user.FirtsName} {user.LastName} ",
                userId = user.IdUser.ToString(),
                Rol = "Admin"
            };

            var token = _authServices.createJWT(userAuth);

            var userAuthOutputModel = _mapper.Map<UserAuthOutputModel>(user);
            userAuthOutputModel.Token = token;

            return Ok(new GenericResponse
            {
                ObjectResponse = userAuthOutputModel,
                SuccessMessage = "Login Success!"
            });
        }
    }
}
