﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Bankstaff Startpage.aspx.cs" Inherits="Kompetensportalen.Bankstaff_Startpage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta charset="utf-8"/>
        <link rel="stylesheet" href="kpstyle.css"/>
        <link rel="stylesheet" href="modal.css" />
        <title>Bankpersonal Hemsida</title>
    </head>

    <body>
        <form id="form1" runat="server">	
                <nav>
                    <img src="images/euro-1105411_1920.jpg" alt="Sample Photo" />

		            <ul>                  
		                <li><i><strong><a href="#" title="Startsida"><span>Startsida</span></a></strong></i></li>
		                <li><a href="Bankstaff Testhistory.aspx" title="Provhistorik"><span>Provhistorik</span></a></li>
		            </ul>
                </nav>
                <div class="main">
                    <div id="content">                  
                        <h3>Hem</h3>

                        <asp:Button ID="btnStartTest" runat="server" Text="Starta testet" OnClick="btnStartTest_Click" PostBackUrl="#" />
                    </div><!--content-->

                    <!--Modal for test-->
                    <div id="testModal" class="modal">
                        
                        <!--Modal content-->
                        <div class="modal-content">
                            <span class="close">&times</span>
                            <p>Här skriver vi testfrågorna</p>
                        </div>
                    </div>
                </div><!--main--> 
            
                <footer>
                    <p> Hemsida Skapad av Grupp 12 <br/>
                        Kontaktinformation: <a href="mailto:grupp12@student.miun.se">grupp12@student.miun.se</a>.
                    </p>
                </footer>
        </form>
    </body>
</html>