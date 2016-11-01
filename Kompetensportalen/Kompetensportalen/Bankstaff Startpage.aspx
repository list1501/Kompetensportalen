<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Bankstaff Startpage.aspx.cs" Inherits="Kompetensportalen.Bankstaff_Startpage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta charset="utf-8"/>
        <link rel="stylesheet" href="kpstyle.css"/>
        <title>Bankpersonal Hemsida</title>
    </head>

    <body>
        <form id="form1" runat="server">
            <div id="menu-top">    		
                <nav>
                    <img src="images/euro-1105411_1920.jpg" alt="Sample Photo" />

		            <ul>                  
		                <li><i><strong><a href="#" title="Startsida"><span>Startsida</span></a></strong></i></li>
		                <li><a href="Bankstaff Testhistory.aspx" title="Provhistorik"><span>Provhistorik</span></a></li>
		            </ul>
                </nav>

                <div id="content">
                    <div class="main">
                        <h3>Hem</h3>

                        <asp:Button ID="Button1" runat="server" Text="Ny användare - Licensieringstest" PostBackUrl="~/Qualification Test.aspx" />
                        <asp:Button ID="Button2" runat="server" Text="Veteran - Kunskapstest" PostBackUrl="~/Competency Test.aspx" />
                    </div><!--main--> 
                </div><!--content-->
            </div><!--menu-top-->

            <div id="wrapper-footer">
                <footer>
                    <p> Hemsida Skapad av Grupp 12 <br/>
                        Kontaktinformation: <a href="mailto:grupp12@student.miun.se">grupp12@student.miun.se</a>.
                    </p>
                </footer>
            </div><!--wrapper-footer-->
        </form>
    </body>
</html>