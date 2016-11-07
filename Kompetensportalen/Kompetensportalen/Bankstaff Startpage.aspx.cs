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
            //Här hämtar vi metoden för att fylla XML med testfrågor. Därefter kör vi JS som startar provet.... ?...
           
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
            }
            else if (type == 1)
            {
               path = Server.MapPath("Qualification Test.xml");
                doc.Load(path);
            }
         
            foreach (Question q in newQuestionsList)
            {             
                XmlNode root = doc.DocumentElement;
                XmlElement newQ = doc.CreateElement("question");

                XmlElement qID = doc.CreateElement("ID");
                qID.InnerText = q.id.ToString();
                XmlElement qCat = doc.CreateElement("category");
                qCat.InnerText = q.category.ToString();
                XmlElement qDescr = doc.CreateElement("description");
                qDescr.InnerText = q.question;
                XmlElement qAns = doc.CreateElement("answer");
                qAns.InnerText = q.correctAnswer.ToString();

                newQ.AppendChild(qID);
                newQ.AppendChild(qCat);
                newQ.AppendChild(qDescr);
                newQ.AppendChild(qAns);
                root.AppendChild(newQ);

                doc.Save(path);

                string xml = GetXMLFromObject(newQ);
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



