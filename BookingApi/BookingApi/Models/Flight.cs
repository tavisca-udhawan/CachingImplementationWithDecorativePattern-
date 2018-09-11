using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingApi.Models
{
    public class Flight
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Arrival { get; set; }
        public string Departure { get; set; }
        public int Cost { get; set; }
        public string time { get; set; }
    }
}