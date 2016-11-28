using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Globalization;

namespace Kompetensportalen
{
    public partial class Admin_Startpage : System.Web.UI.Page
    {
        SQL openconn = new SQL();
        List<Test> testlist;
        List<User> userlist;

        protected void Page_Load(object sender, EventArgs e)
        {
            User currentUser = Loginpage.currentLogin;
        }      

        protected void btnSeeUncertified_Click(object sender, EventArgs e)
        {
            btnSeeUncertified.Enabled = false;
            btnSeeCertified.Enabled = true;
            btnSeeUsersforAnnualCheck.Enabled = true;
            btnSeeAllUsers.Enabled = true;

            table.Visible = true;
            table.Rows.Clear();

            HtmlTableRow rowHeader = new HtmlTableRow();

            HtmlTableCell username = new HtmlTableCell("th");
            HtmlTableCell date = new HtmlTableCell("th");
            HtmlTableCell type = new HtmlTableCell("th");

            username.InnerText = "Användarnamn";
            date.InnerText = "Senaste Testdatum";

            rowHeader.Cells.Add(username);
            rowHeader.Cells.Add(date);

            table.Rows.Add(rowHeader);

            userlist = new List<User>();
            userlist = openconn.GetNotCertifiedUsersAdmin();

            foreach (User user in userlist)
            {
                HtmlTableRow testRows = new HtmlTableRow();
                table.Rows.Add(testRows);

                HtmlTableCell usname = new HtmlTableCell();
                HtmlTableCell usdate = new HtmlTableCell();

                var dateOnlyString = user.lastTestDate.ToShortDateString(); //Return 00/00/0000

                testRows.Cells.Add(usname);
                testRows.Cells.Add(usdate);

                usname.InnerText = user.username;
                usdate.InnerText = dateOnlyString;
            }
        }

        protected void btnSeeCertified_Click(object sender, EventArgs e)
        {
            btnSeeCertified.Enabled = false;
            btnSeeUncertified.Enabled = true;
            btnSeeUsersforAnnualCheck.Enabled = true;
            btnSeeAllUsers.Enabled = true;

            table.Visible = true;
            table.Rows.Clear();

            HtmlTableRow rowHeader = new HtmlTableRow();

            HtmlTableCell username = new HtmlTableCell("th");
            HtmlTableCell date = new HtmlTableCell("th");
            HtmlTableCell type = new HtmlTableCell("th");

            username.InnerText = "Användarnamn";
            date.InnerText = "Senaste Testdatum";

            rowHeader.Cells.Add(username);
            rowHeader.Cells.Add(date);

            table.Rows.Add(rowHeader);

            userlist = new List<User>();
            userlist = openconn.GetCertifiedUsersAdmin();

            foreach (User user in userlist)
            {
                HtmlTableRow testRows = new HtmlTableRow();
                table.Rows.Add(testRows);

                HtmlTableCell usname = new HtmlTableCell();
                HtmlTableCell usdate = new HtmlTableCell();

                var dateOnlyString = user.lastTestDate.ToShortDateString(); //Return 00/00/0000

                testRows.Cells.Add(usname);
                testRows.Cells.Add(usdate);

                usname.InnerText = user.username;
                usdate.InnerText = dateOnlyString;
            }
        }

        protected void btnSeeUsersforAnnualCheck_Click(object sender, EventArgs e)
        {
            btnSeeUsersforAnnualCheck.Enabled = false;
            btnSeeUncertified.Enabled = true;
            btnSeeCertified.Enabled = true;
            btnSeeAllUsers.Enabled = true;

            table.Visible = true;
            table.Rows.Clear();

            HtmlTableRow rowHeader = new HtmlTableRow();

            HtmlTableCell username = new HtmlTableCell("th");
            HtmlTableCell date = new HtmlTableCell("th");
            HtmlTableCell type = new HtmlTableCell("th");

            username.InnerText = "Användarnamn";
            date.InnerText = "Senaste Testdatum";
            type.InnerText = "Test som måste utföras";

            rowHeader.Cells.Add(username);
            rowHeader.Cells.Add(date);
            rowHeader.Cells.Add(type);

            table.Rows.Add(rowHeader);

            userlist = new List<User>();
            userlist = openconn.GetUsersForAnnualCheck();

            foreach (User user in userlist)
            {
                HtmlTableRow testRows = new HtmlTableRow();
                table.Rows.Add(testRows);

                HtmlTableCell usname = new HtmlTableCell();
                HtmlTableCell usdate = new HtmlTableCell();
                HtmlTableCell ustype = new HtmlTableCell();

                string typeofTest = "";
                if (user.qualified == true)
                {
                    typeofTest = "Kunskapstest";
                }
                else
                    typeofTest = "Licensieringstest";               

                var dateOnlyString = user.lastTestDate.ToShortDateString(); //Return 00/00/0000

                testRows.Cells.Add(usname);
                testRows.Cells.Add(usdate);
                testRows.Cells.Add(ustype);

                usname.InnerText = user.username;
                usdate.InnerText = dateOnlyString;
                ustype.InnerText = typeofTest;
            }
        }

