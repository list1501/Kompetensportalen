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

        SQL newSQL = new SQL();
        

        //Method to get questions in random order from database
        public void getQuestions(int type)
        {
            int testType = type;
            int n = 25;
            int c = 0;

            if (testType == 2)
            {
                n = 15;
            }
            else
            {
                n = 25;
            }

            while (c < n)
            {
                Random rand = new Random();
                int r = rand.Next(1, n);
                bool unique = true;
                foreach (Question q in questions)
                {
                    if (r == q.id)
                    {
                        unique = false;
                    }
                    else
                    {
                        unique = true;
                    }
                }
                if (unique == true)
                {
                    Question newQuestion = newSQL.getQuestion(r, testType);
                    questions.Add(newQuestion);
                }
                c++;
            }
    }
       

        //public void passOrFail()
        //{
        //    if ()
        //}
    }
}