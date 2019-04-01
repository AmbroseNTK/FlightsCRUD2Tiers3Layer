using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsCRUD2Tiers3Layer.DTO
{
    class Flight
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string ArrivalAirport { get; set; }
        public string DepatureAirport { get; set; }
        public string ArrivalGate { get; set; }
        public string DepatureGate { get; set; }
        public string Date { get; set; }
        public string CheckInTime { get; set; }

        public override string ToString()
        {
            return String.Format("ID={0} CODE={1} AA={2} AG={3} DA={4} DG={5} DATE={6} TIME={7}",
                    ID,
                    Code,
                    ArrivalAirport,
                    ArrivalGate,
                    DepatureAirport,
                    DepatureGate,
                    Date,
                    CheckInTime
                    );
        }

    }
}
