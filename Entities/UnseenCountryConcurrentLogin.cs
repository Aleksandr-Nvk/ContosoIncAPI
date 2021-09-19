using Newtonsoft.Json;

namespace ContosoIncAPI.Entities
{
	public record UnseenCountryConcurrentLogin : ConcurrentLogin
	{
		[JsonProperty(PropertyName = "unexpectedLogin", Order = 2)]
		public UnseenCountryLogin UnseenCountryLogin {get; init;}
	}
}