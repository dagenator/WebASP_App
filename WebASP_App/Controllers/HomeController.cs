using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebASP_App.Models;

namespace WebASP_App.Controllers
{
    public class HomeController : Controller
    {
        private int ItemsToShow = 50;
        public HomeController(UserContext context, SessionContext session)
        {
            UsersData.SetDb(context);
            SessionsData.SetDb(session);
        }

        public async Task<IActionResult> Index()
        {
            byte[] cookie;
            if (HttpContext.Session.TryGetValue("asp_test_key", out cookie))
            {
                var newCookie = Encoding.ASCII.GetString(cookie);
                SessionsData.GetAllCurrentSessions();
                var user = SessionsData.GetUserBySession(newCookie);
                ViewData.Add("email", user.Email);
            }
            ViewData.Add("itemsAmount", ItemsToShow);
            CoinMarketApi coinApi = new CoinMarketApi("4f1661d5-efff-4d8d-aab7-a0c100d58573");
            return View(coinApi.GetJoinObjectsFromJson(1, ItemsToShow).data);
        }
    }
}
