using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ContosoIncAPI.Entities;
using ContosoIncAPI.Security;
using Newtonsoft.Json;
using System.Linq;

namespace ContosoIncAPI.Controllers
{
	[ApiKey]
	[ApiController]
	[Route("/api/sessions/byhour")]
	public class LoginStatsController : ControllerBase
	{
		[HttpGet]
		public string GetSessions(string startTime = "0001-01-01T12:00:00", string endTime = "9999-12-31T11:59:59")
		{
			var response = Database.LoadSessionCounts(startTime, endTime);
			
			var sessionCounts = new List<SessionCount>(response.Count);
			
			foreach (var entry in response)
			{
				var sessionTime = Database.LoadSessionTimes(entry.Date).FirstOrDefault();
				
				if (sessionTime == null) continue; // skip if sessionTime was not found

				var sessionCountTime = new SessionCountTime
				{
					Date = entry.Date.ToString("yyyy-MM-dd"),
					SessionsNum = entry.SessionsNum,
					Hour = sessionTime.Hour,
					Duration = sessionTime.Duration,
					DurationAccumulated = sessionTime.DurationAccumulated
				};

				sessionCounts.Add(sessionCountTime);
			}
			
			return JsonConvert.SerializeObject(sessionCounts);
		}
	}
}