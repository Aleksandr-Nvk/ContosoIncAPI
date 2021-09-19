using Microsoft.AspNetCore.Mvc;
using ContosoIncAPI.Entities;
using System.Globalization;
using Newtonsoft.Json;
using System.Linq;
using System;

namespace ContosoIncAPI.Controllers
{
    [ApiController]
    [Route("/api/registration/bymonth")]
    public class UserController : ControllerBase
    {
        [HttpGet, Route("{date?}")]
        public ActionResult<string> GetUser(string date = null)
        {
            date ??= DateTime.Now.Year + (DateTime.Now.Month < 10 ? "0" : "") + DateTime.Now.Month;
            
            DateTime dateParsed;

            try
            {
                dateParsed = DateTime.ParseExact(date, "yyyyMM", CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                return NotFound(); // return 404 status code if date format was incorrect
            }

            var response = Database.LoadUsers(dateParsed).FirstOrDefault();
            
            if (response == null) return NotFound(); // return 404 status code if no data was found

            var result = new UserDevice
            {
                Year = response.Year,
                Month = response.Month,
                UsersNum = response.UsersNum,
                RegisteredDevices = Database.LoadDevices(dateParsed)
            };

            return JsonConvert.SerializeObject(result);
        }
    }
}