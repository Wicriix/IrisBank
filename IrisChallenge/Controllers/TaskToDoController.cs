using Iris.Commons.Library.Base;
using Iris.Data.Repository;
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
    public class TaskToDoController : ControllerBase
    {
        private readonly IAuthServices _authServices;
        private readonly ITaskToDoService _taskToDoService;
        public TaskToDoController(ITaskToDoService taskToDoService, IAuthServices authServices)
        {
            _taskToDoService = taskToDoService;
            _authServices = authServices;
        }

        [HttpGet("Get")]
        [Authorize]
        public async Task<IActionResult> GetTypes()
        {
            int idUser = 0;
            var resp = _authServices.getUserIdfromToken(HttpContext);
            if (resp.Item1)
                idUser = int.Parse(resp.Item2);

            var res = await _taskToDoService.GetTaskToDobyUser(idUser);
            return Ok(new GenericResponse(res));
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddType(string text, int idtype)
        {
            int idUser = 0;
            var resp = _authServices.getUserIdfromToken(HttpContext);
            if (resp.Item1)
                idUser = int.Parse(resp.Item2);

            var res = await _taskToDoService.SetTaskToDobyUser(idtype, idUser, text);
            return Ok(new GenericResponse(res));
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateType(int id, string text, bool done = false)
        {
            int idUser = 0;
            var resp = _authServices.getUserIdfromToken(HttpContext);
            if (resp.Item1)
                idUser = int.Parse(resp.Item2);

            var res = await _taskToDoService.UpdateTaskToDobyUser(id, idUser, text, done);
            return Ok(new GenericResponse(res));
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteType(int id)
        {
            int idUser = 0;
            var resp = _authServices.getUserIdfromToken(HttpContext);
            if (resp.Item1)
                idUser = int.Parse(resp.Item2);

            var res = await _taskToDoService.DeleteTaskToDobyUser(id, idUser);
            return Ok(new GenericResponse(res));
        }

    }
}
