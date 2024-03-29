using Microsoft.AspNetCore.Mvc;

namespace WebApiTraining2.Controllers
{
	[ApiController]
	[ApiExplorerSettings(IgnoreApi =true)]
	public class ErrorController : ControllerBase
	{
		[Route("error")]
		public IActionResult HandleError() => Problem();
	}
}
