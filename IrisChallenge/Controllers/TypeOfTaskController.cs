using Iris.Commons.Library.Base;
using Iris.Shared.InputModels;
using IrisChallenge.Interfaces;
using IrisChallenge.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IrisChallenge.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class TypeOfTaskController : ControllerBase
    {
        private readonly IAuthServices _authServices;
        private readonly ITypeOfTaskService _typeOfTaskService;
        public TypeOfTaskController(ITypeOfTaskService typeOfTaskService, IAuthServices authServices)
        {
            _typeOfTaskService = typeOfTaskService;
            _authServices = authServices;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetTypes()
        {
            int idUser = 0;
            var resp = _authServices.getUserIdfromToken(HttpContext);
            if (resp.Item1)
                idUser = int.Parse(resp.Item2);

            var res = await _typeOfTaskService.GetTypeOfTaskbyUser(idUser);
            return Ok(new GenericResponse(res));
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddType(string text)
        {
            int idUser = 0;
            var resp = _authServices.getUserIdfromToken(HttpContext);
            if (resp.Item1)
                idUser = int.Parse(resp.Item2);

            var res = await _typeOfTaskService.SetTypeOfTaskbyUser(idUser, text);
            return Ok(new GenericResponse(res));
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateType(int id, string text)
        {
            int idUser = 0;
            var resp = _authServices.getUserIdfromToken(HttpContext);
            if (resp.Item1)
                idUser = int.Parse(resp.Item2);

            var res = await _typeOfTaskService.UpdateTypeOfTaskbyUser(id, idUser, text);
            return Ok(new GenericResponse(res));
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteType(int id)
        {
            int idUser = 0;
            var resp = _authServices.getUserIdfromToken(HttpContext);
            if (resp.Item1)
                idUser = int.Parse(resp.Item2);

            var res = await _typeOfTaskService.DeleteTypeOfTaskbyUser(id, idUser);
            return Ok(new GenericResponse(res));
        }

    }
}
