using System;

namespace Essperta.RawRabbit.Extensions
{
	[AttributeUsage(AttributeTargets.Class)]
	public class MessageDomainAttribute : Attribute
	{
		public string Domain { get; }
		public MessageDomainAttribute(string domain)
		{
			this.Domain = domain?.ToLowerInvariant();
		}
	}
}
