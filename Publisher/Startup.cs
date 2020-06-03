using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Essperta.RawRabbit.Extensions;
using Essperta.RawRabbit.Extensions.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RawRabbit.Configuration;
using RawRabbit.Configuration.Exchange;
using RawRabbit.DependencyInjection.ServiceCollection;
using RawRabbit.Enrichers.MessageContext;
using RawRabbit.Instantiation;

namespace Publisher
{
	public class Startup
	{
		private IConfiguration configuration;

		public Startup(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			RawRabbitConfiguration rawRabbitConfiguration = null;
			this.configuration.GetSection("RabbitMq").Bind(rawRabbitConfiguration);

			if(rawRabbitConfiguration == null)
			{
				throw new ArgumentNullException("Can't find raw rabbit configuration.");
			}

			services.AddRabbitMq(rawRabbitConfiguration);
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
