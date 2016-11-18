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
        
        //Objects        
        public Test newTest = new Test();
        public Test latestTest = new Test();
        SQL newSQL = new SQL();
        DateTime today = DateTime.Today;

        #region Get test for user

        //Method to get new test for user
        public List<Question> getNewTest(int type)
        {
            int testType = type;

            newTest.employee = username;
            newTest.date = today;
            newTest.testType = testType;
            newTest.sourceFile = newSQL.DbToXml(testType); //Create source file from XML stored in database
            newTest.questions = new List<Question>();

            #region Get test from XML
            
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
                    feedbackWrong = xQ["feedbackWrong"].InnerText,
                    answerList = new List<Answer>(),
                    userAnswerList = new List<Answer>()                    
                };

                foreach (XmlNode node in xQ.ChildNodes)
                {
                    if (node.Name == "Answer")
                    {
                        Answer a = new Answer()
                        {
                            id = node.Attributes["ansId"].Value,
                            correct = Convert.ToBoolean(node.Attributes["correct"].Value),
                            text = node.InnerText
                        };
                        q.answerList.Add(a);
                    }
                }
                tempQList.Add(q);              
            }
            newTest.questions = tempQList;
            #endregion

            #region Randomise questions into test (Not working yet)

            int c = tempQList.Count;
            List<int> randomList = getRandomList(c);

            return tempQList;

            //for (int i = 0; i < randomList.Count; i++)
            //{
            //    int r = randomList[i];
            //    Question q = tempQList[r];
            //    newTest.questions.Add(q);
            //}
            //foreach (int i in randomList)
            //{
            //    System.Diagnostics.Debug.WriteLine(randomList[i].ToString());
            //}
            #endregion
        }

        //Method to get list of random, unique numbers
        public List<int> getRandomList(int length)
        {
            int l = length;
            int n = length;

            List<int> possibleNumbers = new List<int>();
            for (int i = 1; i < l+1; i++)
            {
                possibleNumbers.Add(i);
            }

            List<int> randomList = new List<int>();
            Random rand = new Random();

            for (int i = 0; i < n; i++)
            {
                int r = rand.Next(1, possibleNumbers.Count) - 1;
                randomList.Add(possibleNumbers[r]);
                possibleNumbers.RemoveAt(r);
            }
            return randomList;
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