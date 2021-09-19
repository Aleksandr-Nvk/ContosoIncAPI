using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ContosoIncAPI.Entities;
using Newtonsoft.Json;
using System.Linq;

namespace ContosoIncAPI.Controllers
{
	[ApiController]
	[Route("/api/sessions/byhour")]
	public class LoginStatsByHourController : ControllerBase
	{
		[HttpGet]
		public string GetSessionEntityByDate(string startTime = "0001-01-01T12:00:00", 
											 string endTime = "9999-12-31T11:59:59")
		{
			var response = Database.QuerySessionCountEntitiesByDate(startTime, endTime);
			var sessionCounts = new List<SessionCount>(response.Count);
			
			foreach (var entry in response)
			{
				var sessionTime = Database.QuerySessionTimeEntitiesByDate(entry.Date).FirstOrDefault();
				if (sessionTime == null) continue;

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