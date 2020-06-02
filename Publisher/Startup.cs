using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Essperta.RawRabbit.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RawRabbit.DependencyInjection.ServiceCollection;
using RawRabbit.Enrichers.MessageContext;
using RawRabbit.Instantiation;

namespace Publisher
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddRawRabbit(new RawRabbit.Instantiation.RawRabbitOptions()
			{
				ClientConfiguration = new RawRabbit.Configuration.RawRabbitConfiguration()
				{
					Hostnames = new List<string>() { "localhost" },
					Username = "guest",
					Password = "guest",
					Port = 5672,
					VirtualHost = "/"
				},
				Plugins = p => p.UseMessageContext<CustomMessageContext>()
			});
		}
				
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
						

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();				
			});
		}
	}
}
