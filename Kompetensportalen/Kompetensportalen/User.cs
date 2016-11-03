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

        public List<Test> testHistory = new List<Test>();
        public Test newTest = new Test();
        SQL newSQL = new SQL();

        public void createTestHistory()
        {
            testHistory = newSQL.getTestHistory(username);
        }

        //Method to create new test
        public void createNewTest()
        {
            bool qual = qualified;
            int testType = 1;
            if (qual == true)
            {
                testType = 2;
            }
            else
            {
                testType = 1;
            }

            newTest.getQuestions(testType);
        }
    }
}