<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="MainMenu.aspx.cs" Inherits="MovieStore.web.MainMenu" %>
<asp:Content ID="cMainMenu" ContentPlaceHolderID="cphMain" runat="server">
  <div class="container">
    <div class="row">
      <div class="col-md-12">
        <h1>Main Menu</h1>

        <p>Please choose one of the options below. </p>

        <ul class="list-group">
          <li class="list-group-item"><a href="Actors.aspx" title="Actors">Manage Actors</a></li>
          <li class="list-group-item"><a href="Categories.aspx" title="Categories">Manage Categories</a></li>
          <li class="list-group-item"><a href="CreditCardTypes.aspx" title="Credit Card Type">Manage Credit Card Types</a></li>
          <li class="list-group-item"><a href="PaymentTypes.aspx" title="Payment Types">Manage Payment Types</a></li>
          <li class="list-group-item"><a href="PhoneTypes.aspx" title="Phone Types">Manage Phone Types</a></li>
        </ul>

        <div class="clearfix">&nbsp;</div>

        <div class="form-group">
          <asp:Button ID="btnLogout" cssclass="btn btn-danger btn-sm" runat="server" Text="Logout" ToolTip="Logout" OnClick="btnLogout_Click" />
        </div>
      </div>
    </div>
  </div>
</asp:Content>
