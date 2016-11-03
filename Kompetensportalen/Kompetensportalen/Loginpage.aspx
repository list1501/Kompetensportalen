<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Loginpage.aspx.cs" Inherits="Kompetensportalen.Loginpage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta charset="utf-8"/>
        <link rel="stylesheet" href="kpstyle.css"/>
        <title>Inloggningssida</title>
    </head>

    <body>
        <form id="form1" runat="server">
            <div id="menu-top">    		
                <div id="content">
                    <div class="main">
                        <div class="sections clearfix">
                            <div class="section">
                                <h3>Inloggningssida</h3>
                            </div>
                            
                            <div class="section">
                                <asp:Button ID="btnLoginEmma" runat="server" Text="Logga in Emma" OnClick="btnLoginEmma_Click" PostBackUrl="~/Bankstaff Startpage.aspx" />
                            </div>

                            <div class="section">
                                <asp:Button ID="btnLoginLinda" runat="server" Text="Logga in Linda" OnClick="btnLoginLinda_Click" PostBackUrl="~/Admin Startpage.aspx" />    
                            </div>

                            <div class="section">
                                <asp:Button ID="btnLoginMartin" runat="server" Text="Logga in Martin" OnClick="btnLoginMartin_Click" />
                            </div>
                        </div><!--sections clearfix-->  
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