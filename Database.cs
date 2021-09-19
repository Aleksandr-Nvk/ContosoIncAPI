using System.Collections.Generic;
using ContosoIncAPI.Entities;
using MySql.Data.MySqlClient;
using System.Globalization;
using System;

namespace ContosoIncAPI
{
	public static class Database
	{
		private const string ConnectionString = "Server=localhost, 3306; Database=contoso_inc; Uid=root; Pwd=f6e527xp;";

		public static List<User> QueryUserEntitiesByDate(DateTime date)
		{
			var result = new List<User>();

			using (var connection = new MySqlConnection(ConnectionString))
			{
				var month = new DateTime(2020, date.Month, 1).ToString("MMMM");
				var query = $"SELECT * FROM registrations_by_month WHERE year = {date.Year} AND month = '{month}';";
				
				var command = new MySqlCommand(query, connection);
				MySqlDataReader reader;
				
				try
				{
					connection.Open();
					reader = command.ExecuteReader();
				}
				catch (Exception)
				{
					return result;
				}
				
				while (reader.Read())
				{
					var user = new User
					{
						Year = reader.GetInt16(0),
						Month = reader.GetString(1),
						UsersNum = reader.GetUInt32(2)
					};
					
					result.Add(user);
				}
			}
			
			return result;
		}
		
		public static List<Device> QueryDeviceEntitiesByDate(DateTime date)
		{
			var result = new List<Device>();

			using (var connection = new MySqlConnection(ConnectionString))
			{
				var month = new DateTime(2020, date.Month, 1).ToString("MMMM");
				var query = $"SELECT * FROM device_registrations_by_type_and_month WHERE year = {date.Year} AND month = '{month}';";
				
				var command = new MySqlCommand(query, connection);
				MySqlDataReader reader;
				
				try
				{
					connection.Open();
					reader = command.ExecuteReader();
				}
				catch (Exception)
				{
					return result;
				}

				while (reader.Read())
				{
					var device = new Device
					{
						DeviceType = reader.GetString(2),
						UsersNum = reader.GetUInt32(3)
					};
					
					result.Add(device);
				}
			}
			
			return result;
		}
		
		public static List<SessionCount> QuerySessionCountEntitiesByDate(string startTime, string endTime)
		{
			var result = new List<SessionCount>();

			using (var connection = new MySqlConnection(ConnectionString))
			{
				var query = $"SELECT * FROM concurrent_sessions_by_hour WHERE hour >= '{startTime}' AND hour <= '{endTime}';";

				var command = new MySqlCommand(query, connection);

				MySqlDataReader reader;
				
				try
				{
					connection.Open();
					reader = command.ExecuteReader();
				}
				catch (Exception)
				{
					return result;
				}
				
				while (reader.Read())
				{
					var sessionCountTime = new SessionCount
					{
						Date = reader.GetDateTime(0),
						SessionsNum = reader.GetUInt32(1)
					};
					
					result.Add(sessionCountTime);
				}
			}
			
			return result;
		}
		
		public static List<SessionTime> QuerySessionTimeEntitiesByDate(DateTime time)
		{
			var result = new List<SessionTime>();

			using (var connection = new MySqlConnection(ConnectionString))
			{
				var query = $"SELECT * FROM session_duration_by_hour WHERE date = '{time.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}' AND hour = {time.Hour};";

				var command = new MySqlCommand(query, connection);

				MySqlDataReader reader;
				
				try
				{
					connection.Open();
					reader = command.ExecuteReader();
				}
				catch (Exception)
				{
					return result;
				}
				
				while (reader.Read())
				{
					var sessionTime = new SessionTime
					{
						Date = reader.GetDateTime(0),
						Hour = reader.GetUInt32(1),
						Duration = reader.GetUInt32(2),
						DurationAccumulated = reader.GetUInt32(3),
					};
					
					result.Add(sessionTime);
				}
			}
			
			return result;
		}
		
		public static List<ConcurrentLogin> QueryConcurrentLogins()
		{
			var result = new List<ConcurrentLogin>();

			using (var connection = new MySqlConnection(ConnectionString))
			{
				var query = "SELECT * FROM concurrent_users_from_multiple_devices;";

				var command = new MySqlCommand(query, connection);

				MySqlDataReader reader;
				
				try
				{
					connection.Open();
					reader = command.ExecuteReader();
				}
				catch (Exception)
				{
					return result;
				}
				
				while (reader.Read())
				{
					var login = new ConcurrentLogin
					{
						UserName = reader.GetString(0),
						DeviceName = reader.GetString(1),
						LoginTs = reader.GetDateTime(2)
					};
					
					result.Add(login);
				}
			}
			
			return result;
		}

		public static List<UnseenCountryLogin> QueryUnseenCountryLoginByNameAndTime(string userName, DateTime time)
		{
			var result = new List<UnseenCountryLogin>();

			using (var connection = new MySqlConnection(ConnectionString))
			{
				Console.WriteLine(time.ToString("s", CultureInfo.InvariantCulture));
				var query = $"SELECT * FROM users_by_unseen_country WHERE user_name = '{userName}' AND login_ts ='{time.ToString("s", CultureInfo.InvariantCulture)}';";

				var command = new MySqlCommand(query, connection);
				connection.Open();

				MySqlDataReader reader;
				
				try
				{
					reader = command.ExecuteReader();
				}
				catch (Exception)
				{
					return result;
				}
				
				while (reader.Read())
				{
					var countryLogin = new UnseenCountryLogin
					{
						UserName = reader.GetString(0),
						Country = reader.GetString(1),
						LoginTs = reader.GetDateTime(2)
					};
					
					result.Add(countryLogin);
				}
			}
			
			return result;
		}
	}
}