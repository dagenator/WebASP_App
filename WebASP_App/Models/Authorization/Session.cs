using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebASP_App.Models
{
    public class Session
    {
        public int UserId { set; get; }
        [Key]
        public string SessionId { set; get; }
    }
}
