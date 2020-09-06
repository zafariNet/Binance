using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace Candle
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString =
                "Data Source=.;Initial Catalog=Candlesticks_v01;"
                + "Integrated Security=true";

            // Provide the query string with a parameter placeholder.
            string queryString =
                "SELECT top(1000) * from cndl.Candlesticks "
                + "WHERE IntervalID=@interval "
                + "AND stockid = 294 ORDER  BY [opentime] DESC  ";

            int paramValue = 6;
            var list = new List<Candlestick>();
            using SqlConnection connection =
                new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@interval", paramValue);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var candle=new Candlestick();
                    var ddd =double.Parse(reader["OpenPrice"].ToString());
                    candle.Id = long.Parse(reader["Id"].ToString() ?? string.Empty);
                    candle.OpenPrice = double.Parse(reader["OpenPrice"].ToString() ?? string.Empty);
                    candle.ClosePrice = double.Parse(reader["ClosePrice"].ToString() ?? string.Empty);
                    candle.HighPrice = double.Parse(reader["HighPrice"].ToString() ?? string.Empty);
                    candle.LowPrice = double.Parse(reader["LowPrice"].ToString() ?? string.Empty);
                    if (CandleDetector.IsHammer(candle))
                    {
                        DoUpdate(candle.Id);
                    }

                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void DoUpdate(long id)
        {
            using SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Candlesticks_v01;"
                                                        + "Integrated Security=true");
            con.Open();
            var command = "Update cndl.Candlesticks set type='Hammer' where id='" + id.ToString() + "'";
            using SqlCommand cmd = new SqlCommand(command, con);
            cmd.ExecuteNonQuery();
        }
    }
}
