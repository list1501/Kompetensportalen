using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kompetensportalen
{
    public class User
    {
        //Automatic properties
        public string username { get; set; }
        public int usertype { get; set; }
        public bool qualified { get; set; }
        public DateTime latestTest { get; set; }
        public bool passed { get; set; }

        List<Test> testHistory = new List<Test>();
    }
}