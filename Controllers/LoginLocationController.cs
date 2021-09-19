using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ContosoIncAPI.Entities;
using Newtonsoft.Json;
using System.Linq;
using ContosoIncAPI.Security;

namespace ContosoIncAPI.Controllers
{
	[ApiKey]
	[ApiController]
	[Route("/api/users/anomalies")]
	public class LoginLocationController : ControllerBase
	{
		[HttpGet]
		public string GetLoginsFromUnseenCountries()
		{
			var response = Database.LoadConcurrentLogins();
			
			var logins = new List<UnseenCountryConcurrentLogin>(response.Count);

			foreach (var entity in response)
			{
				logins.Add(new UnseenCountryConcurrentLogin
				{
					UserName = entity.UserName,
					DeviceName = entity.DeviceName,
					LoginTs = entity.LoginTs,

					UnseenCountryLogin = Database
						.LoadUnseenCountryLogins(entity.UserName, entity.LoginTs)
						.FirstOrDefault()
				});
			}

			return JsonConvert.SerializeObject(logins);
		}
	}
}