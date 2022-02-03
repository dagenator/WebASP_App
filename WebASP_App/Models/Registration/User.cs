using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebASP_App.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Email {  set; get; }
        public string Password { set; get; }
        
    }
}

    

