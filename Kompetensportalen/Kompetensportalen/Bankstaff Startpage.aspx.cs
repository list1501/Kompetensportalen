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
            
        }

        protected void btnStartTest_Click(object sender, EventArgs e)
        {          
            currentUser.createNewTest();
            Test newTest = currentUser.newTest;
            List<Question> newQuestionsList = newTest.questions;            
            int type = newTest.testType;
            XmlDocument doc = new XmlDocument();
            string path;

            if (type == 2)
            {
                path = Server.MapPath("Competency Test.xml");
                doc.Load(path);
                int i = 1;
                foreach (Question q in newQuestionsList)
                {                    
                    List<Answer> newCorrectList = q.correctAnswer;
                    List<Answer> newWrongList = q.wrongAnswer;

                    XmlNode root = doc.DocumentElement;
                    XmlElement newQ = doc.CreateElement("question");

                    XmlElement qID = doc.CreateElement("ID");
                    qID.InnerText = i.ToString();
                    XmlElement qDbID = doc.CreateElement("dbID");
                    qDbID.InnerText = q.id.ToString();
                    XmlElement qCat = doc.CreateElement("category");
                    qCat.InnerText = q.category.ToString();
                    XmlElement qDescr = doc.CreateElement("description");
                    qDescr.InnerText = q.question;

                    newQ.AppendChild(qID);
                    newQ.AppendChild(qCat);
                    newQ.AppendChild(qDescr);

                    foreach (Answer a in newCorrectList)
                    {
                        XmlElement qAns = doc.CreateElement("answer");
                        qAns.InnerText = a.ToString();
                        newQ.AppendChild(qAns);
                    }
                    foreach (Answer a in newWrongList)
                    {
                        XmlElement qAns = doc.CreateElement("answer");
                        qAns.InnerText = a.ToString();
                        newQ.AppendChild(qAns);
                    }

                    root.AppendChild(newQ);

                    doc.Save(path);

                    GetXMLFromObject(newQ);
                    i++;
                }
                
            }
            else if (type == 1)
            {
                path = Server.MapPath("Qualification Test.xml");
                doc.Load(path);
                int i = 1;
                foreach (Question q in newQuestionsList)
                {
                    List<Answer> newCorrectList = q.correctAnswer;
                    List<Answer> newWrongList = q.wrongAnswer;

                    XmlNode root = doc.DocumentElement;
                    XmlElement newQ = doc.CreateElement("question");

                    XmlElement qID = doc.CreateElement("ID");
                    qID.InnerText = i.ToString();
                    XmlElement qDbID = doc.CreateElement("dbID");
                    qDbID.InnerText = q.id.ToString();
                    XmlElement qCat = doc.CreateElement("category");
                    qCat.InnerText = q.category.ToString();
                    XmlElement qDescr = doc.CreateElement("description");
                    qDescr.InnerText = q.question;
                    
                    newQ.AppendChild(qID);
                    newQ.AppendChild(qCat);
                    newQ.AppendChild(qDescr);

                    foreach (Answer a in newCorrectList)
                    {
                        XmlElement qAns = doc.CreateElement("answer");
                        qAns.InnerText = a.ToString();
                        newQ.AppendChild(qAns);
                    }
                    foreach (Answer a in newWrongList)
                    {
                        XmlElement qAns = doc.CreateElement("answer");
                        qAns.InnerText = a.ToString();
                        newQ.AppendChild(qAns);
                    }
                    root.AppendChild(newQ);

                    doc.Save(path);

                    GetXMLFromObject(newQ);
                    i++;
                }                  
            }
        }

        public static string GetXMLFromObject(object o)
        {
            StringWriter sw = new StringWriter();
            XmlTextWriter tw = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(o.GetType());
                tw = new XmlTextWriter(sw);
                serializer.Serialize(tw, o);
            }
            catch (Exception ex)
            {
                //Handle Exception Code
            }
            finally
            {
                sw.Close();
                if (tw != null)
                {
                    tw.Close();
                }
            }
            return sw.ToString();
        }
    }
}



