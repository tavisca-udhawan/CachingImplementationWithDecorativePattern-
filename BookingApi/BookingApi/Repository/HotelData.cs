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
    public static class HotelData
    {
        public static bool Add(Hotel hotel)
        {
            try
            {
                using (var connection = new SqlConnection("Data Source = TAVRENTDESK04; Initial Catalog = BookingApiDB; User ID = sa; Password = test123!@#"))
                {
                    connection.Open();
                    string command = "INSERT INTO Hotel(Id,Name,Price,Address,StarRating) VALUES(@Id,@Name,@Price,@Address,@StarRating)";
                    SqlCommand cmd = new SqlCommand(command, connection);
                    cmd.Parameters.AddWithValue("@Id", Guid.NewGuid().ToString());
                    cmd.Parameters.AddWithValue("@Name", hotel.Name);
                    cmd.Parameters.AddWithValue("@Price", hotel.Price);
                    cmd.Parameters.AddWithValue("@Address", hotel.Address);
                    cmd.Parameters.AddWithValue("@StarRating", hotel.StarRating);

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
        public static bool Book(Guid hotelId)
        {
            try
            {
                using (var connection = new SqlConnection("Data Source = TAVRENTDESK04; Initial Catalog = BookingApiDB; User ID = sa; Password = test123!@#"))
                {

                    connection.Open();
                    string sql = "UPDATE Hotel SET IsBook=@Booked WHERE Id = @HotelId";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@Booked", true);
                    cmd.Parameters.AddWithValue("@HotelId", hotelId);
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

        public static bool Save(Guid hotelId)
        {
            try
            {
                using (var connection = new SqlConnection("Data Source = TAVRENTDESK04; Initial Catalog = BookingApiDB; User ID = sa; Password = test123!@#"))
                {

                    connection.Open();
                    string sql = "UPDATE Hotel SET IsSave=@isSave WHERE Id = @HotelID";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@isSave", true);
                    cmd.Parameters.AddWithValue("@HotelID", hotelId);
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
        public static Hotel Get(Guid hotelId)
        {
            try
            {
                using (var connection = new SqlConnection("Data Source=TAVRENTDESK04;Initial Catalog=BookingApiDB;User ID=sa;Password=test123!@#"))
                {

                    connection.Open();
                    string command = "Select Id,Name,Price,Address,StarRating from Hotel where Id = @HotelID";
                    SqlCommand cmd = new SqlCommand(command, connection);
                    cmd.Parameters.AddWithValue("@HotelID", hotelId.ToString());
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Hotel hotel = new Hotel()
                            {
                                Id = new Guid(reader["Id"].ToString()),
                                Address = reader["Address"].ToString(),
                                Name = reader["Name"].ToString(),
                                StarRating = Convert.ToInt32(reader["StarRating"]),
                                Price = Convert.ToInt32(reader["Price"])
                            };
                            return hotel;
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

        public static List<Hotel> GetAll()
        {
            const string CacheKey = "hotelProduct";
            ObjectCache cache = MemoryCache.Default;

            if (cache.Contains(CacheKey))
            {
                return (List<Hotel>)cache.Get(CacheKey);
            }
            else
            {
                try
                {
                    using (var connection = new SqlConnection("Data Source=TAVRENTDESK04;Initial Catalog=BookingApiDB;User ID=sa;Password=test123!@#"))
                    {
                        List<Hotel> hotels = new List<Hotel>();
                        connection.Open();
                        string command = "SELECT Id,Name,Price,Address,StarRating FROM Hotel";
                        SqlCommand cmd = new SqlCommand(command, connection);
                        cmd.CommandType = CommandType.Text;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Hotel hotel = new Hotel()
                                {
                                    Id = new Guid(reader["Id"].ToString()),
                                    Address = reader["Address"].ToString(),
                                    Name = reader["Name"].ToString(),
                                    StarRating = Convert.ToInt32(reader["StarRating"]),
                                    Price = Convert.ToInt32(reader["Price"])
                                };
                                hotels.Add(hotel);
                            };
                        }
                        // Store data in the cache
                        CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
                        cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddHours(1.0);
                        cache.Add(CacheKey, hotels, cacheItemPolicy);
                        return hotels;
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
