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
    public class CarData
    {
        public static bool Add(Car car)
        {
            try
            {
                using (var connection = new SqlConnection("Data Source = TAVRENTDESK04; Initial Catalog = BookingApiDB; User ID = sa; Password = test123!@#"))
                {
                    connection.Open();
                    string sql = "INSERT INTO Car(Id,Name,dropCity,Pick,DropDate,PickDate) VALUES(@Id,@Name,@Drop,@Pick,@DropDate,@PickDate)";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@Id", Guid.NewGuid().ToString());
                    cmd.Parameters.AddWithValue("@Name", car.Name);
                    cmd.Parameters.AddWithValue("@Pick", car.Pick);
                    cmd.Parameters.AddWithValue("@Drop", car.Drop);
                    cmd.Parameters.AddWithValue("@PickDate", car.PickDate);
                    cmd.Parameters.AddWithValue("@DropDate", car.DropDate);

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

        public static List<Car> GetAll()
        {
            const string CacheKey = "carProduct";
            ObjectCache cache = MemoryCache.Default;

            if (cache.Contains(CacheKey))
            {
                return (List<Car>)cache.Get(CacheKey);
            }
            else
            {
                try
                {
                    using (var connection = new SqlConnection("Data Source=TAVRENTDESK04;Initial Catalog=BookingApiDB;User ID=sa;Password=test123!@#"))
                    {
                        List<Car> cars = new List<Car>();
                        connection.Open();
                        string sql = "SELECT Id,Name,dropCity,Pick,DropDate,PickDate FROM Car";
                        SqlCommand cmd = new SqlCommand(sql, connection);
                        cmd.CommandType = CommandType.Text;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Car car = new Car()
                                {
                                    Id = new Guid(reader["Id"].ToString()),
                                    Name = reader["Name"].ToString(),
                                    Pick = reader["Pick"].ToString(),
                                    Drop = reader["dropCity"].ToString(),
                                    DropDate = reader["DropDate"].ToString(),
                                    PickDate = reader["PickDate"].ToString()

                                };
                                cars.Add(car);
                            };
                        }
                        return cars;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public static bool Book(Guid carId)
        {
            try
            {
                using (var connection = new SqlConnection("Data Source = TAVRENTDESK04; Initial Catalog = BookingApiDB; User ID = sa; Password = test123!@#"))
                {
                    connection.Open();
                    string command = "UPDATE Car SET IsBook=@isBook WHERE Id = @carId";
                    SqlCommand cmd = new SqlCommand(command, connection);
                    cmd.Parameters.AddWithValue("@isBook", true);
                    cmd.Parameters.AddWithValue("@carId", carId);
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

        public static bool Save(Guid carID)
        {
            try
            {
                using (var connection = new SqlConnection("Data Source = TAVRENTDESK04; Initial Catalog = BookingApiDB; User ID = sa; Password = test123!@#"))
                {

                    connection.Open();
                    string command = "UPDATE Car SET IsSave=@isSave WHERE Id = @carID";
                    SqlCommand cmd = new SqlCommand(command, connection);
                    cmd.Parameters.AddWithValue("@isSave", true);
                    cmd.Parameters.AddWithValue("@carID", carID);
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
    }
}