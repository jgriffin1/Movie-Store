<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="MovieStore.web.Logout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
  <div class="row">
    <div class="col-md-12">
      <h1>You have successfully logged out</h1>
      <p>Click continue to log in again</p>

      <div class="form-group">
        <asp:Button ID="btnContinue" runat="server" CssClass="btn btn-warning btn-sm" Text="Continue" ToolTip="Continue" OnClick="btnContinue_Click"/>
      </div>
    </div>
  </div>
</asp:Content>
