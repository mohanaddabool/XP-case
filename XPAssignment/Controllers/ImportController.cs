using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XPAssignment.Context;
using XPAssignment.DbModels;
using XPAssignment.Import;
using XPAssignment.PostData.Show;
using Show = XPAssignment.ViewModel.Show;

namespace XPAssignment.Controllers
{
    [ApiController]
    [Authorize]
    public class ImportController : Controller
    {
        public IImport Import { get; }

        public ImportController(IImport import)
        {
            Import = import;
        }

        [Route("App/Import/ReadJson")]
        [HttpGet]
        public IActionResult ReadJson()
        {
            Import.ReadAndInsert();
            return Ok();
        }
    }
}
