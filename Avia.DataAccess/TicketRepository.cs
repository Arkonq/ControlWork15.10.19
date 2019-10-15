using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using Avia.DataAccess.Abstract;
using Avia.Domain;

namespace Avia.DataAccess
{
	public class TicketRepository : ITicketRepository
	{
		private readonly string connectionString;
		private readonly DbProviderFactory providerFactory;

		public TicketRepository(string connectionString, string providerName)
		{
			this.connectionString = connectionString;
			providerFactory = DbProviderFactories.GetFactory(providerName);
		}

		public void Add(Ticket ticket)
		{
			using (DbConnection connection = providerFactory.CreateConnection())
			using (DbCommand command = connection.CreateCommand())
			{
				string query = $"INSERT INTO Tickets" +
						$"VALUES (" +
						$"@Id, " +
						$"@FlightId," +
						$"@UserId," +
						$"@Price," +
						$"@Type," +
						$"@Seat," +
						$"@FullName," +
						$"@PassportNum," +
						$"@IsKid);";
				command.CommandText = query;

				DbParameter parameter = providerFactory.CreateParameter();
				parameter.DbType = System.Data.DbType.Guid;
				parameter.ParameterName = "@Id";
				parameter.Value = ticket.Id;
				command.Parameters.Add(parameter);

				parameter = new SqlParameter();
				parameter.DbType = System.Data.DbType.Guid;
				parameter.ParameterName = "@FlightId";
				parameter.Value = ticket.FlightId;
				command.Parameters.Add(parameter);

				parameter = new SqlParameter();
				parameter.DbType = System.Data.DbType.Guid;
				parameter.ParameterName = "@UserId";
				parameter.Value = ticket.UserId;
				command.Parameters.Add(parameter);

				parameter = new SqlParameter();
				parameter.DbType = System.Data.DbType.Int32;
				parameter.ParameterName = "@Price";
				parameter.Value = ticket.Price;
				command.Parameters.Add(parameter);

				parameter = new SqlParameter();
				parameter.DbType = System.Data.DbType.String;
				parameter.ParameterName = "@Class";
				parameter.Value = ticket.Class;
				command.Parameters.Add(parameter);

				parameter = new SqlParameter();
				parameter.DbType = System.Data.DbType.Int32;
				parameter.ParameterName = "@Seat";
				parameter.Value = ticket.Seat;
				command.Parameters.Add(parameter);

				parameter = new SqlParameter();
				parameter.DbType = System.Data.DbType.String;
				parameter.ParameterName = "@FullName";
				parameter.Value = ticket.FullName;
				command.Parameters.Add(parameter);

				parameter = new SqlParameter();
				parameter.DbType = System.Data.DbType.String;
				parameter.ParameterName = "@PassportNum";
				parameter.Value = ticket.PassportNum;
				command.Parameters.Add(parameter);

				parameter = new SqlParameter();
				parameter.DbType = System.Data.DbType.Boolean;
				parameter.ParameterName = "@Email";
				parameter.Value = ticket.IsKid;
				command.Parameters.Add(parameter);

				connection.ConnectionString = connectionString;
				connection.Open();

				using (DbTransaction transaction = connection.BeginTransaction())
				{
					try
					{
						command.Transaction = transaction;
						command.ExecuteNonQuery();

						transaction.Commit();
					}
					catch
					{
						transaction.Rollback();
					}
				}
			}
		}

		public void Delete(string ticketId)
		{
			throw new System.NotImplementedException();
		}

		public ICollection<Ticket> GetAll()
		{
			using (DbConnection connection = providerFactory.CreateConnection())
			using (DbCommand command = connection.CreateCommand())
			{
				string query = "SELECT * FROM Tickets";
				command.CommandText = query;

				connection.ConnectionString = connectionString;
				connection.Open();
				DbDataReader dataReader = command.ExecuteReader();

				List<Ticket> tickets = new List<Ticket>();
				while (dataReader.Read())
				{
					tickets.Add(new Ticket
					{
						Id = Guid.Parse(dataReader["Id"].ToString()),
						FlightId = Guid.Parse(dataReader["FlightId"].ToString()),
						UserId = Guid.Parse(dataReader["UserId"].ToString()),
						Price = Int32.Parse(dataReader["Price"].ToString()),
						Class = dataReader["Class"].ToString(),
						Seat = Int32.Parse(dataReader["Seat"].ToString()),
						FullName = dataReader["FullName"].ToString(),
						PassportNum = dataReader["PassportNum"].ToString(),
						IsKid = Boolean.Parse(dataReader["PassportNum"].ToString())
					});
				}
				return tickets;
			}
		}

		public void Update(Ticket ticket)
		{
			throw new System.NotImplementedException();
		}
	}
}
