using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kompetensportalen
{
    public class User
    {
        //Automatic properties
        string username { get; set; }
        int usertype { get; set; }
        DateTime latestTest { get; set; }
        bool passed { get; set; }

        List<Test> testHistory = new List<Test>();
    }
}