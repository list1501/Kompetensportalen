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
        

        //public List<Test> testHistory = new List<Test>();
        public Test newTest = new Test();
        public Test latestTest = new Test();
        SQL newSQL = new SQL();
        DateTime today = DateTime.Today;

        #region Get test for user

        //Method to get new test for user
        public void getNewTest(int type)
        {
            int testType = type;
            newTest.employee = username;
            newTest.date = today;
            newTest.testType = testType;

            //Create source file from XML stored in database
            newTest.sourceFile = newSQL.DbToXml(testType);

            #region Randomise questions and add to object from XML

            int n = 25;
            int c = 0;

            if (testType == 2)
            {
                n = 15;
            }
            else if (testType == 1)
            {
                n = 25;
            }

            //Read from XML to temp object before randomising
            List<Question> tempQList = new List<Question>();
            XmlDocument tempX = newTest.sourceFile;
            XmlNodeList xQList = tempX.SelectNodes("Test/question");
            
            foreach (XmlNode xQ in xQList)
            {
                Question q = new Question()
                {
                    id = int.Parse(xQ.Attributes["ID"].Value),
                    category = int.Parse(xQ.Attributes["categoryID"].Value),
                    question = xQ["description"].InnerText,
                    feedbackCorrect = xQ["feedbackCorrect"].InnerText,
                    feedbackWrong = xQ["feedbackWrong"].InnerText
                };

                XmlNodeList xAList = xQ.SelectNodes("Answer");

                foreach (XmlNode xA in xAList)
                {
                    Answer a = new Answer()
                    {
                        id = xA.Attributes["ID"].Value,
                        //correct = Convert.ToBoolean(xA.Attributes["correct"].Value),
                        text = xA["Answer"].InnerText
                    };
                    q.answerList.Add(a);
                }
                tempQList.Add(q);
            }
            System.Diagnostics.Debug.WriteLine(tempQList[5].answerList[1].text);
            
            #endregion
        }

        //Method to get user's latest test
        public void getLastTest()
        {
            string user = username;
            DateTime date = lastTestDate;
            latestTest = newSQL.getLastTest(user, date);

            //latestTest.createLastTest();
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
        //public void createTest()
        //{           
        //    DateTime testDate = lastTestDate;
        //    string user = username;

        //    if (testDate.Year != today.Year || testDate == null)
        //    {
        //        bool qual = qualified;
        //        int testType = 1;
        //        if (qual == true)
        //        {
        //            testType = 2;
        //        }
        //        else
        //        {
        //            testType = 1;
        //        }

        //        newTest.getQuestions(testType);
        //    }
        //    else if (testDate.Year == today.Year)
        //    {
        //        getLastTest(user, testDate);
        //    }
        //}

        //public void getLastTest(string user, DateTime date)
        //{
        //    string username = user;
        //    DateTime latestDate = date;
        //}
    }
}