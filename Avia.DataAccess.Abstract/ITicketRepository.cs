using Avia.Domain;
using System.Collections.Generic;

namespace Avia.DataAccess.Abstract
{
	public interface ITicketRepository
	{
		void Add(Ticket ticket);
		void Delete(string ticketId);
		void Update(Ticket ticket);
		ICollection<Ticket> GetAll();
	}
}
