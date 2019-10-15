using System;
using System.Collections.Generic;
using System.Text;

namespace Avia.Domain
{
	public class Flight : Entity
	{
		public string From { get; set; }
		public string To { get; set; }
		public DateTime DepartureDate { get; set; }
		public string Plane { get; set; }
		public string AirCarrier { get; set; }
	}
}
