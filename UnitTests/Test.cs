using System;
using ContosoIncAPI.Controllers;
using NUnit.Framework;

namespace ContosoIncAPI.UnitTests
{
	// almost everything in the code is related to databases. Since databases can never be static
	// (new data is added, and old data is altered), these unit tests are only applied to general
	// requirements of loading methods, like results never being null, etc.
	public class Test
	{
		[Test]
		public void TestDatabase()
		{
			Assert.AreEqual(Database.LoadUsers(DateTime.MinValue).Count, 0);
			Assert.AreNotEqual(Database.LoadUsers(DateTime.MaxValue), null);
			
			Assert.AreEqual(Database.LoadSessionCounts("invalid", "date").Count, 0);
			
			Assert.AreNotEqual(Database.LoadConcurrentLogins(), null);
		}
		
		[Test]
		public void TestControllers()
		{
			Assert.AreEqual(new UserController().GetUser("invalid date").Value, null);
			Assert.AreEqual(new LoginStatsController().GetSessions("invalid", "date"), "[]");
			
			// Assert.AreEqual(new LoginLocationController().GetLoginsFromUnseenCountries(), "[]"); // may not be true
		}
	}
}