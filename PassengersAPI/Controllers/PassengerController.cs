using PassengersAPI.Models;
using PassengersAPI.Repository;
using PassengersAPI.Support;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PassengersAPI.Controllers
{
    /// <summary>
    ///  This controller is responsible for Passenger related operations
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PassengerController : ApiController
    {
        private IPassengerSupport _passengerSupport;

        /// <summary>
        /// Constructor with dependency injection
        /// </summary>
        /// <param name="passengerSupport"></param>        
        public PassengerController(IPassengerSupport passengerSupport)
        {
            _passengerSupport = passengerSupport;
        }

        // GET
        /// <summary>
        /// This finds the Passengers who have similar name as the 'name' parameter from the Passenger Name List (PNL) and return the result group by their Locator Names.   
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Locator> Get(string name)
        {
            return _passengerSupport.GetByPassenger(name);
        }

        //POST
        /// <summary>
        /// This will add the given passenger to the Passenger Name List (PNL).
        /// </summary>
        /// <param name="passenger"></param> 
        public HttpResponseMessage Post([FromBody]Passenger passenger)
        {
            if (ModelState.IsValid)
            {
                if (_passengerSupport.AddPassenger(passenger) == 0)
                {
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Failed to add the Passenger record.");
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }


    }
}