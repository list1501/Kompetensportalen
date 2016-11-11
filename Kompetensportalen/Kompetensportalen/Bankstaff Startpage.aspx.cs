using System;
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
            SQL newSQL = new SQL();
            
            XmlDocument test = newSQL.DbToXml((1));
            List<Question> newQuestionsList = new List<Question>();

            foreach (XmlNode xTest in test["Test"])
            {               
                    Question q = new Question();
                    q.category = Int32.Parse(xTest.Attributes["ID"].Value);
                    q.catdescription = xTest["description"].Value;

                    foreach (XmlNode xNode in xTest["question"])
             {                               
                q.id = Int32.Parse(xNode.Attributes["ID"].Value);
                q.question = xNode["description"].Value;

                             foreach (XmlNode xA in xNode["Answer"])
                                {
                                    Answer a = new Answer();
                                    a.text = xA.InnerText;
                                    a.correctOrNot = Int32.Parse(xA.Attributes["correct"].Value);
                                }
                               //newQuestionsList.Add();
            }
            //return newQuestionsList;
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

        //public static string GetXMLFromObject(object o)
        //{
        //    StringWriter sw = new StringWriter();
        //    XmlTextWriter tw = null;
        //    try
        //    {
        //        XmlSerializer serializer = new XmlSerializer(o.GetType());
        //        tw = new XmlTextWriter(sw);
        //        serializer.Serialize(tw, o);
        //    }
        //    catch (Exception ex)
        //    {
        //        //Handle Exception Code
        //    }
        //    finally
        //    {
        //        sw.Close();
        //        if (tw != null)
        //        {
        //            tw.Close();
        //        }
        //    }
        //    return sw.ToString();
        //}
    }
}
}




