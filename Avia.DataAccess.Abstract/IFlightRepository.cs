using Avia.Domain;
using System.Collections.Generic;

namespace Avia.DataAccess.Abstract
{
	public interface IFlightRepository
	{
		void Add(Flight flight);
		void Delete(string flightId);
		void Update(Flight flight);
		ICollection<Flight> GetAll();
	}
}
