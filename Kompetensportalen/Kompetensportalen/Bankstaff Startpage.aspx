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
		        <ul>                  
		            <li><i><strong><a href="#" title="Startsida"><span>Startsida</span></a></strong></i></li>
		        </ul>
            </nav>
            <div class="main">                  
                <h3>Hem</h3>
                   
                <asp:Image runat="server"></asp:Image>

                <asp:Button ID="btnStartTest" runat="server"/>                   
                <!--<asp:Panel ID="pnlquestionWAnswer" runat="server" />
                

                <!-- MODAL POPUP STARTS HERE -->
                <ajax:ModalPopupExtender ID="mpeTest" runat="server" PopupControlID="panelTest" TargetControlID="btnStartTest" CancelControlID="btnCancel" BackgroundCssClass="modalBackground" RepositionMode="RepositionOnWindowResizeAndScroll">
                </ajax:ModalPopupExtender>

                <asp:Label ID="label1" runat="server" Text=""></asp:Label>
                <asp:Panel ID="panelTest" runat="server" CssClass="modalPopup" align="center" style="display:none" ScrollBars="Vertical">
                    <asp:Panel ID="testContent" runat="server" CssClass="modalContent">

                    </asp:Panel>
                    <asp:Button ID="btnCancel" runat="server" Text="Avbryt test" />
                    <asp:Button ID="btnStopTest" runat="server" Visible="false" Text="Stoppa testet" OnClick="btnStopTest_Click"/>

                </asp:Panel>
                <!-- END OF MODAL POPUP -->
                                       
            </div><!--main-->             
        </form>
            <footer>
                <p> Hemsida Skapad av Grupp 12 <br/>
                    Kontaktinformation: <a href="mailto:grupp12@student.miun.se">grupp12@student.miun.se</a>.
                </p>
            </footer>
    </body>
</html>