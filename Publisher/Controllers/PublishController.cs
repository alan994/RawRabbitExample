using Essperta.RawRabbit.Extensions;
using Essperta.RawRabbit.Extensions.Publisher;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RawRabbit;
using RawRabbit.Pipe;
using Shared;
using System;
using System.Diagnostics;
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
				TenantExternalId = "essperta external id",
				TenantId = Guid.NewGuid(),
				UserExternalId = "essperta user external id",
				UserId = Guid.NewGuid(),
				GlobalRequestId = Guid.NewGuid()				
			});

			var activity = Activity.Current.Id;

			return this.Ok("Poslano");
		}
	}
}
