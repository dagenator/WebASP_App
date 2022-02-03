using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebASP_App.Models;

namespace WebASP_App.Controllers
{
    public class RegistrationController : Controller
    {
        public RegistrationController(UserContext context)
        {
            UsersData.SetDb(context);
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
       
        [HttpPost]
        public async Task<IActionResult> CreateNewUser(User user)
        {
            if (UsersData.IsUserInBDbyEmail(user.Email))
                return RedirectToAction("Error"); // to do: Предупреждение об ошибке, логин занят
            
            UsersData.AddUser(user);
            return RedirectToAction("Index","Authorization");
        }
    }
}
