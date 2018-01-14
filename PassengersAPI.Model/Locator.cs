using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassengersAPI.Models
{
    public class Locator
    {
        public string Name { set; get; }

        public List<Passenger> Passengers { set; get; }
    }
}