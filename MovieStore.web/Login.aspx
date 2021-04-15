<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MovieStore.web.Login" Async="true" %>

<asp:Content ID="cLogin" ContentPlaceHolderID="cphMain" runat="server">
  <div class="container">
    <div class="row">
      <div class="col-sm-9 col-md-7 col-lg-5 mx-auto">

        <h1>Login</h1>
        <p>You are entering the movie store.</p>

        <asp:ValidationSummary ID="vsError" HeaderText="The following error(s) occured:" runat="server"
          DisplayMode="BulletList" CssClass="alert alert-danger text-danger" />

        <div class="card">
          <div class="card-header">
            Enter your username and password
          </div>
          <div class="card-body">
            <div class="form-group">
              <asp:Label ID="lblUsername" runat="server" AssociatedControlID="tboxUsername" Text="Username:" />
              <asp:TextBox ID="tboxUsername" runat="server" MaxLength="30" CssClass="form-control" />
            </div>
            <div class="form-group">
              <asp:Label ID="lblPassword" runat="server" AssociatedControlID="tboxPassword" Text="Password:" />
              <asp:TextBox ID="tboxPassword" TextMode="Password" runat="server" MaxLength="30" CssClass="form-control" />
            </div>
            
          </div>
          <div class="card-footer">
              Please click
              <asp:LinkButton ID="lbtnRegister" runat="server" Text="here" ToolTip="click here to register" OnClick="lbtnRegister_Click" />
              to register.
            </div>
        </div>

        <div class="clearfix">&nbsp;</div>

        <div class="form-group">
          <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-success btn-sm" Text="Login"
            OnClick="btnLogin_Click" />
          <asp:CustomValidator ID="cvLogin" runat="server" Display="None" CssClass="text-danger" ErrorMessage="Invalid Credentials." OnServerValidate="cvLogin_ServerValidate" />
        </div>
      </div>
    </div>
  </div>
</asp:Content>
