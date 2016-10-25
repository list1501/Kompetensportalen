<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Loginsida.aspx.cs" Inherits="Kompetensportalen.Loginsida" %>

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
    <h3>Inloggningssida</h3>

     <asp:Button ID="Button1" runat="server" Text="Bankpersonal"
PostBackUrl="~/Bankpersonal Hemsida.aspx" />
        <asp:Button ID="Button2" runat="server" Text="Administration"
PostBackUrl="~/Admin Hemsida.aspx" />         
   
 </div><!--menu-top--> 
 </div><!--main-->
 </div><!--hem-->

<div id="wrapper-footer">
<footer>
  <p> Hemsida Skapad av Grupp 12 <br/>
 Kontaktinformation: <a href="mailto:grupp12@student.miun.se">
  grupp12@student.miun.se</a>.</p>
</footer>
</div><!--wrapper-footer-->
    </form>
</body>
</html>
 