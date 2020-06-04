using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Essperta.RawRabbit.Extensions.Subscriber
{
	public interface IBusSubscriber
	{
		Task SubscribeCommandAsync<TCommand>(TCommand command);
	}
}
