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
        DateTime today = DateTime.Today;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Check if logged in user is qualified and load correct test to object
            if (currentUser.qualified && currentUser.lastTestDate.Year == today.Year)
            {
                btnStartTest.Text = "Titta på senaste testet";
                currentUser.getLastTest();
            }
            else if (currentUser.qualified && currentUser.lastTestDate.Year != today.Year)
            {
                btnStartTest.Text = "Starta ÅKU-test";
                currentUser.getNewTest(2);
            }
            else if (currentUser.qualified == false) //Här kan vi lägga till en metodjämförelse om vi vill för att kolla om den som inte är godkänd får göra provet än. /Martin
            {
                btnStartTest.Text = "Starta licensieringstest";
                currentUser.getNewTest(1);
            }
        }

        protected void btnStartTest_Click(object sender, EventArgs e)
        {
            currentUser.getNewTest(1);
        }
    }
}




