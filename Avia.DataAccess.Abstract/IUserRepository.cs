using Avia.Domain;
using System.Collections.Generic;

namespace Avia.DataAccess.Abstract
{
	public interface IUserRepository
	{
		void Add(User user);
		void Delete(string userId);
		void Update(User user);
		ICollection<User> GetAll();
	}
}
