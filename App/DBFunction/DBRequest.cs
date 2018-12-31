using Microsoft.EntityFrameworkCore;
using ParkAndRide.App.ActionModel;
using ParkAndRide.Models;
using ParkAndRide.Models.App;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace ParkAndRide.App.DBFunction
{
    public class DBRequest
    {
        private static ParkAndRideContext db = new ParkAndRideContext();

        public static async Task<List<StatisticModel>> getStatisticsModel(string query)
        {
            List<StatisticModel> statistics = new List<StatisticModel>();
            var conn = db.Database.GetDbConnection();
            string result = null;
            try
            {
                await conn.OpenAsync();
                using (var command = conn.CreateCommand())
                {
                    //string query = "SELECT * FROM FN_free_places_statistic('P+R Metro Młociny')";
                    command.CommandText = query;
                    DbDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            result = reader.ToString();

                            statistics.Add(new StatisticModel()
                            {
                                DayOfWeek = reader.GetInt32(0),
                                Hour = reader.GetInt32(1),
                                Minutes = reader.GetInt32(2),
                                AvgNumberOfFreePlaces = reader.GetInt32(3)
                            });

                            Console.WriteLine();

                        }
                    }
                    reader.Dispose();
                }
            }catch(Exception ex)
            {
                Console.WriteLine("DBRequest getStatisticsModel" + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return statistics;
        }
    }
}
