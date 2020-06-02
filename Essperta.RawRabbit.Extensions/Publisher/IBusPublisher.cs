using RawRabbit.Pipe;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Essperta.RawRabbit.Extensions.Publisher
{
	public interface IBusPublisher
	{
		Task SendAsync<TCommand>(TCommand command, ICustomMessageContext messageContext);
	}
}
