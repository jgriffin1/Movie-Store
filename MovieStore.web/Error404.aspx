<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Error404.aspx.cs" Inherits="MovieStore.web.Error404" %>

<asp:Content ID="cError404" ContentPlaceHolderID="cphMain" runat="server">
  <div class="container">
    <div class="row">
      <div class="col-md-12">
        <h1>ERROR</h1>
        <p>... --- ... </p>
        <p>Why Hello there</p>
        <p>Not sure what you expected to find here....</p>
        <p>but whatever it was, it isn't here.</p>

        <div class="form-group">
          <asp:Button ID="btnContinue" runat="server" CssClass="btn btn-warning btn-sm" Text="Continue" ToolTip="Continue" OnClick="btnContinue_Click" />
        </div>
      </div>
    </div>
  </div>
</asp:Content>
