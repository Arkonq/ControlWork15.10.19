using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using Avia.DataAccess.Abstract;
using Avia.Domain;

namespace Avia.DataAccess
{
	public class UserRepository : IUserRepository
	{
		private readonly string connectionString;
		private readonly DbProviderFactory providerFactory;

		public UserRepository(string connectionString, string providerName)
		{
			this.connectionString = connectionString;
			providerFactory = DbProviderFactories.GetFactory(providerName);
		}

		public void Add(User user)
		{
			using (DbConnection connection = providerFactory.CreateConnection())
			using (DbCommand command = connection.CreateCommand())
			{
				string query = $"INSERT INTO Users (Id,UserName_,Password,PhoneNumber,Email)" +
						$"VALUES (" +
						$"@Id, " +
						$"@Login," +
						$"@Password," +
						$"@PhoneNumber," +
						$"@Email);";
				command.CommandText = query;

				DbParameter parameter = providerFactory.CreateParameter();
				parameter.DbType = System.Data.DbType.Guid;
				parameter.ParameterName = "@Id";
				parameter.Value = user.Id;
				command.Parameters.Add(parameter);

				parameter = new SqlParameter();
				parameter.DbType = System.Data.DbType.String;
				parameter.ParameterName = "@Login";
				parameter.Value = user.Login;
				command.Parameters.Add(parameter);

				parameter = new SqlParameter();
				parameter.DbType = System.Data.DbType.String;
				parameter.ParameterName = "@Password";
				parameter.Value = user.Password;
				command.Parameters.Add(parameter);

				parameter = new SqlParameter();
				parameter.DbType = System.Data.DbType.String;
				parameter.ParameterName = "@PhoneNumber";
				parameter.Value = user.PhoneNumber;
				command.Parameters.Add(parameter);

				parameter = new SqlParameter();
				parameter.DbType = System.Data.DbType.String;
				parameter.ParameterName = "@Email";
				parameter.Value = user.Email;
				command.Parameters.Add(parameter);

				connection.ConnectionString = connectionString;
				connection.Open();
				command.ExecuteNonQuery();	

				//using (DbTransaction transaction = connection.BeginTransaction())
				//{
				//	try
				//	{
				//		command.Transaction = transaction;

				//		transaction.Commit();
				//	}
				//	catch
				//	{
				//		transaction.Rollback();
				//	}
				//}
			}
		}

		public void Delete(string userId)
		{
			throw new System.NotImplementedException();
		}

		public ICollection<User> GetAll()
		{
			using (DbConnection connection = providerFactory.CreateConnection())
			using (DbCommand command = connection.CreateCommand())
			{
				string query = "SELECT * FROM Users";
				command.CommandText = query;

				connection.ConnectionString = connectionString;
				connection.Open();
				DbDataReader dataReader = command.ExecuteReader();

				List<User> users = new List<User>();
				while (dataReader.Read())
				{
					users.Add(new User
					{
						Id = Guid.Parse(dataReader["Id"].ToString()),
						Login = dataReader["UserName"].ToString(),
						Password = dataReader["Password"].ToString(),
						PhoneNumber = dataReader["PhoneNumber"].ToString(),
						Email = dataReader["Email"].ToString()
					});
				}
				return users;
			}
		}

		public void Update(User user)
		{
			throw new System.NotImplementedException();
		}
	}
}
