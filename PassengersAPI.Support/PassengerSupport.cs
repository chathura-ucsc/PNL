using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PassengersAPI.Models;
using PassengersAPI.Repository;

namespace PassengersAPI.Support
{
    public class PassengerSupport : IPassengerSupport
    {

        private IRepository repository;

        /// <summary>
        /// This will assign the correct repository
        /// </summary>
        /// <param name="repo"></param>
        public PassengerSupport(IRepository repo)
        {
            repository = repo;
        }

        /// <summary>
        /// Adding a passenger to the data source
        /// </summary>
        /// <param name="passenger"></param>
        /// <returns></returns>
        public int AddPassenger(Passenger passenger)
        {
            repository.Passengers.Add(passenger);
            return repository.SaveChanges();
        }

        /// <summary>
        /// This will get the passenger list, searched using the locator name
        /// list is grouped by Locators
        /// </summary>
        /// <param name="lName"></param>
        /// <returns></returns>
        public List<Locator> GetByLocator(string lName)
        {   
            //ToLower is for search without considering the Case
            return repository.Passengers
                  .Where(p => p.LocatorName.ToLower().Contains(lName.ToLower()))
                  .GroupBy(p => p.LocatorName)
                  .Select(l => new Locator
                  {
                      Name = l.Key,
                      Passengers = l.ToList(),
                  }).ToList();

        }

        /// <summary>
        /// This will get the passenger list name searched using any part of the passenger name
        /// list is grouped by Locators
        /// </summary>
        /// <param name="pName"></param>
        /// <returns></returns>
        public List<Locator> GetByPassenger(string pName)
        {
            //ToLower is for search without considering the Case
            return repository.Passengers
                  .Where(p => p.FirstName.ToLower().Contains(pName.ToLower()) || p.LastName.ToLower().Contains(pName.ToLower()))
                  .GroupBy(p => p.LocatorName)
                  .Select(l => new Locator
                  {
                      Name = l.Key,
                      Passengers = l.ToList(),
                  }).ToList();

        }
    }
}