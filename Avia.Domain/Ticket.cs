using System;
using System.Collections.Generic;
using System.Text;

namespace Avia.Domain
{
	public class Ticket : Entity
	{
		public Guid FlightId { get; set; }
		public Guid UserId { get; set; }
		public int Price { get; set; }
		public string Class { get; set; }
		public int Seat { get; set; }
		public string FullName { get; set; }
		public string PassportNum { get; set; }
		public bool IsKid { get; set; }
	}
}
