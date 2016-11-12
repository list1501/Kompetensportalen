﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;

namespace Kompetensportalen
{
    public partial class Bankstaff_Startpage : System.Web.UI.Page
    {
        User currentUser = Loginpage.currentLogin;

        protected void Page_Load(object sender, EventArgs e)
        {

            System.Diagnostics.Debug.WriteLine(currentUser.qualified.ToString());
        }

        protected void btnStartTest_Click(object sender, EventArgs e)
        {            
            SQL newSQL = new SQL();
            //om currentUser är qualified ska testID 2 ut, som är Competency testet
            if (currentUser.qualified == true)
            {
                int testType = 2;
                XmlDocument test = newSQL.DbToXml(testType);
                List<Question> newQuestionsList = new List<Question>();
                List<Answer> AnswerList = new List<Answer>();

                foreach (XmlNode xTest in test["Test"])
                {
                    Question q = new Question();
                    q.category = Int32.Parse(xTest.Attributes["ID"].Value);
                    q.catdescription = xTest["description"].Value;
                    q.id = Int32.Parse(xTest.Attributes["ID"].Value);
                    q.question = xTest["description"].Value;

                    labelQuestion.Text = xTest["description"].InnerText;

                    foreach (XmlNode xA in xTest["Answer"])
                    {
                        Answer a = new Answer();
                        a.text = xA.InnerText;
                        //a.correctOrNot = Int32.Parse(xA.Attributes["correct"].Value);

                        chBAnswers.Items.Add(xA.InnerText);
                    }
                    newQuestionsList.Add(q);
                    chBAnswers.DataBind();
                }
            }
            //om currentUser INTE är qualified ska testID 1 ut, som är qualification testet
            else if (currentUser.qualified == false)
            {
                XmlDocument test = newSQL.DbToXml((1));
                List<Question> newQuestionsList = new List<Question>();
                List<Answer> AnswerList = new List<Answer>();

                foreach (XmlNode xTest in test["Test"])
                {
                    Question q = new Question();
                    q.category = Int32.Parse(xTest.Attributes["ID"].Value);
                    q.catdescription = xTest["description"].Value;
                    q.id = Int32.Parse(xTest.Attributes["ID"].Value);
                    q.question = xTest["description"].Value;

                    labelQuestion.Text = xTest["description"].InnerText;

                    foreach (XmlNode xA in xTest["Answer"])
                    {
                        Answer a = new Answer();
                        
                        a.text = xA.InnerText;
                        //a.correctOrNot = Int32.Parse(xA.Attributes["correct"].Value);

                        chBAnswers.Items.Add(xA.InnerText);

                    }
                    newQuestionsList.Add(q);
                    chBAnswers.DataBind();
                }
            }

        }               
        //currentUser.createTest();
        //Test newTest = currentUser.newTest;
        //List<Question> newQuestionsList = newTest.questions;            
        //int type = newTest.testType;
        //XmlDocument doc = newSQL.DbToXml((1));
        //string path;

        //if (type == 2)
        //{
        //    path = Server.MapPath("Competency Test.xml");
        //}
        //else
        //{
        //    path = Server.MapPath("Qualification Test.xml");
        //}
        //doc.Load(path);

        //int i = 1;
        //foreach (Question q in newQuestionsList)
        //{                    
        //    List<Answer> newCorrectList = q.correctAnswer;
        //    List<Answer> newWrongList = q.wrongAnswer;

        //    XmlNode root = doc.DocumentElement;
        //    XmlElement newQ = doc.CreateElement("question");

        //    XmlElement qID = doc.CreateElement("ID");
        //    qID.InnerText = i.ToString();
        //    XmlElement qDbID = doc.CreateElement("dbID");
        //    qDbID.InnerText = q.id.ToString();
        //    XmlElement qCat = doc.CreateElement("category");
        //    qCat.InnerText = q.category.ToString();
        //    XmlElement qDescr = doc.CreateElement("description");
        //    qDescr.InnerText = q.question;

        //    newQ.AppendChild(qID);
        //    newQ.AppendChild(qCat);
        //    newQ.AppendChild(qDescr);

        //    foreach (Answer a in newCorrectList)
        //    {
        //        XmlElement qAns = doc.CreateElement("correctAnswer");
        //        qAns.InnerText = a.ToString();
        //        newQ.AppendChild(qAns);
        //    }
        //    foreach (Answer a in newWrongList)
        //    {
        //        XmlElement qAns = doc.CreateElement("wrongAnswer");
        //        qAns.InnerText = a.ToString();
        //        newQ.AppendChild(qAns);
        //    }

        //    root.AppendChild(newQ);

        //    doc.Save(path);

        //    GetXMLFromObject(newQ);
        //    i++;
        //}
        //}
      
    }
}




