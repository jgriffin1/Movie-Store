<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="MovieStore.web.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
          <asp:Label ID="lblDateTime" runat="server" />
          <asp:Literal ID="ltrDateTime" runat="server" Text="Hey You" />
          <asp:CheckBox ID="CheckBox1" runat="server" />

          <input id="File1" type="file" />
          <asp:TextBox ID="tboxFirstName" runat="server" />
          <asp:DropDownList ID="ddlStates" runat="server">
            <asp:ListItem Text="ND" Value="ND"></asp:ListItem>
            <asp:ListItem Text="FL" Value="FL"></asp:ListItem>
          </asp:DropDownList>
          
        </div>
    </form>
</body>
</html>
