
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Ordering.Application.Common.Observers;

public class BusObserver : IBusObserver
{
	private readonly ILogger<BusObserver> _logger;

	public BusObserver(ILogger<BusObserver> logger)
	{
		_logger = logger;
	}

	public void PostCreate(IBus bus)
	{
		_logger.LogInformation("MassTransit Bus Created");
	}

	public void CreateFaulted(Exception exception)
	{
		_logger.LogError(exception, "MassTransit Bus Creation Failed");
	}

	public Task PreStart(IBus bus)
	{
		_logger.LogInformation("MassTransit Bus Starting...");
		return Task.CompletedTask;
	}

	public Task PostStart(IBus bus, Task<BusReady> busReady)
	{
		_logger.LogInformation("MassTransit Bus Started Successfully");
		return Task.CompletedTask;
	}

	public Task StartFaulted(IBus bus, Exception exception)
	{
		_logger.LogError(exception, "MassTransit Bus Failed to Start");
		return Task.CompletedTask;
	}

	public Task PreStop(IBus bus)
	{
		_logger.LogInformation("MassTransit Bus Stopping...");
		return Task.CompletedTask;
	}

	public Task PostStop(IBus bus)	
	{
		_logger.LogInformation("MassTransit Bus Stopped");
		return Task.CompletedTask;
	}

	public Task StopFaulted(IBus bus, Exception exception)
	{
		_logger.LogError(exception, "MassTransit Bus Failed to Stop");
		return Task.CompletedTask;
	}
}
