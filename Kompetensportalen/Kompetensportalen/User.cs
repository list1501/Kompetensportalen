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
        User currentUser = Loginpage.currentLogin;

        #region Get test for user
        //Method to get new test for user
        public void getNewTest()
        {

        }

        //Method to get user's latest test
        public void getLastTest()
        {
            string user = username;
            DateTime date = lastTestDate;
            latestTest = newSQL.getLastTest(user, date);
        }
        #endregion Get test for user

        //public void ShowXMLTestforUser()
        //{
        //    SQL newSQL = new SQL();
        //    //om currentUser är qualified ska testID 2 ut, som är Competency testet
        //    if (currentUser.qualified == true)
        //    {
        //        int testType = 2;
        //XmlDocument test = newSQL.DbToXml(testType);
        //List<Question> newQuestionsList = new List<Question>();
        //List<Answer> AnswerList = new List<Answer>();

        //        foreach (XmlNode xTest in test["Test"])
        //        {
        //            Question q = new Question();
        //q.category = Int32.Parse(xTest.Attributes["ID"].Value);
        //            q.catdescription = xTest["description"].Value;
        //            q.id = Int32.Parse(xTest.Attributes["ID"].Value);
        //            q.question = xTest["description"].Value;

        //           // labelQuestion.Text = xTest["description"].InnerText;

        //            foreach (XmlNode xA in xTest["Answer"])
        //            {
        //                Answer a = new Answer();
        //a.text = xA.InnerText;
        //                //a.correctOrNot = Int32.Parse(xA.Attributes["correct"].Value);

        //               // chBAnswers.Items.Add(xA.InnerText);
        //            }
        //            newQuestionsList.Add(q);
        //            // chBAnswers.DataBind();
        //        }
        //    }
        //    //om currentUser INTE är qualified ska testID 1 ut, som är qualification testet
        //    else if (currentUser.qualified == false)
        //    {
        //        XmlDocument test = newSQL.DbToXml((1));
        //        List<Question> newQuestionsList = new List<Question>();
        //        List<Answer> AnswerList = new List<Answer>();

        //        foreach (XmlNode xTest in test["Test"])
        //        {
        //            Question q = new Question();
        //            q.category = Int32.Parse(xTest.Attributes["ID"].Value);
        //            q.catdescription = xTest["description"].Value;
        //            q.id = Int32.Parse(xTest.Attributes["ID"].Value);
        //            q.question = xTest["description"].Value;

        //            //labelQuestion.Text = xTest["description"].InnerText;

        //            foreach (XmlNode xA in xTest["Answer"])
        //            {
        //                Answer a = new Answer();

        //                   a.text = xA.InnerText;
        //                //a.correctOrNot = Int32.Parse(xA.Attributes["correct"].Value);

        //                //chBAnswers.Items.Add(xA.InnerText);

        //            }
        //            newQuestionsList.Add(q);
        //            //chBAnswers.DataBind();
        //        }
        //    }
        //}

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