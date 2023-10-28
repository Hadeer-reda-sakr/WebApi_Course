using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi_Task.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FirstController : ControllerBase
	{

		[HttpGet]
		public string GetAll()
		{
			return "Hello There";
		}

	}
}
