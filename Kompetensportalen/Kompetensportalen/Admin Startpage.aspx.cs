using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Kompetensportalen
{
    public partial class Admin_Startpage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            User currentUser = Loginpage.currentLogin;
        }

        protected void btnSeeTests_Click(object sender, EventArgs e)
        {

        }
    }
}