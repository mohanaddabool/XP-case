using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XPAssignment.Helper.Response;
using XPAssignment.PostData.Show;
using XPAssignment.Service.Interfaces;

namespace XPAssignment.Controllers
{
    [ApiController]
    [Authorize]
    public class ShowController : ControllerBase
    {
        public IShowService ShowService { get; }

        public ShowController(IShowService showService)
        {
            ShowService = showService;
        }

        [Route("App/Show/GetAllFromTvMaze")]
        [HttpGet]
        public IActionResult GetFromApi()
        {
            var data = ShowService.GetFromTvMaze();
            return Ok(data);
        } 

        [Route("App/Show/GetAll")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = ShowService.GetAll();
            return result.State switch
            {
                ResponseState.NotFound => NotFound(result.Message),
                ResponseState.Found => Ok(result.Shows),
                _ => BadRequest(result.Message),
            };
        }

        [Route("App/Show/GetByName")]
        [HttpPost]
        public IActionResult GetByName([FromBody] GetShowByName model)
        {
            TryValidateModel(model);
            var result = ShowService.GetByName(model.Name);
            return result.State switch
            {
                ResponseState.NotFound => NotFound(result.Message),
                ResponseState.Found => Ok(result.Show),
                _ => BadRequest(result.Message),
            };
        }

        [Route("App/Show/Add")]
        [HttpPost]
        public IActionResult Add([FromBody] AddShow model)
        {
            TryValidateModel(model);
            var result = ShowService.Add(model);
            return result.State switch
            {
                ResponseState.DuplicateName => BadRequest(result.Message),
                ResponseState.Created => Ok(result),
                _ => BadRequest(result.Message)
            };
        }

        [Route("App/Show/Edit")]
        [HttpPost]
        public IActionResult Edit([FromBody] EditShow model)
        {
            TryValidateModel(model);
            var result = ShowService.Update(model);
            return result.State switch
            {
                ResponseState.NotFound => BadRequest(result.Message),
                ResponseState.Edited => Ok(result.Show),
                _ => BadRequest(result.Message)
            };
        }

        [Route("App/Show/Delete")]
        [HttpPost]
        public IActionResult Edit([FromBody] DeleteShow model)
        {
            TryValidateModel(model);
            var result = ShowService.Delete(model.Id);
            return result.State switch
            {
                ResponseState.NotFound => BadRequest(result.Message),
                ResponseState.Deleted => Ok(result.Message),
                _ => BadRequest(result.Message)
            };
        }
    }
}
