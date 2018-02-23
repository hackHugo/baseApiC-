using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.Response
{
    public class ReplyUser
    {
        public string message { set; get; }
        public int status { set; get; }
        public dataUser data { set; get; }
    }
    public class dataUser
    {
        public int id { set; get; }
        public string nombre { set; get; }
        public string email { set; get; }
    }
}