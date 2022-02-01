using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebASP_App.Models;

namespace WebASP_App.Controllers
{
    public class AuthorizationController : Controller
    {
        public AuthorizationController(SessionContext session, UserContext user)
        {
            SessionsData.SetDb(session);
            UsersData.SetDb(user);
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DoAuthorization(User user)
        {
            if (UsersData.IsUserInBDbyEmail(user.Email))
            {
                if (UsersData.IsPasswordCorrect(user))
                {
                    byte[] cookie;
                    var sessionValue = HttpContext.Session.Id;
                    HttpContext.Session.Set("asp_test_key", Encoding.ASCII.GetBytes(sessionValue.ToString()));
                    var newUser = UsersData.GetUserByEmail(user.Email);
                    await SessionsData.AddNewSession(newUser, sessionValue.ToString());
                    return RedirectToAction("Index", "Home");
                }
            }
            return   RedirectToAction("Error");
        }

        [HttpPost]
        public async Task<IActionResult> Exit(User user)
        {
            var cookie = HttpContext.Session.ToString();
            SessionsData.DeleteSession(user, cookie);
            return RedirectToAction("Index", "Home");

        }
    }
}
