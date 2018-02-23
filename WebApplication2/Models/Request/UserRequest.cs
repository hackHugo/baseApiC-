using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.Request
{
    public class UserRequest
    {
        [Required,EmailAddress]
        public string email { set; get; }
        [Required]
        public string pass { set; get; }
    }
}