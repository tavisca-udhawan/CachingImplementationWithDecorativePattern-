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
    public class ActivityData
    {
        public static bool Add(Activity activity)
        {
            try
            {
                using (var connection = new SqlConnection("Data Source = TAVRENTDESK04; Initial Catalog = BookingApiDB; User ID = sa; Password = test123!@#"))
                {
                    connection.Open();
                    string command = "insert into Activity(Id,Name,Destination,startDate,lastDate) Values(@Id,@Name,@Destination,@startDate,@lastDate)";
                    SqlCommand cmd = new SqlCommand(command, connection);
                    cmd.Parameters.AddWithValue("@Id", Guid.NewGuid().ToString());
                    cmd.Parameters.AddWithValue("@Name", activity.Name);
                    cmd.Parameters.AddWithValue("@Destination", activity.Destination);
                    cmd.Parameters.AddWithValue("@startDate", activity.StartDate);
                    cmd.Parameters.AddWithValue("@lastDate", activity.LastDate);

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
        public static bool Book(Guid activityId)
        {
            try
            {
                using (var connection = new SqlConnection("Data Source = TAVRENTDESK04; Initial Catalog = BookingApiDB; User ID = sa; Password = test123!@#"))
                {

                    connection.Open();
                    string sql = "UPDATE Activity SET IsBook=@Booked WHERE Id = @ActivityId";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@Booked", true);
                    cmd.Parameters.AddWithValue("@ActivityId", activityId);
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

        public static bool Save(Guid activityId)
        {
            try
            {
                using (var connection = new SqlConnection("Data Source = TAVRENTDESK04; Initial Catalog = BookingApiDB; User ID = sa; Password = test123!@#"))
                {

                    connection.Open();
                    string sql = "UPDATE Activity SET IsSave=@isSave WHERE Id = @ActivityID";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@isSave", true);
                    cmd.Parameters.AddWithValue("@ActivityID", activityId);
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
        public static Activity Get(Guid activityId)
        {
            try
            {
                using (var connection = new SqlConnection("Data Source=TAVRENTDESK04;Initial Catalog=BookingApiDB;User ID=sa;Password=test123!@#"))
                {

                    connection.Open();
                    string command = "Select Id,Name,Destination,startDate,lastDate from Activity where Id = @ActivityID";
                    SqlCommand cmd = new SqlCommand(command, connection);
                    cmd.Parameters.AddWithValue("@ActivityID", activityId.ToString());
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Activity activity = new Activity()
                            {
                                Id = new Guid(reader["Id"].ToString()),
                                Name = reader["Name"].ToString(),
                                Destination = reader["Destination"].ToString(),
                                StartDate = reader["startDate"].ToString(),
                                LastDate = reader["lastDate"].ToString()
                            };
                            return activity;
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

        public static List<Activity> GetAll()
        {
            const string CacheKey = "activityProduct";
            ObjectCache cache = MemoryCache.Default;

            if (cache.Contains(CacheKey))
            {
                return (List<Activity>)cache.Get(CacheKey);
            }
            else
            {
                try
                {
                    using (var connection = new SqlConnection("Data Source=TAVRENTDESK04;Initial Catalog=BookingApiDB;User ID=sa;Password=test123!@#"))
                    {
                        List<Activity> activities = new List<Activity>();
                        connection.Open();
                        string command = "SELECT Id,Name,Destination,startDate,lastDate from Activity";
                        SqlCommand cmd = new SqlCommand(command, connection);
                        cmd.CommandType = CommandType.Text;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Activity activity = new Activity()
                                {
                                    Id = new Guid(reader["Id"].ToString()),
                                    Name = reader["Name"].ToString(),
                                    Destination = reader["Destination"].ToString(),
                                    StartDate = reader["startDate"].ToString(),
                                    LastDate = reader["lastDate"].ToString()
                                };
                                activities.Add(activity);
                            };
                        }
                        // Store data in the cache
                        CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
                        cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddHours(1.0);
                        cache.Add(CacheKey, activities, cacheItemPolicy);
                        return activities;
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