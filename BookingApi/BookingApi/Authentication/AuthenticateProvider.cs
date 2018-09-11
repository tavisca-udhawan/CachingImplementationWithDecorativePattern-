using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace BookingApi.Authentication
{
    public class AuthenticateProvider
    {
        private string _username;
        private string _password;
        public enum UserType
        {
            admin,
            client
        }
        public AuthenticateProvider(string username, string password)
        {
            _username = username;
            _password = password;
        }

        public UserType? Authenticate()
        {
            if (_username != string.Empty || _password != null)
            {
                var password = ConfigurationManager.AppSettings[_username.ToLower()];
                if (string.Equals(_password, password))
                {
                    return string.Equals(_username, UserType.admin.ToString(),
                        StringComparison.InvariantCultureIgnoreCase) ? UserType.admin : UserType.client;
                }
            }
            return null;
        }
    }
}