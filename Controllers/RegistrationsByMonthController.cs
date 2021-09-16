using Microsoft.AspNetCore.Mvc;
using ContosoIncAPI.Repositories;
using System.Collections.Generic;
using ContosoIncAPI.Entities;
using System.Web;
using System.Linq;
using System;
using System.Globalization;

namespace ContosoIncAPI.Controllers
{
    [ApiController]
    [Route("/api/registration/bymonth")]
    public class RegistrationsByMonthController : ControllerBase
    {
        private readonly RegistrationsByMonthRepository regByMonthRepo;

        public RegistrationsByMonthController()
        {
            regByMonthRepo = new RegistrationsByMonthRepository();
        }

        [HttpGet]
        public ActionResult<IEnumerable<RegistrationByMonth>> GetUsersByMonth()
        {
            var response = regByMonthRepo.GetUsersByMonth(DateTime.Now).ToList();
            
            return response.Count == 0 ? NotFound() : response;
        }

        [HttpGet("{date}")]
        public ActionResult<IEnumerable<RegistrationByMonth>> GetUsersByMonth(string date)
        {
            DateTime dateParsed = default;

            try
            {
                dateParsed = DateTime.ParseExact(date, "yyyyMM", CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                return NotFound();
            }

            var response = regByMonthRepo.GetUsersByMonth(dateParsed).ToList();

            return response.Count == 0 ? NotFound() : response;
        }
    }
}