using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingApi.Models
{
    public class Car
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Pick { get; set; }
        public string Drop { get; set; }
        public string PickDate { get; set; }
        public string DropDate { get; set; }
    }
}