using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Kompetensportalen
{
    public partial class Loginpage : System.Web.UI.Page
    {
        public static User currentLogin = new User();
        SQL newSQL = new SQL();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //Login for user: Admin
        protected void btnLoginAdmin_Click(object sender, EventArgs e)
        {
            currentLogin = newSQL.getLogin("admin");
            System.Diagnostics.Debug.WriteLine(currentLogin.username);
            Server.Transfer("Admin Startpage.aspx");
        }

        //Login for user: Emma
        protected void btnLoginEmma_Click(object sender, EventArgs e)
        {
            currentLogin = newSQL.getLogin("emsu101");
            System.Diagnostics.Debug.WriteLine(currentLogin.username);
            Server.Transfer("Bankstaff Startpage.aspx");
        }

        //Login for user: Linda
        protected void btnLoginLinda_Click(object sender, EventArgs e)
        {
            currentLogin = newSQL.getLogin("list069");
            System.Diagnostics.Debug.WriteLine(currentLogin.username);
            Server.Transfer("Bankstaff Startpage.aspx");

        }

        //Login for user: Martin
        protected void btnLoginMartin_Click(object sender, EventArgs e)
        {
            currentLogin = newSQL.getLogin("maca007");
            System.Diagnostics.Debug.WriteLine(currentLogin.username);
            Server.Transfer("Bankstaff Startpage.aspx");
        }
    }
}