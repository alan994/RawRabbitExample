using Essperta.RawRabbit.Extensions.Utils;
using RawRabbit;
using RawRabbit.Enrichers.MessageContext;
using RawRabbit.Instantiation;
using System.Threading.Tasks;

namespace Essperta.RawRabbit.Extensions.Publisher
{
	public class BusPublisher : IBusPublisher
	{
		private IBusClient busClient;

		public BusPublisher(IBusClient busClient, RawRabbitOptions rawRabbitOptions)
		{
			this.busClient = busClient;
			RawRabbitOptions = rawRabbitOptions;
		}

		public RawRabbitOptions RawRabbitOptions { get; }

		public async Task SendAsync<TCommand>(TCommand command, ICustomMessageContext messageContext)
		{
			await this.busClient.PublishAsync(command, ctx => {
				ctx.UseMessageContext(messageContext).UseCustomRoutingKey(messageContext.TenantId, command.GetType());
				
			});
		}
	}
}
