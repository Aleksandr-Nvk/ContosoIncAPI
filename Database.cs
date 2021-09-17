using System.Collections.Generic;
using ContosoIncAPI.Entities;
using MySql.Data.MySqlClient;
using System;

namespace ContosoIncAPI
{
	public static class Database
	{
		private const string ConnectionString = "Server=localhost, 3306; Database=contoso_inc; Uid=root; Pwd=f6e527xp;";

		public static IEnumerable<User> QueryUsersByDate(DateTime date)
		{
			var result = new List<User>();

			using (var connection = new MySqlConnection(ConnectionString))
			{
				var month = new DateTime(2020, date.Month, 1).ToString("MMMM");
				var query = $"select * from registrations_by_month where year = {date.Year} AND month = '{month}';";
				
				var command = new MySqlCommand(query, connection);
				connection.Open();
				
				var reader = command.ExecuteReader();
				
				while (reader.Read())
				{
					var user = new User
					{
						year = reader.GetInt16(0),
						month = reader.GetString(1),
						registeredUsers = reader.GetUInt32(2)
					};
					
					result.Add(user);
				}
			}
			
			return result;
		}
		
		public static IEnumerable<Device> QueryDevicesByDate(DateTime date)
		{
			var result = new List<Device>();

			using (var connection = new MySqlConnection(ConnectionString))
			{
				var month = new DateTime(2020, date.Month, 1).ToString("MMMM");
				var query = $"select * from device_registrations_by_type_and_month where year = {date.Year} AND month = '{month}';";
				
				var command = new MySqlCommand(query, connection);
				connection.Open();
				
				var reader = command.ExecuteReader();
				
				while (reader.Read())
				{
					var device = new Device
					{
						type = reader.GetString(2),
						value = reader.GetUInt32(3)
					};
					
					result.Add(device);
				}
			}
			
			return result;
		}
	}
}