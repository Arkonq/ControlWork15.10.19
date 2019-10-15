using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using Avia.DataAccess.Abstract;
using Avia.Domain;

namespace Avia.DataAccess
{
	public class FlightRepository : IFlightRepository
	{
		private readonly string connectionString;
		private readonly DbProviderFactory providerFactory;

		public FlightRepository(string connectionString, string providerName)
		{
			this.connectionString = connectionString;
			providerFactory = DbProviderFactories.GetFactory(providerName);
		}

		public void Add(Flight flight)
		{
			using (DbConnection connection = providerFactory.CreateConnection())
			using (DbCommand command = connection.CreateCommand())
			{
				string query = $"INSERT INTO Flights" +
						$"VALUES (" +
						$"@Id, " +
						$"@From," +
						$"@To," +
						$"@DepartureDate," +
						$"@Plane," +
						$"@AirCarrier);";
				command.CommandText = query;

				DbParameter parameter = providerFactory.CreateParameter();
				parameter.DbType = System.Data.DbType.Guid;
				parameter.ParameterName = "@Id";
				parameter.Value = flight.Id;
				command.Parameters.Add(parameter);

				parameter = new SqlParameter();
				parameter.DbType = System.Data.DbType.String;
				parameter.ParameterName = "@From";
				parameter.Value = flight.From;
				command.Parameters.Add(parameter);

				parameter = new SqlParameter();
				parameter.DbType = System.Data.DbType.String;
				parameter.ParameterName = "@To";
				parameter.Value = flight.To;
				command.Parameters.Add(parameter);

				parameter = new SqlParameter();
				parameter.DbType = System.Data.DbType.String;
				parameter.ParameterName = "@DepartureDate";
				parameter.Value = flight.DepartureDate;
				command.Parameters.Add(parameter);

				parameter = new SqlParameter();
				parameter.DbType = System.Data.DbType.String;
				parameter.ParameterName = "@Plane";
				parameter.Value = flight.Plane;
				command.Parameters.Add(parameter);

				parameter = new SqlParameter();
				parameter.DbType = System.Data.DbType.String;
				parameter.ParameterName = "@AirCarrier";
				parameter.Value = flight.AirCarrier;
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

		public void Delete(string flightId)
		{
			throw new System.NotImplementedException();
		}

		public ICollection<Flight> GetAll()
		{
			using (DbConnection connection = providerFactory.CreateConnection())
			using (DbCommand command = connection.CreateCommand())
			{
				string query = "SELECT * FROM Tickets";
				command.CommandText = query;

				connection.ConnectionString = connectionString;
				connection.Open();
				DbDataReader dataReader = command.ExecuteReader();

				List <Flight> flights = new List<Flight>();
				while (dataReader.Read())
				{
					flights.Add(new Flight
					{
						Id = Guid.Parse(dataReader["Id"].ToString()),
						From = dataReader["From"].ToString(),
						To = dataReader["To"].ToString(),
						DepartureDate = DateTime.Parse(dataReader["DepartureDate"].ToString()),
						Plane = dataReader["Plane"].ToString(),
						AirCarrier = dataReader["AirCarrier"].ToString()
					});
				}
				return flights;
			}
		}

		public void Update(Flight flight)
		{
			throw new System.NotImplementedException();
		}
	}
}
