using RawRabbit;
using RawRabbit.Enrichers.MessageContext;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Essperta.RawRabbit.Extensions
{
	public interface IBusPublisher
	{
		Task SendAsync<TCommand>(TCommand command, ICustomMessageContext messageContext);
	}
	
	public class BusPublisher : IBusPublisher
	{
		private IBusClient busClient;

		public BusPublisher(IBusClient busClient)
		{
			this.busClient = busClient;
		}
		public async Task SendAsync<TCommand>(TCommand command, ICustomMessageContext messageContext)
		{
			await this.busClient.PublishAsync(command, ctx => {
				ctx.UseMessageContext(messageContext);
			});
		}
	}
}
