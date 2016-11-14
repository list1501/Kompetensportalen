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

        //public void countPoints()
        //{
        //    int counter = 0;
        //    while (correct == true)
        //    {
        //        counter++;
        //    }
        //}
    }
    }