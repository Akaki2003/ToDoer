using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using ToDoer.API.ModelExamples;
using ToDoer.Application.PatchModel;
using ToDoer.Application.Subtasks;
using ToDoer.Application.Subtasks.Requests;
using ToDoer.Application.Subtasks.Responses;
using ToDoer.Application.ToDos;
using ToDoer.Application.ToDos.Requests;
using ToDoer.Application.ToDos.Responses;
using ToDoer.Domain.ToDos;
using Status = ToDoer.Domain.ToDos.Status;

namespace ToDoer.API.Controllers
{

    //[Route("api/v{version:apiVersion}/[controller]")]
    [Route("v1/[controller]")]
    [ApiController]
    [Authorize]
    public class ToDoApiController : Controller
    {
        private readonly IToDoService _toDoService;
        private readonly ISubtaskService _subtaskService;
        private readonly IHttpContextAccessor _Accessor;
        private readonly int userId;
        public ToDoApiController(IToDoService toDoService, ISubtaskService subtaskService, IHttpContextAccessor accessor)
        {
            _toDoService = toDoService;
            _subtaskService = subtaskService;
            _Accessor = accessor;
            userId = int.Parse(_Accessor.HttpContext.User.Claims.Single(x => x.Type == "UserId").Value);
        }
        /// <summary>
        /// Get ToDos
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ToDosGet))]
        [Route("GetCurrentUserToDos")]
        [HttpGet]
        public async Task<List<ToDoResponseModel>> GetToDos(CancellationToken cancellationToken,StatusChecking? status)
        {
            return await _toDoService.GetAllAsync(cancellationToken, userId, status);
        }

        /// <summary>
        /// Get To Do
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCurrentUserToDosWithId/{id}")]
        [Produces("application/json")]
        [SwaggerResponseExample(StatusCodes.Status200OK,typeof(ToDoGet))]
        public async Task<ToDoResponseModel> GetToDoById(CancellationToken cancellationToken, int id)
        {
            return await _toDoService.GetByIdAsync(cancellationToken, id,userId);
        }


        /// <summary>
        /// Create To Do
        /// </summary>
        /// <returns></returns>
        [SwaggerRequestExample(typeof(ToDoCreateModel), typeof(TodosCreate))]
        [HttpPost("CreateToDo")]
        public async Task Post(CancellationToken cancellationToken, ToDoCreateModel request)
        {
            await _toDoService.CreateAsync(cancellationToken, request,userId);
        }


        /// <summary>
        /// Uptade To Do
        /// </summary>
        /// <returns></returns>
        [SwaggerRequestExample(typeof(ToDoRequestPutModel), typeof(TodosUpdate))]
        [HttpPut("UpdateToDo")]
        public async Task Put(CancellationToken cancellationToken, ToDoRequestPutModel request)
        {
            await _toDoService.UpdateToDoAsync(cancellationToken, request,userId);
        }

        /// <summary>
        /// Patch To Do
        /// </summary>
        /// <returns></returns>
        [SwaggerRequestExample(typeof(List<RequestPatchModel>), typeof(ToDosPatch))]
        [HttpPatch("{id}")]
        public async Task<IActionResult> ToDoPatch( CancellationToken cancellationToken,[FromBody] JsonPatchDocument toDoToUptade, [FromRoute] int id)
        {
            await _toDoService.UpdateToDoPatchAsync(cancellationToken,id,toDoToUptade,userId);
            return Ok();
        }

        /// <summary>
        /// Delete ToDo
        /// </summary>
        /// <returns></returns>
        [HttpDelete("DeleteToDo/{id}")]
        public async Task<int> Delete(CancellationToken cancellationToken, int id)
        {
            return await _toDoService.DeleteAsync(cancellationToken, id,userId);
        }

        /// <summary>
        /// Get Subtask by ID
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SubtaskById))]
        [HttpGet("GetCurrentUserSubtaskById/{id}")]
        public async Task<SubtaskResponseModel> GetSubtaskById(CancellationToken cancellationToken, int id)
        {
            return await _subtaskService.GetByIdAsync(cancellationToken, id,userId);

        }

        /// <summary>
        /// Create Subtask
        /// </summary>
        /// <returns></returns>
        [SwaggerRequestExample(typeof(SubtaskCreateModel), typeof(SubtasksCreate))]
        [HttpPost("CreateSubtask")]
        public async Task PostSubtask(CancellationToken cancellationToken, SubtaskRequestModel request)
        {
            await _subtaskService.CreateAsync(cancellationToken, request, request.TodoItemId);
        }
        /// <summary>
        /// Delete Subtask
        /// </summary>
        /// <returns></returns>
        [HttpDelete("DeleteSubtask/{id}")]
        public async Task DeleteSubtask(CancellationToken cancellationToken, int id)
        {
            await _subtaskService.DeleteAsync(cancellationToken, id, userId);
        }
        /// <summary>
        /// Update Subtask
        /// </summary>
        /// <returns></returns>
        [SwaggerRequestExample(typeof(SubtaskRequestPutModel), typeof(SuptaskUpdate))]
        [HttpPut("UpdateSubtask")]
        public async Task PutSubtask(CancellationToken cancellationToken, SubtaskRequestPutModel request)
        {
            await _subtaskService.UpdateSubtaskAsync(cancellationToken, request, userId);
        }


    }
}
