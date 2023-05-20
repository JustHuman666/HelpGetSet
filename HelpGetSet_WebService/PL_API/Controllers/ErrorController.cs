using PL_API.ErrorBuilder;
using BLL.Validation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace PL_API.Controllers
{
    /// <summary>
    /// Controller for error handling
    /// </summary>
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        public ActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error;

            if (exception is HelpSiteException)
            {
                return BadRequest(new ApiErrorBuilder(exception.Message, HttpStatusCode.BadRequest));
            }

            if (exception is NotFoundException)
            {
                return NotFound(new ApiErrorBuilder(exception.Message, HttpStatusCode.NotFound));
            }

            return new JsonResult(new ApiErrorBuilder(exception.Message, HttpStatusCode.InternalServerError));
        }
    }
}
