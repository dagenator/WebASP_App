using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Net;
using System.Web;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Specialized;

namespace WebASP_App.Models
{
    public class CoinMarketApi
    {
        private readonly string API_KEY;
        //Настоящие данные
        private string cryptocurrency_listings_latest = "https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest";
        private string cryptocurrency_info = "https://pro-api.coinmarketcap.com/v1/cryptocurrency/info";
        //Тестовые данные
        //var URL = new UriBuilder("https://sandbox-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");

        public CoinMarketApi(string key)
        {
            API_KEY = key;
        }

        private string makeAPICall(string url, NameValueCollection query = null)
        {
            var URL = new UriBuilder(url);

            if (query != null)
                URL.Query = query.ToString();

            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY);
            client.Headers.Add("Accepts", "application/json");
            var res = client.DownloadData(URL.ToString());
            return System.Text.Encoding.Default.GetString(res);
        }

        private string makeAPICallWithParametres(string url, Dictionary<string, string> param)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            foreach (var p in param)
            {
                queryString[p.Key] = p.Value;
            }
            return makeAPICall(url, queryString);
        }

        public CoinJsonRootClass GetJoinObjectsFromJson(int start = 1, int limit = 50)
        {
            string mainJsonText = makeAPICallWithParametres(cryptocurrency_listings_latest,
                new Dictionary<string, string> {
                    {"start",start.ToString()},
                    {"limit", limit.ToString()},
                    {"convert", "USD"}
                });

            var mainRoot = JsonConvert.DeserializeObject<CoinJsonRootClass>(mainJsonText);
            var idList = mainRoot.data.Select(x => x.id);

            var LogoJsonText = makeAPICallWithParametres(cryptocurrency_info,
                new Dictionary<string, string> { { "id", string.Join(",", idList) } });

            var logoJObject = JObject.Parse(LogoJsonText);
            foreach (var item in mainRoot.data)
            {
                item.SetLogo(logoJObject["data"][item.id.ToString()]["logo"].ToString());
            }
            return mainRoot;
        }
    }
}
