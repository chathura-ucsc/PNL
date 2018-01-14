using System;
using System.Collections.Generic;
using System.Linq;
using PassengersAPI.Models;
using System.IO;
using System.Configuration;

namespace PassengersAPI.Repository
{
    public class FileRepository : IRepository
    {
        private string pnl;
        private StreamReader reader;
        private StreamWriter writer;
        private List<Passenger> inMemoryPassengers;

        /// <summary>
        /// When the filerepository object is create, pnl will be filled
        /// </summary>
        public FileRepository()
        {
            this.pnl = $"{ AppDomain.CurrentDomain.BaseDirectory}{ConfigurationManager.AppSettings["PNLFileLocation"]}"; // the file location is take from the app settings
        }

        /// <summary>
        /// Passenger property will return the passenger object list either from in memory of reading from the file (for the first time)
        /// </summary>
        public List<Passenger> Passengers
        {

            get
            {
                if (inMemoryPassengers == null)
                {

                    inMemoryPassengers = GetPassengersListFromFile();

                }
                return inMemoryPassengers;
            }
        }

        public int SaveChanges()
        {
            //if records are different, we can assure that some thing has been changed in the in-memory
            //since we support only addition of records, it should be an addition
            //with this, we can add more than one record, at once
            //new objects have always been added to the end of the list
            if (inMemoryPassengers.Count > GetPassengersListFromFile().Count) //addition of a record
            {
                var addedCount = inMemoryPassengers.Count - GetPassengersListFromFile().Count;

                var copyOfInMemory = inMemoryPassengers;

                copyOfInMemory.Reverse(); //reverse will make the added objects to the front                

                var addedPassengers = copyOfInMemory.Take(addedCount).ToList();

                return AddPassengerListToFile(addedPassengers);

            }

            return 0;
        }

        /// <summary>
        /// This will write the given passenger list to the file
        /// While writing to the file, the given format is followed
        /// Ex: 1ARNOLD/NIGERMR.L/MCCEFR
        /// </summary>
        /// <param name="passenger"></param>
        /// <returns></returns>
        private int AddPassengerListToFile(List<Passenger> passenger)
        {
            try
            {
                this.writer = new StreamWriter(pnl, true);

                foreach (var p in passenger)
                {
                    //Ex: 1ARNOLD/NIGERMR .L/MCCEFR
                    writer.WriteLine($"1{p.LastName}/{p.FirstName}{p.Title} .L/{p.LocatorName}");

                }

                writer.Close();
                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }

        }
        private List<Passenger> GetPassengersListFromFile()
        {
            this.reader = new StreamReader(pnl);
            List<Passenger> passengerList = new List<Passenger>();

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();

                //we only consider lines starting with 1
                if (line.FirstOrDefault() == '1')
                {
                    //Ex: 1ARNOLD/NIGERMR .L/MCCEFR
                    var firstSlash = line.IndexOf('/');
                    var firstDash = line.IndexOf('-');
                    var firstDot = line.IndexOf('.');
                    //in some lines -B2, -E2 etc. might not be there.
                    var mrs = firstDash == -1? line.IndexOf("MRS "): line.IndexOf("MRS-");
                    var mr = firstDash == -1 ? line.IndexOf("MR ") : line.IndexOf("MR-");

                    passengerList.Add(new Passenger
                    {  
                        
                        //NIGER
                        FirstName = line.Substring((firstSlash + 1), (mrs == -1 ? mr  : mrs) - firstSlash - 1),
                        //ARNOLD
                        LastName = line.Substring(1, (firstSlash - 1)),
                        //MR
                        Title = (mrs == -1 ?  "MR": "MRS"),
                        //MCCEFR - always locator length is 6 characters
                        LocatorName = line.Substring((firstDot + 3), 6)
                    });
                }
                else
                {
                    continue;
                }
            }
            reader.Close();
            return passengerList;
        }
    }


}
