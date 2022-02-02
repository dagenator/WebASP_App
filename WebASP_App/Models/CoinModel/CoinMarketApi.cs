using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Net;
using System.Web;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace WebASP_App.Models
{
    public class CoinMarketApi
    {
        private static string API_KEY = "4f1661d5-efff-4d8d-aab7-a0c100d58573";

        private static string makeAPICall(int start=1, int limit=1)
        {
            //Настоящие данные
            var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");
            //Тестовые данные
            //var URL = new UriBuilder("https://sandbox-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["start"] = start.ToString();
            queryString["limit"] = limit.ToString();
            queryString["convert"] = "USD";

            URL.Query = queryString.ToString();

            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY);
            client.Headers.Add("Accepts", "application/json");
            var res = client.DownloadData(URL.ToString());

            return System.Text.Encoding.Default.GetString(res);

        }

        public static CoinJsonRootClass GetObjectsFromJson(int itemsAmount, int itemsAmountInOneStep = 1)
        {
            itemsAmountInOneStep = itemsAmountInOneStep - 1; //Отправка эллементов идет включительно 
            CoinJsonRootClass root = new CoinJsonRootClass();
            root.data = new List<CoinInfo>();
            for (int i = 1; i < itemsAmount; i += itemsAmountInOneStep)
            {
                string jsonText = makeAPICall(i, (i + itemsAmountInOneStep) > itemsAmount ? itemsAmount : i + itemsAmountInOneStep);
                var tempRoot = JsonConvert.DeserializeObject<CoinJsonRootClass>(jsonText);
                root.data.AddRange(tempRoot.data);
                root.status = tempRoot.status;
                if (root.status.error_message != null)
                {
                    throw new Exception(root.status.error_message.ToString());
                }
            }
            return root;

            //string jsonText = makeAPICall(1,1);
            //return JsonConvert.DeserializeObject<CoinJsonRootClass>(jsonText);
        }
    }
}
