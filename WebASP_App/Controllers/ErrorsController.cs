using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebASP_App.Controllers
{
    public class ErrorsController : Controller
    {
        public IActionResult Index(string ErrorMessage)
        {
            return View(ErrorMessage);
        }
    }
}
