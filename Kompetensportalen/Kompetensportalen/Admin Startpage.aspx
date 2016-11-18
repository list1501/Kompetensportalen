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
                            <asp:Button ID="btnSeeTests" runat="server" Text="Se lista över användare och deras prov" OnClick="btnSeeTests_Click"/>
                        </div>

                <table id="table" runat="server" border="1" visible="false"></table> 

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