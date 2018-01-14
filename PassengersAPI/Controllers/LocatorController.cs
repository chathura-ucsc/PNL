using PassengersAPI.Models;
using PassengersAPI.Repository;
using PassengersAPI.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PassengersAPI.Controllers
{
    /// <summary>
    ///  This controller is responsible for Locator related operations
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LocatorController : ApiController
    {
        private IPassengerSupport _passengerSupport;

        /// <summary>
        /// Constructor with dependency injection
        /// </summary>
        /// <param name="passengerSupport"></param>
        public LocatorController(IPassengerSupport passengerSupport)
        {
            _passengerSupport = passengerSupport;
        }       

        // GET
        /// <summary>
        /// This finds the Locator names which are similar to the given 'name' parameter from the Passenger Name List (PNL) and return those Locators with the Passengers associated with them.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Locator> Get(string name)
        {
            return _passengerSupport.GetByLocator(name);
        }

    }
}
