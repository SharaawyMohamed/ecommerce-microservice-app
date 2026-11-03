using EventBus.Messages.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Messages
{
	public class BasketCheckoutEventV2 : BaseIntegrationEvent
	{
		public string UserName { get; set; }
		public decimal TotalPrice { get; set; }
	}

}
