using PassengersAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassengersAPI.Repository
{
    public interface IRepository
    {
        List<Passenger> Passengers { get; }
        int SaveChanges();
    }
}
