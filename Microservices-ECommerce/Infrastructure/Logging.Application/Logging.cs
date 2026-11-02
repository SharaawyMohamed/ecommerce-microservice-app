using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logging.Application
{
	public static class Logging
	{
		public static Action<HostBuilderContext,LoggerConfiguration> ConfigureLogging => (context, configuration) =>
		{
			var env = context.HostingEnvironment;
			configuration.MinimumLevel.Information()
				.Enrich.FromLogContext()          // Add additional information to the log context
				.Enrich.WithProperty("ApplicationName", env.ApplicationName) // Add application name to the log context
				.Enrich.WithProperty("EnvironmentName", env.EnvironmentName) // Add environment name to the log context
				.Enrich.WithExceptionDetails()
				.MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)// Override the minimum level for Microsoft namespaces
		        .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)// Override the minimum level for Microsoft.Hosting.Lifetime namespace
				.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext} - {Message:lj}{NewLine}{Exception}");


			if(context.HostingEnvironment.IsDevelopment())
			{
				configuration.MinimumLevel.Override("Catalog",LogEventLevel.Debug);	
				configuration.MinimumLevel.Override("Basket",LogEventLevel.Debug);	
				configuration.MinimumLevel.Override("Discount",LogEventLevel.Debug);	
				configuration.MinimumLevel.Override("Ordering",LogEventLevel.Debug);	
			}
			// TODO:: Implement Elastic search sink for production environment
		};
	}
}
