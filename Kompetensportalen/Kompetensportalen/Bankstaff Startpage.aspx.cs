using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
        }
    }
}