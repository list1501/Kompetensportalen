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

             Question nyQuestion = new Question();

            string path = Server.MapPath("Competency Test.xml");
            string path1 = Server.MapPath("Qualification Test.xml");
            XmlDocument doc = new XmlDocument();
            doc.Load(path+path1);

            XmlNode root = doc.DocumentElement;

            XmlElement nyQ = doc.CreateElement("question");

            XmlElement qID = doc.CreateElement("ID");
            qID.InnerText = nyQuestion.id.ToString();
            XmlElement qCat = doc.CreateElement("category");
            qCat.InnerText = nyQuestion.category.ToString();
            XmlElement qDescr = doc.CreateElement("description");
            qDescr.InnerText = nyQuestion.question;
            XmlElement qAns = doc.CreateElement("answer");
            qAns.InnerText = nyQuestion.correctAnswer.ToString();

            nyQ.AppendChild(qID);
            nyQ.AppendChild(qCat);
            nyQ.AppendChild(qDescr);
            nyQ.AppendChild(qAns);
            root.AppendChild(nyQ);

            doc.Save(path+path1);

            string xml = GetXMLFromObject(nyQuestion);
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



