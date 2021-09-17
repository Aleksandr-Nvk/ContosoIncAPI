using Microsoft.AspNetCore.Mvc;
using ContosoIncAPI.Entities;
using System;
using System.Globalization;
using System.Linq;

namespace ContosoIncAPI.Controllers
{
    [ApiController]
    [Route("/api/registration/bymonth")]
    public class UserByDateController : ControllerBase
    {

        [HttpGet]
        public ActionResult<User> GetUserByDate()
        {
            var response = Database.QueryUsersByDate(DateTime.Now).FirstOrDefault();
            
            return response == null ? NotFound() : response;
        }

        [HttpGet("{date}")]
        public ActionResult<User> GetUserByDate(string date)
        {
            DateTime dateParsed;

            try
            {
                dateParsed = DateTime.ParseExact(date, "yyyyMM", CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                return NotFound();
            }

            var response = Database.QueryUsersByDate(dateParsed).FirstOrDefault();
            
            if (response != null)
            {
                response.registeredDevices = Database.QueryDevicesByDate(dateParsed);
            }
            else
            {
                return NotFound();
            }

            return response;
        }
    }
}