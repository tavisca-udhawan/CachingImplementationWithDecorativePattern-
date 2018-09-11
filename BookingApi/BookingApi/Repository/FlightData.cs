using BookingApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Caching;
using System.Web;

namespace BookingApi.Repository
{
    public class FlightData
    {
        public static bool Add(Flight flight)
        {
          
                try
                {
                    using (var connection = new SqlConnection("Data Source = TAVRENTDESK04; Initial Catalog = BookingApiDB; User ID = sa; Password = test123!@#"))
                    {
                        connection.Open();
                        string command = "insert into Flight(Id,name,startPoint,destination,arrival,departure,Cost,time) Values(@Id,@Name,@startPoint,@destination,@arrival,@departure,@Cost,@time)";
                        SqlCommand cmd = new SqlCommand(command, connection);
                        cmd.Parameters.AddWithValue("@Id", Guid.NewGuid().ToString());
                        cmd.Parameters.AddWithValue("@Name", flight.Name);
                        cmd.Parameters.AddWithValue("@startPoint", flight.From);
                        cmd.Parameters.AddWithValue("@destination", flight.To);
                        cmd.Parameters.AddWithValue("@arrival", flight.Arrival);
                        cmd.Parameters.AddWithValue("@departure", flight.Departure);
                        cmd.Parameters.AddWithValue("@Cost", flight.Cost);
                        cmd.Parameters.AddWithValue("@time", flight.time);
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            
        }
        public static bool Book(Guid flightId)
        {
            try
            {
                using (var connection = new SqlConnection("Data Source = TAVRENTDESK04; Initial Catalog = BookingApiDB; User ID = sa; Password = test123!@#"))
                {

                    connection.Open();
                    string command = "Update Flight SET IsBook=@Booked WHERE Id = @flightId";
                    SqlCommand cmd = new SqlCommand(command, connection);
                    cmd.Parameters.AddWithValue("@Booked", true);
                    cmd.Parameters.AddWithValue("@flightId", flightId);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool Save(Guid flightId)
        {
            try
            {
                using (var connection = new SqlConnection("Data Source = TAVRENTDESK04; Initial Catalog = BookingApiDB; User ID = sa; Password = test123!@#"))
                {

                    connection.Open();
                    string command = "Update Flight SET IsSave=@isSave WHERE Id = @FlightId";
                    SqlCommand cmd = new SqlCommand(command, connection);
                    cmd.Parameters.AddWithValue("@isSave", true);
                    cmd.Parameters.AddWithValue("@FlightId", flightId);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static Flight Get(Guid flightId)
        {
            try
            {
                using (var connection = new SqlConnection("Data Source=TAVRENTDESK04;Initial Catalog=BookingApiDB;User ID=sa;Password=test123!@#"))
                {

                    connection.Open();
                    string command = "Select Id,name,startPoint,destination,arrival,departure,Cost,time from Flight where Id = @flightID";
                    SqlCommand cmd = new SqlCommand(command, connection);
                    cmd.Parameters.AddWithValue("@flightID", flightId.ToString());
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Flight flight = new Flight()
                            {
                                Id = new Guid(reader["Id"].ToString()),
                                Name = reader["name"].ToString(),
                                From = reader["startPoint"].ToString(),
                                To = reader["destination"].ToString(),
                                Arrival =reader["arrival"].ToString(),
                                Departure= reader["departure"].ToString(),
                                Cost = Convert.ToInt32(reader["Cost"]),
                                time = reader["time"].ToString(),
                            };
                            return flight;
                        };
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<Flight> GetAll()
        {
            const string CacheKey = "flightProduct";
            ObjectCache cache = MemoryCache.Default;

            if (cache.Contains(CacheKey))
            {
                return (List<Flight>)cache.Get(CacheKey);
            }
            else
            {
                try
                {
                    using (var connection = new SqlConnection("Data Source=TAVRENTDESK04;Initial Catalog=BookingApiDB;User ID=sa;Password=test123!@#"))
                    {
                        List<Flight> flights = new List<Flight>();
                        connection.Open();
                        string command = "SELECT Id,name,startPoint,destination,arrival,departure,Cost,time from Flight";
                        SqlCommand cmd = new SqlCommand(command, connection);
                        cmd.CommandType = CommandType.Text;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Flight flight = new Flight()
                                {
                                    Id = new Guid(reader["Id"].ToString()),
                                    Name = reader["name"].ToString(),
                                    From = reader["startPoint"].ToString(),
                                    To = reader["destination"].ToString(),
                                    Arrival = reader["arrival"].ToString(),
                                    Departure = reader["departure"].ToString(),
                                    Cost = Convert.ToInt32(reader["Cost"]),
                                    time = reader["time"].ToString(),
                                };
                                flights.Add(flight);
                            };
                        }

                        // Store data in the cache
                        CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
                        cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddHours(1.0);
                        cache.Add(CacheKey, flights, cacheItemPolicy);

                        return flights;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        
        }
    }
}