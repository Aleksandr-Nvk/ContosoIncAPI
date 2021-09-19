using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ContosoIncAPI.Entities;
using Newtonsoft.Json;
using System.Linq;

namespace ContosoIncAPI.Controllers
{
	[ApiController]
	[Route("/api/users/anomalies")]
	public class LoginLocationController : ControllerBase
	{
		[HttpGet]
		public string GetLoginsFromUnseenCountries()
		{
			var response = Database.QueryConcurrentLogins();
			var logins = new List<UnseenCountryConcurrentLogin>(response.Count);

			for (var i = 0; i < response.Count; i++)
			{
				logins.Add(new UnseenCountryConcurrentLogin
				{
					UserName = response[i].UserName,
					DeviceName = response[i].DeviceName,
					LoginTs = response[i].LoginTs,

					UnseenCountryLogin = Database
						.QueryUnseenCountryLoginByNameAndTime(response[i].UserName, response[i].LoginTs)
						.FirstOrDefault()
				});
			}

			return JsonConvert.SerializeObject(logins);
		}
	}
}