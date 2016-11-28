using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kompetensportalen
{
    public class Question
    {
        public int id { get; set; }
        public int category { get; set; }
        public string question { get; set; }
        public string feedbackCorrect { get; set; }
        public string feedbackWrong { get; set; }
        public bool correct { get; set; }
        public List<Answer> answerList { get; set; }
        public List<Answer> userAnswerList { get; set; }
        Test nyttest = new Test();

        public void countPoints()
        {
            int points = 0;

            for (int i = 0; i < answerList.Count; i++)
            {
                if (answerList[i] == userAnswerList[i])
                {
                    points++;
                }

                else
                {

                }

                nyttest.passorfail();

            }


            

            //int count = 0; 

            //for (int i = 0; i < userAnswerList.Count; i++)
            //{
            //    string svar = userAnswerList[i].ToString();

            //    foreach (Answer item in answerList)
            //    {
            //        if (svar == item.ToString())
            //        {
            //            count++;
            //        }
            //    }                           
            //    nyttest.passorfail();
            //}

            //    if (userAnswerList == answerList)
            //{
            //    int count = 0;

            //    for (int i = 0; i < question.Length; i++)
            //        if (question[correct[i].ToString] == i)
            //        {
            //            count++;
            //        }
            //}

        }







    }
}