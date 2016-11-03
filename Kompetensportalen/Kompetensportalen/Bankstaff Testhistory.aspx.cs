using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Kompetensportalen
{
    public partial class Bankstaff_Testhistory : System.Web.UI.Page
    {
        User currentUser = Loginpage.currentLogin;
        List<Test> testHistory = new List<Test>();

        protected void Page_Load(object sender, EventArgs e)
        {            
            currentUser.createTestHistory();
            testHistory = currentUser.testHistory;
        }
    }
}