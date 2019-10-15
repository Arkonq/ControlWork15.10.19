using System;
using System.Collections.Generic;
using System.Text;

namespace Avia.Domain
{
	public class User : Entity
	{
		public string Login { get; set; }
		public string Password { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
	}
}
