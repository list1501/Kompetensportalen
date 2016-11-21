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

namespace Kompetensportalen
{
    public partial class Admin_Startpage : System.Web.UI.Page
    {
        SQL openconn = new SQL();
        List<Test> testlist;

        protected void Page_Load(object sender, EventArgs e)
        {
            User currentUser = Loginpage.currentLogin;
        }

        protected void btnSeeTests_Click(object sender, EventArgs e)
        {
            //testslist = openconn.getTestsAdmin();
            //ListBox1.Items.Add(testslist.ToString());
            //openconn.closeConn();

            table.Visible = true;
            table.Rows.Clear();
            openconn.getTestsAdmin();

            HtmlTableRow rowHeader = new HtmlTableRow();

            HtmlTableCell username = new HtmlTableCell("th");
            HtmlTableCell date = new HtmlTableCell("th");
            HtmlTableCell passed = new HtmlTableCell("th");
            HtmlTableCell totalpoints = new HtmlTableCell("th");
            HtmlTableCell category1 = new HtmlTableCell("th");
            HtmlTableCell category2 = new HtmlTableCell("th");
            HtmlTableCell category3 = new HtmlTableCell("th");

            username.InnerText = "Användarnamn";
            date.InnerText = "Datum";
            passed.InnerText = "Godkänd";
            totalpoints.InnerText = "Totalpoäng";
            category1.InnerText = "Kategori 1";
            category2.InnerText = "Kategori 2";
            category3.InnerText = "Kategori 3";
            
            rowHeader.Cells.Add(username);
            rowHeader.Cells.Add(date);
            rowHeader.Cells.Add(passed);
            rowHeader.Cells.Add(totalpoints);
            rowHeader.Cells.Add(category1);
            rowHeader.Cells.Add(category2);
            rowHeader.Cells.Add(category3);

            table.Rows.Add(rowHeader);

            testlist = new List <Test>();

            foreach (Test tests in testlist)
            {
                HtmlTableRow testRows = new HtmlTableRow();
                table.Rows.Add(testRows);

                HtmlTableCell usname = new HtmlTableCell();
                HtmlTableCell usdate = new HtmlTableCell();
                HtmlTableCell uspassed = new HtmlTableCell();
                HtmlTableCell ustotalp = new HtmlTableCell();
                HtmlTableCell uscat1 = new HtmlTableCell();
                HtmlTableCell uscat2 = new HtmlTableCell();
                HtmlTableCell uscat3 = new HtmlTableCell();

                testRows.Cells.Add(usname);
                testRows.Cells.Add(usdate);
                testRows.Cells.Add(uspassed);
                testRows.Cells.Add(ustotalp);
                testRows.Cells.Add(uscat1);
                testRows.Cells.Add(uscat2);
                testRows.Cells.Add(uscat3);

                usname.InnerText = tests.employee;
                usdate.InnerText = tests.date.ToString();
                uspassed.InnerText = tests.passed.ToString();
                ustotalp.InnerText = tests.totalPoints.ToString();
                uscat1.InnerText = tests.category1.ToString();
                uscat2.InnerText = tests.category2.ToString();
                uscat3.InnerText = tests.category3.ToString();            
        }
        }
    }
}


