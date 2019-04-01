using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using FlightsCRUD2Tiers3Layer.DAL;
using FlightsCRUD2Tiers3Layer.DTO;

namespace FlightsCRUD2Tiers3Layer.BLL
{
    class FlightBUS
    {
        private FlightDAO flightDAO;
        public FlightBUS()
        {
            flightDAO = new FlightDAO();
        }
        public List<Flight> GetAll()
        {
            return flightDAO.SelectAll();
        }
        public void Add(Flight flight)
        {
            try
            {
                flightDAO.Insert(flight);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void Update(Flight flight)
        {
            try
            {
                flightDAO.Update(flight);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void Remove(int flightID)
        {
            try
            {
                flightDAO.Delete(flightID);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public List<Flight> Search(string expression)
        {
            List<Flight> data = flightDAO.SelectAll();
            List<string> rawList = new List<string>();
            List<int> selection = new List<int>();

            // Parse flight list to raw text list
            foreach(Flight flight in data)
            {
                rawList.Add(flight.ToString());
            }

            string[] commands = expression.Split(";");
            List<int> temp;
            Regex regex;
            foreach(string command in commands)
            {
                regex = new Regex(command,RegexOptions.IgnoreCase);
                temp = new List<int>();
                for(int i = 0; i < rawList.Count; i++)
                {
                    if (regex.IsMatch(rawList[i]))
                    {
                        if (!temp.Contains(i))
                        {
                            temp.Add(i);
                        }
                    }
                }
                if (selection.Count == 0)
                {
                    selection.AddRange(temp);
                }
                else
                {
                    selection = selection.Intersect(temp).ToList<int>();
                }
            }

            //Create final result

            List<Flight> result = new List<Flight>();
            for(int i = 0; i < selection.Count; i++)
            {
                result.Add(data[selection[i]]);
            }

            return result;
        }
        
    }
}
