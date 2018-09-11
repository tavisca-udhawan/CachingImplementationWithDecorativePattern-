using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static BookingApi.Authentication.AuthenticateProvider;

namespace BookingApi.Authentication
{
    public class User
    {
        public Guid Id { get; set; }
        public UserType? UserType { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}