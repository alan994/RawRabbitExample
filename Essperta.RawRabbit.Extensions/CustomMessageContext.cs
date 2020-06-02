using RawRabbit.Enrichers.MessageContext.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Essperta.RawRabbit.Extensions
{
	public interface ICustomMessageContext : IMessageContext
	{
		Guid TenantId { get; }
		Guid UserId { get; }
		string TenantExternalId { get; }
		string UserExternalId { get; }
	}

	public class CustomMessageContext : ICustomMessageContext
	{
		public Guid GlobalRequestId { get; set; }
		public Guid TenantId { get; set; }
		public Guid UserId { get; set; }
		public string TenantExternalId { get; set; }
		public string UserExternalId { get; set; }

	}
}
