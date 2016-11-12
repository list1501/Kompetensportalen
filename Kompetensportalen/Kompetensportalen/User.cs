using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace Kompetensportalen
{
    public class User
    {
        //Automatic properties
        public string username { get; set; }
        public int usertype { get; set; }
        public bool qualified { get; set; }
        public DateTime lastTestDate { get; set; }
        public XmlDocument latestTest { get; set; }

        public List<Test> testHistory = new List<Test>();
        public Test newTest = new Test();
        SQL newSQL = new SQL();
        DateTime today = DateTime.Today;

        public void createTestHistory()
        {
     //       testHistory = newSQL.getTestHistory(username);
        }

        //Method to create new test
        public void createTest()
        {           
            DateTime testDate = lastTestDate;
            string user = username;

            if (testDate.Year != today.Year || testDate == null)
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
            else if (testDate.Year == today.Year)
            {
                getLastTest(user, testDate);
            }
        }

        public void getLastTest(string user, DateTime date)
        {
            string username = user;
            DateTime latestDate = date;
        }
    }
}