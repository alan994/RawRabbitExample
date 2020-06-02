using RawRabbit.Pipe;
using System;
using System.Collections.Generic;
using System.Text;

namespace Essperta.RawRabbit.Extensions
{
	public static class PipeContextExtensions
	{
		public static void UseCustomRoutingKey(this IPipeContext context, Guid tenant, string domain)
		{
			//TODO: Custom routing key
		}
	}
}
