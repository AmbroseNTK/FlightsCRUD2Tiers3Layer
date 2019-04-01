using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace FlightsCRUD2Tiers3Layer.DAL
{
    class DBConnection
    {
        public SqlConnection Connection; 
        private DBConnection()
        {
            try
            {
                Connection = new SqlConnection(@"Server=35.194.193.246;Database=FlightsDB;User Id=sa;Password=WM>nf:$FnwpUNo,");
                Connection.Open();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        private static DBConnection instance;
        public static DBConnection Instance
        {
            get
            {
                if(instance== null)
                {
                    instance = new DBConnection();
                }
                return instance;
            }
        }
    }
}
