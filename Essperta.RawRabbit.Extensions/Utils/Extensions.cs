using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit.Configuration;
using RawRabbit.DependencyInjection.ServiceCollection;
using RawRabbit.Enrichers.MessageContext;
using RawRabbit.Instantiation;
using RawRabbit.Pipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Essperta.RawRabbit.Extensions.Utils
{
	public static class Extensions
	{
		public static string Underscore(this string value)
			=> string.Concat(value.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString()));

		public static TModel GetOptions<TModel>(this IConfiguration configuration, string section) where TModel : new()
		{
			var model = new TModel();
			configuration.GetSection(section).Bind(model);

			return model;
		}

		public static IPipeContext UseCustomRoutingKey(this IPipeContext context, Guid tenant, Type type)
		{
			context.Properties.Add(PipeKey.RoutingKey, $"{type.GetDomainFromType()}.{tenant.ToString()}");
			return context;
		}

		public static string GetDomainFromType(this Type type)
		{
			var domain = type.GetCustomAttribute<MessageDomainAttribute>()?.Domain ?? "general";
			return domain;
		}

		public static IServiceCollection RegisterRawRabbitSettings(this IServiceCollection services)
		{

			services.AddSingleton<RawRabbitConfiguration>((config) =>
			{
				var configuration = config.GetRequiredService<IConfiguration>();
				var settings = configuration.GetOptions<RawRabbitConfiguration>("RabbitMq");
				if (settings == null)
				{
					throw new ArgumentNullException("Can't find raw rabbit configuration");
				}
				return settings;
			});
			return services;
		}

		public static IServiceCollection AddRabbitMq(this IServiceCollection services, RawRabbitConfiguration rawRabbitConfiguration)
		{
			services.RegisterRawRabbitSettings();
			services.AddRawRabbit(new RawRabbitOptions()
			{
				ClientConfiguration = rawRabbitConfiguration,
				Plugins = p => p.UseMessageContext<CustomMessageContext>()
			});
			return services;
		}
	}
}
