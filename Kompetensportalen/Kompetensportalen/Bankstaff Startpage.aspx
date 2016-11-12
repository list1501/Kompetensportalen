<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Bankstaff Startpage.aspx.cs" Inherits="Kompetensportalen.Bankstaff_Startpage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %> <!-- ASSEMBLY REFERENCE FOR AjaxControlToolkit -->

<!DOCTYPE html>

<html lang="sv-se" xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta charset="utf-8"/>
        <link rel="stylesheet" href="kpstyle.css"/>
        <link rel="stylesheet" href="modal.css" />        
        <title>Bankpersonal Hemsida</title>
    </head>

    <body>
        <form id="form1" runat="server">	
            <asp:ScriptManager ID="scriptMan" runat="server"></asp:ScriptManager> <!-- SCRIPT MANAGER FOR USING Ajax SCRIPTS -->

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
                    
                    <asp:Button ID="btnStartTest" runat="server" Text="Starta testet" OnClick="btnStartTest_Click"/>

                    <asp:Label ID="labelQuestion" runat="server" Text=""></asp:Label>
                    <asp:RadioButtonList ID="RbAnswers" runat="server"></asp:RadioButtonList>
                    <asp:CheckBoxList ID="chBAnswers" runat="server"></asp:CheckBoxList>
                </div><!--content-->

               <%-- <!-- MODAL POPUP STARTS HERE -->
                <ajax:ModalPopupExtender ID="mpeTest" runat="server" PopupControlID="panelTest" TargetControlID="btnStartTest" CancelControlID="btnCancel" BackgroundCssClass="modalBackground">
                </ajax:ModalPopupExtender>

                <asp:Panel ID="panelTest" runat="server" CssClass="modalPopup" align="center" style="display:none">
                   <br />
                    <asp:Button ID="btnCancel" runat="server" Text="Avbryt test" />
                </asp:Panel>
                <!-- END OF MODAL POPUP -->--%>
                                       
            </div><!--main--> 
            
            <footer>
                <p> Hemsida Skapad av Grupp 12 <br/>
                    Kontaktinformation: <a href="mailto:grupp12@student.miun.se">grupp12@student.miun.se</a>.
                </p>
            </footer>
        </form>
    </body>
</html>