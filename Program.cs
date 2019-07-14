using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace etherium_mining_price
{
    class GetData
    {
        public static string GETTHHTPREQUEST(string URL)
        {
            try
            {
                ServicePointManager.SecurityProtocol =  SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                WebRequest wreq = WebRequest.Create(URL);
                wreq.Timeout = 60000;
                WebResponse wres = wreq.GetResponse();
                Stream datastream = wres.GetResponseStream();
                StreamReader SR = new StreamReader(datastream);
                string result = SR.ReadToEnd();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
    class Program
    {    
        static void Main(string[] args)
        {

                double neededPrice = 0;
                Stex[] JS ;
                string pricehttp = @"https://app.stocks.exchange/api2/prices";
                string JSON = GetData.GETTHHTPREQUEST(pricehttp);
                
                JS = JsonConvert.DeserializeObject<Stex[]>(JSON);
                int iMax = JS.Length;
                int i = iMax;
                
                do
                {
                    i--;
                } while (JS[i].MarketName != "CLO_BTC" && i >= 0);
                if (i + 1 < JS.Length)
                //    neededPrice = (double)JS[i].Buy;
                Console.WriteLine(JS[i].Buy);

            Console.WriteLine("Hello World!");
        }
    }
}
