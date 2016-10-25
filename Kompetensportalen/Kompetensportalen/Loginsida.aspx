<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Loginsida.aspx.cs" Inherits="Kompetensportalen.Loginsida" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     
    
        <asp:Button ID="btnbankpersonal" runat="server" Text="Bankpersonal"
PostBackUrl="~/Bankpersonal Hemsida.aspx" />
        <asp:Button ID="btnAdmin" runat="server" Text="Administration"
PostBackUrl="~/Admin Hemsida.aspx" />
      
    </div>
    </form>
</body>
</html>
