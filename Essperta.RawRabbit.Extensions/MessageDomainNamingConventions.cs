using Essperta.RawRabbit.Extensions.Utils;
using RawRabbit.Common;
using RawRabbit.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Essperta.RawRabbit.Extensions
{
	public class MessageDomainNamingConventions : NamingConventions
	{
        private RawRabbitConfiguration rawRabbitConfiguration;

        public MessageDomainNamingConventions(RawRabbitConfiguration rawRabbitConfiguration)
        {
            this.rawRabbitConfiguration = rawRabbitConfiguration;
        }

		public MessageDomainNamingConventions(string defaultNamespace)
		{
            var assemblyName = Assembly.GetEntryAssembly().GetName().Name;

            ExchangeNamingConvention = GetExchangeName;
            RoutingKeyConvention = GetRoutingKeyName;
            QueueNamingConvention = GetQueueName;

            ErrorExchangeNamingConvention = () => $"{defaultNamespace}.error";
            RetryLaterExchangeConvention = span => $"{defaultNamespace}.retry";
            RetryLaterQueueNameConvetion = (exchange, span) => 
                $"{defaultNamespace}.retry_for_{exchange.Replace(".", "_")}_in_{span.TotalMilliseconds}_ms".ToLowerInvariant();
        }

        private string GetExchangeName(Type messageType)
        {
            return "performa_365_dev";
        }

        private string GetRoutingKeyName(Type messageType)
        {
            return messageType.GetDomainFromType();
        }
        
        private string GetQueueName(Type messageType)
        {
            return $"{GetExchangeName(messageType)}__{GetRoutingKeyName(messageType)}";
        }
    }
}
