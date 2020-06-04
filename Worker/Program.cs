using Essperta.RawRabbit.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RawRabbit.DependencyInjection.ServiceCollection;
using RawRabbit.Enrichers.MessageContext;
using Serilog;

namespace Worker
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.UseSerilog((webHostBuilderConfiguration, loggerConfiguration) =>
				{
					loggerConfiguration.WriteTo.Console();
					//.ReadFrom.Configuration(webHostBuilderConfiguration.Configuration.GetSection("Logging"));
				})
				.ConfigureServices((hostContext, services) =>
				{
					services.AddHostedService<Worker>();
					services.AddRawRabbit(new RawRabbit.Instantiation.RawRabbitOptions()
					{
						ClientConfiguration = new RawRabbit.Configuration.RawRabbitConfiguration()
						{
							Hostnames = new System.Collections.Generic.List<string>() { "localhost" },
							Username = "guest",
							Password = "guest",
							Port = 5672,
							VirtualHost = "/"
						},
						Plugins = p => p.UseMessageContext<CustomMessageContext>()
					});
				});
	}
}
