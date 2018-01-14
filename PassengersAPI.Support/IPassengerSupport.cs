using PassengersAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassengersAPI.Support
{
    public interface IPassengerSupport
    {
        List<Locator> GetByPassenger(string pName);

        List<Locator> GetByLocator(string lName);

        int AddPassenger(Passenger passenger);
    }
}
