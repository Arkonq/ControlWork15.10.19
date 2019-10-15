using Avia.DataAccess;
using Avia.DataAccess.Abstract;
using Avia.Domain;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace ControlWork
{
	class Program
	{
		static IConfigurationBuilder builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", false, true);

		static IConfigurationRoot configurationRoot = builder.Build();
		static readonly string connectionString = configurationRoot.GetConnectionString("DebugConnectionString");
		static readonly string providerName = configurationRoot.GetSection("AppConfig").GetChildren().Single().Value;

		static void Menu()
		{
			int answ;
			Console.WriteLine("1.Регистрация");
			Console.WriteLine("2.Вход");
			Console.WriteLine("\tВыберите пункт: ");
			answ = Int32.Parse(Console.ReadLine());
			switch (answ)
			{
				case 1:
					Console.Clear();
					Registration();
					Console.WriteLine("Пользователь успешно создан!");
					Console.ReadKey();
					break;
				case 2:
					Console.Clear();

					Console.WriteLine("!");
					Console.ReadKey();
					break;
			}
		}
		static void Registration()
		{
			string UserName, Password, PhoneNumber, Email;
			Console.WriteLine("Введите имя пользователя: ");
			UserName = Console.ReadLine();
			Console.WriteLine("Введите пароль: ");
			Password = Console.ReadLine();
			Console.WriteLine("Введите номер телефона (+7-123-456-78-90): ");
			PhoneNumber = Console.ReadLine();
			Console.WriteLine("Введите почту: ");
			Email = Console.ReadLine();

			User user = new User
			{
				Login = UserName,
				Password = Password,
				PhoneNumber = PhoneNumber,
				Email = Email
			};
			IUserRepository repository = new UserRepository(connectionString, providerName);
			repository.Add(user);
		}

		static void Main()
		{
			DbProviderFactories.RegisterFactory(providerName, SqlClientFactory.Instance);

			User user = new User
			{
				Login = "Macaron",
				Password = "qwerty123",
				PhoneNumber = "8-800-555-35-35",
				Email = "pochta@example.com"
			};

			IUserRepository repository = new UserRepository(connectionString, providerName);
			repository.Add(user);
			var res = repository.GetAll();

			//Menu();
		}
	}
}
