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
    public class UserByDateController : ControllerBase
    {
        [HttpGet, Route("{date?}")]
        public ActionResult<string> GetUserEntityByDate(string date = null)
        {
            date ??= DateTime.Now.Year + (DateTime.Now.Month < 10 ? "0" : "") + DateTime.Now.Month;
            
            DateTime dateParsed;

            try
            {
                dateParsed = DateTime.ParseExact(date, "yyyyMM", CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                return NotFound();
            }

            var response = Database.QueryUserEntitiesByDate(dateParsed).FirstOrDefault();
            UserDevice result;

            if (response != null)
            {
                result = new UserDevice
                {
                    Year = response.Year,
                    Month = response.Month,
                    UsersNum = response.UsersNum,
                    RegisteredDevices = Database.QueryDeviceEntitiesByDate(dateParsed)
                };
            }
            else
            {
                return NotFound();
            }

            return JsonConvert.SerializeObject(result);
        }
    }
}