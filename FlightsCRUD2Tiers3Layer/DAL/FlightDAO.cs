using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

using FlightsCRUD2Tiers3Layer.DTO;

namespace FlightsCRUD2Tiers3Layer.DAL
{
    class FlightDAO
    {
        SqlCommand command;
        public List<Flight> SelectAll()
        {
            List<Flight> result = new List<Flight>();
            try
            {
            
                command = new SqlCommand("SELECT * FROM Flight", DBConnection.Instance.Connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new Flight()
                    {
                        ID = reader.GetInt32(0),
                        Code = reader.GetString(1),
                        ArrivalAirport = reader.GetString(2),
                        DepatureAirport = reader.GetString(3),
                        ArrivalGate = reader.GetString(4),
                        DepatureGate = reader.GetString(5),
                        Date = reader.GetString(6),
                        CheckInTime = reader.GetString(7)
                    });
                }
                reader.Close();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public void Insert(Flight flight)
        {
            try
            {
                command = new SqlCommand("INSERT INTO Flight VALUES(@Code,@ArrAir,@DeptAir,@ArrGate,@DeptGate,@Date,@CheckInTime)",
                    DBConnection.Instance.Connection);
                command.Parameters.Add(new SqlParameter("Code", flight.Code));
                command.Parameters.Add(new SqlParameter("ArrAir", flight.ArrivalAirport));
                command.Parameters.Add(new SqlParameter("DeptAir", flight.DepatureAirport));
                command.Parameters.Add(new SqlParameter("ArrGate", flight.ArrivalGate));
                command.Parameters.Add(new SqlParameter("DeptGate", flight.DepatureGate));
                command.Parameters.Add(new SqlParameter("Date", flight.Date));
                command.Parameters.Add(new SqlParameter("CheckInTime", flight.CheckInTime));
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Update(Flight flight)
        {
            try
            {
                command = new SqlCommand("UPDATE Flight SET Code = @Code," +
                    " ArrivalAirport = @ArrAir," +
                    " DepatureAirport = @DeptAir," +
                    " ArrivalGate = @ArrGate," +
                    " DepatureGate = @DeptGate," +
                    " Date = @Date," +
                    " CheckInTime = @CheckInTime" +
                    " WHERE ID=@ID", DBConnection.Instance.Connection);
                command.Parameters.Add(new SqlParameter("Code", flight.Code));
                command.Parameters.Add(new SqlParameter("ArrAir", flight.ArrivalAirport));
                command.Parameters.Add(new SqlParameter("DeptAir", flight.DepatureAirport));
                command.Parameters.Add(new SqlParameter("ArrGate", flight.ArrivalGate));
                command.Parameters.Add(new SqlParameter("DeptGate", flight.DepatureGate));
                command.Parameters.Add(new SqlParameter("Date", flight.Date));
                command.Parameters.Add(new SqlParameter("CheckInTime", flight.CheckInTime));
                command.Parameters.Add(new SqlParameter("ID", flight.ID));
                command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void Delete(int id)
        {
            try
            {
                command = new SqlCommand("DELETE FROM Flight WHERE ID = @ID",DBConnection.Instance.Connection);
                command.Parameters.Add(new SqlParameter("ID", id));
                command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
