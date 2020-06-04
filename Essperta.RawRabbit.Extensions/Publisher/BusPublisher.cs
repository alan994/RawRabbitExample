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

		public BusPublisher(IBusClient busClient)
		{
			this.busClient = busClient;
		}
		
		public async Task SendAsync<TCommand>(TCommand command, ICustomMessageContext messageContext)
		{
			await this.busClient.PublishAsync(command, ctx => {
				ctx.UseMessageContext(messageContext).UseCustomRoutingKey(messageContext.TenantId, command.GetType());
				ctx.UsePublishConfiguration(config => config.OnExchange("Some_exchange_name"));
			});
		}
	}
}
