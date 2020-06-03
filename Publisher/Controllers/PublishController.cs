using Essperta.RawRabbit.Extensions;
using Essperta.RawRabbit.Extensions.Publisher;
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
		private readonly IBusPublisher publisher;

		public PublishController(ILogger<PublishController> logger, IBusPublisher publisher)
		{
			this.logger = logger;
			this.publisher = publisher;
		}

		[HttpGet("createEducation/essperta")]
		public async Task<ActionResult> PublishCreateEducationForEssperta()
		{
			var command = new CreateEducation()
			{
				Name = "Essperta education",
				Description = "Essperta education description"
			};

			await this.publisher.SendAsync(command, new CustomMessageContext()
			{
				
			});

			return this.Ok("Poslano");
		}
	}
}
