using System.Collections.Generic;
using ContosoIncAPI.Entities;
using System.Collections;
using System.Linq;
using System;
using System.Web;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using ContosoIncAPI.Repositories;

namespace ContosoIncAPI.Repositories
{
    public class RegistrationsByMonthRepository
    {
        private readonly List<RegistrationByMonth> usersByMonth = new()
        {
            new RegistrationByMonth {Year = 2020, Month = "September", UserNumber = 13},
            new RegistrationByMonth {Year = 2020, Month = "June", UserNumber = 9},
            new RegistrationByMonth {Year = 2021, Month = "May", UserNumber = 1},
            new RegistrationByMonth {Year = 2021, Month = "April", UserNumber = 66},
            new RegistrationByMonth {Year = 2021, Month = "November", UserNumber = 6},
        };

        public IEnumerable<RegistrationByMonth> GetUsersByMonth(DateTime date)
        {
            return usersByMonth.Where(x => 
                x.Year == date.Year && 
                DateTime.ParseExact(x.Month, "MMMM", CultureInfo.CurrentCulture).Month == date.Month);
        }
    }
}