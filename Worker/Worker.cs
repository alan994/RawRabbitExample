using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Essperta.RawRabbit.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RawRabbit;
using RawRabbit.Common;
using Shared;

namespace Worker
{
	public class Worker : BackgroundService
	{
		private readonly ILogger<Worker> _logger;
		private readonly IBusClient client;

		public Worker(ILogger<Worker> logger, IBusClient client)
		{
			_logger = logger;
			this.client = client;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			//while (!stoppingToken.IsCancellationRequested)
			//{
			//	_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
			//	await Task.Delay(1000, stoppingToken);
			//}

			await client.SubscribeAsync<CreateEducation, CustomMessageContext>(CreateEducationHandler, ctx =>
			{
				ctx.UseSubscribeConfiguration(config => config.OnDeclaredExchange(exchange => exchange.WithName("Some_exchange_name")));
			});
		}

		private async Task<Ack> CreateEducationHandler(CreateEducation arg, CustomMessageContext messageContext)
		{
			this._logger.LogInformation("Create education handler. {@Payload}, {@MessageContext}", arg, messageContext);
			return new Ack();
		}
	}
}