        protected void btnSeeAllUsers_Click(object sender, EventArgs e)
        {
            btnSeeAllUsers.Enabled = false;
            btnSeeUncertified.Enabled = true;
            btnSeeCertified.Enabled = true;
            btnSeeUsersforAnnualCheck.Enabled = true;

            table.Visible = true;
            table.Rows.Clear();


            HtmlTableRow rowHeader = new HtmlTableRow();

            HtmlTableCell username = new HtmlTableCell("th");
            HtmlTableCell date = new HtmlTableCell("th");
            HtmlTableCell type = new HtmlTableCell("th");
            HtmlTableCell passed = new HtmlTableCell("th");
            HtmlTableCell totalpoints = new HtmlTableCell("th");
            HtmlTableCell category1 = new HtmlTableCell("th");
            HtmlTableCell category2 = new HtmlTableCell("th");
            HtmlTableCell category3 = new HtmlTableCell("th");

            username.InnerText = "Användarnamn";
            date.InnerText = "Datum";
            type.InnerText = "Test";
            passed.InnerText = "Resultat";
            totalpoints.InnerText = "Totalpoäng";
            category1.InnerText = "Kategori 1";
            category2.InnerText = "Kategori 2";
            category3.InnerText = "Kategori 3";

            rowHeader.Cells.Add(username);
            rowHeader.Cells.Add(date);
            rowHeader.Cells.Add(type);
            rowHeader.Cells.Add(passed);
            rowHeader.Cells.Add(totalpoints);
            rowHeader.Cells.Add(category1);
            rowHeader.Cells.Add(category2);
            rowHeader.Cells.Add(category3);

            table.Rows.Add(rowHeader);

            testlist = new List<Test>();
            testlist = openconn.getallUsersAdmin();

            foreach (Test tests in testlist)
            {
                HtmlTableRow testRows = new HtmlTableRow();
                table.Rows.Add(testRows);

                HtmlTableCell usname = new HtmlTableCell();
                HtmlTableCell usdate = new HtmlTableCell();
                HtmlTableCell ustype = new HtmlTableCell();
                HtmlTableCell uspassed = new HtmlTableCell();
                HtmlTableCell ustotalp = new HtmlTableCell();
                HtmlTableCell uscat1 = new HtmlTableCell();
                HtmlTableCell uscat2 = new HtmlTableCell();
                HtmlTableCell uscat3 = new HtmlTableCell();

                string typeofTest = "";
                if (tests.testType == 1)
                {
                    typeofTest = "Licensieringstest";
                }
                else
                    typeofTest = "Kunskapstest";

                string passeds = "";
                if (tests.passed == true)
                {
                    passeds = "Godkänd";
                }
                else
                    passeds = "Ej Godkänd";

                var dateOnlyString = tests.date.ToShortDateString(); //Return 00/00/0000

                testRows.Cells.Add(usname);
                testRows.Cells.Add(usdate);
                testRows.Cells.Add(ustype);
                testRows.Cells.Add(uspassed);
                testRows.Cells.Add(ustotalp);
                testRows.Cells.Add(uscat1);
                testRows.Cells.Add(uscat2);
                testRows.Cells.Add(uscat3);

                usname.InnerText = tests.employee;
                usdate.InnerText = dateOnlyString;
                ustype.InnerText = typeofTest;
                uspassed.InnerText = passeds;
                ustotalp.InnerText = tests.totalPoints.ToString();
                uscat1.InnerText = tests.category1.ToString();
                uscat2.InnerText = tests.category2.ToString();
                uscat3.InnerText = tests.category3.ToString();
            }
        }
    }
}


