using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingApi.Models
{
    //[MetadataType(typeof(Product))]
    public class Hotel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double StarRating { get; set; }
        public string Address { get; set; }
    }
}