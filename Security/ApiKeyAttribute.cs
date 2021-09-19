using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContosoIncAPI.Security
{
	[AttributeUsage(AttributeTargets.Class)]
	public class ApiKeyAttribute : Attribute, IAsyncActionFilter
	{
		private const string ApiKeyName = "x-api-key"; // this string must be identical to the one in appsettings.json
		
		private readonly ContentResult _noKeyResult = new()
		{
			StatusCode = 401,
			Content = "Access denied: no API key!"
		};
		
		private readonly ContentResult _wrongKeyResult = new()
		{
			StatusCode = 403,
			Content = "Access denied: incorrect API key!"
		};
		
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyName, out var requestApiKey))
			{
				context.Result = _noKeyResult;
				return;
			}
 
			var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
			
			if (!appSettings.GetValue<string>(ApiKeyName).Equals(requestApiKey))
			{
				context.Result = _wrongKeyResult;
				return;
			}
 
			await next();
		}
	}
}