using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Essperta.RawRabbit.Extensions.Subscriber
{
	public class BusSubscriber : IBusSubscriber
	{
		public async Task SubscribeCommandAsync<TCommand>(TCommand command)
		{
			
		}
	}
}
