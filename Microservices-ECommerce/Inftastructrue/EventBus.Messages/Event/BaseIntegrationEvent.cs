using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace EventBus.Messages.Event
{
	public class BaseIntegrationEvent
	{
		public string CorrelationId { get; set; }
		public DateTime CreationDate { get; set; }

		public BaseIntegrationEvent()
		{
			CorrelationId = Guid.NewGuid().ToString();
		    CreationDate = DateTime.UtcNow; 
		}

		public BaseIntegrationEvent(Guid _correlationId, DateTime _creationDate)
		{
			CorrelationId = _correlationId.ToString();
			CreationDate = _creationDate;
		}
	}
}
