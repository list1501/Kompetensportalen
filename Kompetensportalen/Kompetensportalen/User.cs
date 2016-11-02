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

        Test newTest = new Test();
        List<Test> testHistory = new List<Test>();
        SQL newSQL = new SQL();

        public void createTestHistory()
        {
            testHistory = newSQL.getTestHistory(username);
        }
    }
}