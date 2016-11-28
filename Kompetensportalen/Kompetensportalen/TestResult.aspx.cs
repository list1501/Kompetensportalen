using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Kompetensportalen
{
    public partial class TestResult : System.Web.UI.Page
    {
        User currentUser = Loginpage.currentLogin;
        DateTime today = DateTime.Today;
        SQL newSQL = new SQL();

        protected void Page_Load(object sender, EventArgs e)
        {
            currentUser.getLastTest();
        }
    }
}