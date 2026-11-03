using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logging.Application
{
	public static class Logging
	{
		public static Action<HostBuilderContext, LoggerConfiguration> ConfigureLogging => (context, configuration) =>
		{
			var env = context.HostingEnvironment;
			configuration.MinimumLevel.Information()
				.Enrich.FromLogContext()
				.Enrich.WithProperty("ApplicationName", env.ApplicationName)
				.Enrich.WithProperty("EnvironmentName", env.EnvironmentName)
				.Enrich.WithExceptionDetails()
				.MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
				.MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
				.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext} - {Message:lj}{NewLine}{Exception}");


			if (context.HostingEnvironment.IsDevelopment())
			{
				configuration.MinimumLevel.Override("Catalog", LogEventLevel.Debug);
				configuration.MinimumLevel.Override("Basket", LogEventLevel.Debug);
				configuration.MinimumLevel.Override("Discount", LogEventLevel.Debug);
				configuration.MinimumLevel.Override("Ordering", LogEventLevel.Debug);
			}


			var elasticUri = context.Configuration["ElasticConfiguration:Uri"];

			if (!string.IsNullOrWhiteSpace(elasticUri))
			{
				configuration.WriteTo.Elasticsearch(
					new ElasticsearchSinkOptions(new Uri(elasticUri))
					{
						AutoRegisterTemplate = true,
						IndexFormat = "logs-{0:yyyy.MM.dd}"
					});
			}

		};
	};

}
