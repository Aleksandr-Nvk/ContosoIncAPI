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

		/// <summary>
		/// Performs a connection to the database. NOTE: this is a lazy connection, and finalizers are responsible
		/// for disposing all unmanaged code and closing the connection.
		/// </summary>
		/// <param name="query">SQL query string</param>
		/// <returns>New MySQL data reader</returns>
		private static MySqlDataReader GetReader(string query)
		{
			var connection = new MySqlConnection(ConnectionString);
			var command = new MySqlCommand(query, connection);
			
			MySqlDataReader reader = null;
				
			try
			{
				connection.Open();
				reader = command.ExecuteReader();
			}
			catch (Exception) 
			{
				// data is unavailable due to a connection fault, ignore
			}

			return reader;
		}
		
		/// <summary>
		/// Returns a list of users logged in by date.
		/// </summary>
		/// <param name="date">Date of login</param>
		/// <returns>List of users</returns>
		public static List<User> LoadUsers(DateTime date)
		{
			var users = new List<User>();

			var month = new DateTime(2020, date.Month, 1).ToString("MMMM");
			var query = $@"SELECT * FROM registrations_by_month 
						   WHERE year = {date.Year} AND month = '{month}';";

			var reader = GetReader(query);

			if (reader == null) return users; // return an empty list if can't connect to the database

			while (reader.Read())
			{
				var user = new User
				{
					Year = reader.GetInt16(0),
					Month = reader.GetString(1),
					UsersNum = reader.GetUInt32(2)
				};
					
				users.Add(user);
			}

			return users;
		}
		
		/// <summary>
		/// Returns a list of devices logged from by date.
		/// </summary>
		/// <param name="date">Date of login</param>
		/// <returns>List of devices</returns>
		public static List<Device> LoadDevices(DateTime date)
		{
			var devices = new List<Device>();

			var month = new DateTime(2020, date.Month, 1).ToString("MMMM");
			var query = $@"SELECT * FROM device_registrations_by_type_and_month 
						   WHERE year = {date.Year} AND month = '{month}';";

			var reader = GetReader(query);

			if (reader == null) return devices; // return an empty list if can't connect to the database

			while (reader.Read())
			{
				var device = new Device
				{
					DeviceType = reader.GetString(2),
					UsersNum = reader.GetUInt32(3)
				};
					
				devices.Add(device);
			}

			return devices;
		}
		
		/// <summary>
		/// Returns a list of session entries (amount) in range of time.
		/// </summary>
		/// <param name="startTime">Time to begin search from</param>
		/// <param name="endTime">Time to end search at</param>
		/// <returns>List of session entries (amount)</returns>
		public static List<SessionCount> LoadSessionCounts(string startTime, string endTime)
		{
			var sessionCounts = new List<SessionCount>();

			var query = $@"SELECT * FROM concurrent_sessions_by_hour 
			               WHERE hour >= '{startTime}' AND hour <= '{endTime}';";

			var reader = GetReader(query);

			if (reader == null) return sessionCounts; // return an empty list if can't connect to the database
				
			while (reader.Read())
			{
				var sessionCountTime = new SessionCount
				{
					Date = reader.GetDateTime(0),
					SessionsNum = reader.GetUInt32(1)
				};
					
				sessionCounts.Add(sessionCountTime);
			}

			return sessionCounts;
		}
		
		/// <summary>
		/// Returns a list of session entries (time) by date.
		/// </summary>
		/// <param name="time">Time to search logins at</param>
		/// <returns>List of session entries (time)</returns>
		public static List<SessionTime> LoadSessionTimes(DateTime time)
		{
			var sessionTimes = new List<SessionTime>();

			var timeString = time.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
			var query = $@"SELECT * FROM session_duration_by_hour 
			               WHERE date = '{timeString}' AND hour = {time.Hour};";

			var reader = GetReader(query);

			if (reader == null) return sessionTimes; // return an empty list if can't connect to the database
				
			while (reader.Read())
			{
				var sessionTime = new SessionTime
				{
					Date = reader.GetDateTime(0),
					Hour = reader.GetUInt32(1),
					Duration = reader.GetUInt32(2),
					DurationAccumulated = reader.GetUInt32(3),
				};
					
				sessionTimes.Add(sessionTime);
			}

			return sessionTimes;
		}
		
		/// <summary>
		/// Returns a list of concurrent login entries.
		/// </summary>
		/// <returns>List of concurrent login entries</returns>
		public static List<ConcurrentLogin> LoadConcurrentLogins()
		{
			var concurrentLogins = new List<ConcurrentLogin>();

			var query = $@"SELECT * FROM concurrent_users_from_multiple_devices;";
			
			var reader = GetReader(query);

			if (reader == null) return concurrentLogins; // return an empty list if can't connect to the database
				
			while (reader.Read())
			{
				var login = new ConcurrentLogin
				{
					UserName = reader.GetString(0),
					DeviceName = reader.GetString(1),
					LoginTs = reader.GetDateTime(2)
				};
					
				concurrentLogins.Add(login);
			}
			
			return concurrentLogins;
		}

		/// <summary>
		/// Returns a list of login entries from unseen countries.
		/// </summary>
		/// <returns>List of login entries from unseen countries</returns>
		public static List<UnseenCountryLogin> LoadUnseenCountryLogins(string userName, DateTime time)
		{
			var unseenCountryLogins = new List<UnseenCountryLogin>();

			var timeString = time.ToString("s", CultureInfo.InvariantCulture);
			var query = $@"SELECT * FROM users_by_unseen_country 
			               WHERE user_name = '{userName}' AND login_ts ='{timeString}';";
			
			var reader = GetReader(query);

			if (reader == null) return unseenCountryLogins;
				
			while (reader.Read())
			{
				var countryLogin = new UnseenCountryLogin
				{
					UserName = reader.GetString(0),
					Country = reader.GetString(1),
					LoginTs = reader.GetDateTime(2)
				};
					
				unseenCountryLogins.Add(countryLogin);
			}

			return unseenCountryLogins;
		}
	}
}