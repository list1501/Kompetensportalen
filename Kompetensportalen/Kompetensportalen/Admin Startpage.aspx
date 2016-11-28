<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin Startpage.aspx.cs" Inherits="Kompetensportalen.Admin_Startpage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta charset="utf-8"/>
        <link rel="stylesheet" href="kpstyle.css"/>
        <title>Administratör Hemsida</title>
    </head>

    <body>
        <form id="form1" runat="server">
            <div id="menu-top">    		
                <nav>
		            <ul>                  
		                <li><i><strong><a href="#" title="Startpage"><span>Hemsida</span></a></strong></i></li>
                    </ul>
                </nav>



                <div id="content">
                    <div class="main">
                        <div class="Section">
                            <h3>Hemsida</h3>
                            <asp:Button ID="btnSeeAllUsers" runat="server" Text="Alla användare" OnClick="btnSeeAllUsers_Click"/>
                            <asp:Button ID="btnSeeUncertified" runat="server" Text="Ej licensierade användare" OnClick="btnSeeUncertified_Click"/>
                            <asp:Button ID="btnSeeCertified" runat="server" Text="Licensierade användare" OnClick="btnSeeCertified_Click"/>
                            <asp:Button ID="btnSeeUsersforAnnualCheck" runat="server" Text="Användare som måste ta årligt kunskapstest" OnClick="btnSeeUsersforAnnualCheck_Click"/>
                        </div>

                        <div style="overflow-x:auto;">
                <table id="table" runat="server" border="1" visible="false"></table> 
                            </div>


                        <div class="Section">
                        </div>       
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