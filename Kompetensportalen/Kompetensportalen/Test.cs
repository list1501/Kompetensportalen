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
        public void countPoints()
        {
           
        }

        
        //method pass och fail
        public void passorfail()
        {
            //om man först själv räknar ut hur många frågor som är antal rätt % osv... 
            //kan inte tänka längre
            //if (category1 == 13 && category2 == 13 && category3 == 10 && totalPoints < 50)
            //{
            //    label1.text = "grattis du klarade testet";
            //    answerlist.userAnswerList.Add(answer);
            //    XmlElement svar = new XmlElement("Svar",
            //    new XmlAttribute("answer"));
            //    Doc.Root.Add(svar);
            //    doc.save(answer.xml);
            //}

            //else
            //{
            //    label1.text = "Du klarade inte detta test.";
            //}
        }
        #endregion
    }
}
