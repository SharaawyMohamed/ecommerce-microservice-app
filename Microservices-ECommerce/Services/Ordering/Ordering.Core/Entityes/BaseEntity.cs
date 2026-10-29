using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Core.Entityes
{
	public abstract class BaseEntity
	{
		public int Id { get; protected set; }
		public string? CreatedBy { get; set; }
		public DateTime? CreatedDate { get; set; }
		public string? LastModifiedBy { get; set; }
		public DateTime? LastModifiedDate { get; set; }

	}
}
