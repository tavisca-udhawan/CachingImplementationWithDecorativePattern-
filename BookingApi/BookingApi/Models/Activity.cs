using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingApi.Models
{
    public class Activity
    {
        public Guid Id{ get; set; }
        public string Name { get; set; }
        public string Destination { get; set; }
        public string StartDate { get; set; }
        public string LastDate { get; set; }
    }
}