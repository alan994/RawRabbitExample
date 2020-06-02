using Essperta.RawRabbit.Extensions.Utils;
using RawRabbit.Common;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Essperta.RawRabbit.Extensions
{
	public class MessageDomainNamingConventions : NamingConventions
	{
		public MessageDomainNamingConventions()
		{

		}
		public MessageDomainNamingConventions(string defaultNamespace)
		{
            var assemblyName = Assembly.GetEntryAssembly().GetName().Name;
            ExchangeNamingConvention = GetExchangeName;

            RoutingKeyConvention = type => $"{GetRoutingKeyNamespace(type, defaultNamespace)}{type.Name.Underscore()}".ToLowerInvariant();

            QueueNamingConvention = type => GetQueueName(assemblyName, type, defaultNamespace);

            ErrorExchangeNamingConvention = () => $"{defaultNamespace}.error";
            RetryLaterExchangeConvention = span => $"{defaultNamespace}.retry";
            RetryLaterQueueNameConvetion = (exchange, span) => 
                $"{defaultNamespace}.retry_for_{exchange.Replace(".", "_")}_in_{span.TotalMilliseconds}_ms".ToLowerInvariant();
        }

        private string GetExchangeName(Type messageType)
        {
            return "performa_365_dev";
        }

        private static string GetRoutingKeyNamespace(Type type, string defaultNamespace)
        {
            var @namespace = type.GetCustomAttribute<MessageDomainAttribute>()?.Domain ?? defaultNamespace;

            return string.IsNullOrWhiteSpace(@namespace) ? string.Empty : $"{@namespace}.";
        }

        private static string GetNamespace(Type type, string defaultNamespace)
        {
            var @namespace = type.GetCustomAttribute<MessageDomainAttribute>()?.Domain ?? defaultNamespace;

            return string.IsNullOrWhiteSpace(@namespace) ? type.Name.Underscore() : $"{@namespace}";
        }

        private static string GetQueueName(string assemblyName, Type type, string defaultNamespace)
        {
            var @namespace = type.GetCustomAttribute<MessageDomainAttribute>()?.Domain ?? defaultNamespace;
            var separatedNamespace = string.IsNullOrWhiteSpace(@namespace) ? string.Empty : $"{@namespace}.";

            return $"{assemblyName}/{separatedNamespace}{type.Name.Underscore()}".ToLowerInvariant();
        }
    }
}
