using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RawRabbit;
using RawRabbit.Pipe;
using Shared;
using System.Threading.Tasks;

namespace Publisher.Controllers
{
	[Route("publish")]
	public class PublishController : Controller
	{
		private ILogger<PublishController> logger;
		private readonly IBusClient client;

		public PublishController(ILogger<PublishController> logger, IBusClient client)
		{
			this.logger = logger;
			this.client = client;
		}

		[HttpGet("createEducation/essperta")]
		public async Task<ActionResult> PublishCreateEducationForEssperta()
		{
			var command = new CreateEducation()
			{
				Name = "Essperta education",
				Description = "Essperta education description"
			};

			await this.client.PublishAsync(command, (publishContext) => { });

			return this.Ok("Poslano");
		}
	}
}
