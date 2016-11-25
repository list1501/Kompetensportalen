﻿using System;
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
        //public void passorfail()
        //{
        //    int cat1 = 60 / 100;
        //    int cat2 = 60 / 100;
        //    int cat3 = 60 / 100;
        //    int total = 70 / 100;

        //    if (category1 <= cat1 && category2 <= cat2 && category3 <= cat3 && totalPoints <= total)
        //    {
        //        string passtext = "grattis du klarade testet";
        //        answerlist.userAnswerList.Add(answer);
        //        XmlElement svar = new XmlElement("Svar",
        //        new XmlAttribute("answer"));
        //        Doc.Root.Add(svar);
        //        doc.save(answer.xml);
        //    }

        //    else
        //    {
        //        string failtext = "Du klarade inte detta test.";
        //    }
        //}
        #endregion
    }
}
