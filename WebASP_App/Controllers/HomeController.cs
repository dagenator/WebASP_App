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
        
        public HomeController(UserContext context)
        {
            UsersData.SetDb(context);
        }

        public async Task<IActionResult> Index()
        {
            
            return View(UsersData.GetUsers());
        }

    }
}
