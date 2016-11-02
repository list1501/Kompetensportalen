using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        public List<Question> questions { get; set; }

        //Method to get questions in random order from database
        public Question getQuestion(int type)
        {
            Question newQuestion = new Question();
            return newQuestion;
        }
    }
}