using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace Kompetensportalen
{
    public class Test
    {
        public string employee { get; set; }
        public DateTime date { get; set; }
        public int testType { get; set; }
        public bool passed { get; set; }
        public int category1 { get; set; }
        public int category2 { get; set; }
        public int category3 { get; set; }
        public int totalPoints { get; set; }
        public XmlDocument sourceFile { get; set; }
        public List<Question> questions { get; set; }       
        


        #region Get questions from test

        #region Method to get a question
        public Question getQuestion(int id)
        {
            int qID = id;
            Question newQuestion = new Question();
            foreach (Question q in questions)
            {
                if (q.id == qID)
                {
                    newQuestion = q;
                }
            }
            return newQuestion;
        }
        #endregion

        #endregion

        #region Count points and percent
       
        //method pass och fail
        public bool passOrFail()
        {
            bool allChecked = false;

            foreach (Question q in questions)
            {
                int numCorrectAnswers = 0;
                int numUserAnswers = q.userAnswerList.Count;

                foreach (Answer a in q.answerList)
                {
                    if (a.correct)
                    {
                        numCorrectAnswers++;
                    }
                }

                if (numCorrectAnswers == numUserAnswers)
                {
                    allChecked = true;
                }
                else
                {
                    allChecked = false;
                }
            }

            if (allChecked)
            {
                //Number of questions in each category
                int cat1 = 0;
                int cat2 = 0;
                int cat3 = 0;
                int total = questions.Count;

                //Number of points in each category
                int cat1Points = 0;
                int cat2Points = 0;
                int cat3Points = 0;

                int totalPoints;

                //Percentages
                double cat1Percent;
                double cat2Percent;
                double cat3Percent;

                double totalPercent;

                foreach (Question q in questions)
                {
                    if (q.category == 1)
                    {
                        cat1++;
                    }
                    else if (q.category == 2)
                    {
                        cat2++;
                    }
                    else if (q.category == 3)
                    {
                        cat3++;
                    }

                    q.correctAnswer();

                    if (q.correct)
                    {
                        if (q.category == 1)
                        {
                            cat1Points++;
                        }
                        else if (q.category == 2)
                        {
                            cat2Points++;
                        }
                        else if (q.category == 3)
                        {
                            cat3Points++;
                        }
                    }
                }

                totalPoints = cat1Points + cat2Points + cat3Points;
                totalPercent = (totalPoints / total) * 100;
                cat1Percent = (cat1Points / cat1) * 100;
                cat2Percent = (cat2Points / cat2) * 100;
                cat3Percent = (cat3Points / cat3) * 100;

                if (totalPercent >= 70 && cat1Percent >= 60 && cat2Percent >= 60 && cat3Percent >= 60)
                {
                    passed = true;
                }

                return true;
            }
            else
            {
                return false;
            }



            //int cat1 = (60 * 100)/100;
            //int cat2 = (60 * 100)/100;
            //int cat3 = (60 * 100)/100;
            //int total = (70 * 100)/100;

            //if (category1 <= cat1 && category2 <= cat2 && category3 <= cat3 && totalPoints <= total)
            //{
                
            //    XmlDocument xdoc = new XmlDocument();
            //    xdoc.LoadXml(sourceFile.ToString());
            //    xdoc.Save("answer.xml");

            //    //XmlElement svar = new XmlElement("Svar",
            //    //new XmlAttribute("answer"));
            //    //Doc.Root.Add(svar);
            //    //doc.save(answer.xml);
            //}

            //else
            //{

            //}
        }
        #endregion
    }
}
