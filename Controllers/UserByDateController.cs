using Microsoft.AspNetCore.Mvc;
using ContosoIncAPI.Entities;
using System.Globalization;
using System.Linq;
using System;

namespace ContosoIncAPI.Controllers
{
    [ApiController]
    [Route("/api/registration/bymonth")]
    public class UserByDateController : ControllerBase
    {
        [HttpGet, Route("{date?}")]
        public ActionResult<User> GetUserByDate(string date = null)
        {
            if (date == null)
            {
                var now = DateTime.Now;
                date = now.Year + (now.Month < 10 ? "0" : "") + now.Month;
            }
            
            DateTime dateParsed;

            try
            {
                dateParsed = DateTime.ParseExact(date, "yyyyMM", CultureInfo.InvariantCulture);
            }
            catch (Exception)
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