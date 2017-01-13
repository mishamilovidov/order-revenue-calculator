using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace shopify_internship
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("/***** SHOPIFY ORDER STATISTICS *****/");   

            // read JSON directly from a file
            using (StreamReader file = File.OpenText(@"orders.json"))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject rss = (JObject)JToken.ReadFrom(reader);
                string json = JsonConvert.SerializeObject(rss);
                float totalOrderRevenue = 0;
                int orderCount = 0;

                //Query json file with Json.NET to get total price for each order
                var totalPrices =
                    from p in rss["orders"]
                    select (float)p["total_price"];
                
                //Loop through each total price and sum up the revenue
                foreach (var item in totalPrices)
                {
                    totalOrderRevenue = item + totalOrderRevenue;
                    orderCount++;
                }

                //Display the number of order and the the total order revenue
                Console.WriteLine();
                Console.WriteLine("ORDER COUNT: \t\t" + orderCount);
                Console.WriteLine("TOTAL ORDER REVENUE: \t$" + Math.Round(totalOrderRevenue, 2));
                Console.WriteLine();
            }            
        }
    }
}
