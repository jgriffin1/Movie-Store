<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="UpdateMember.aspx.cs" Inherits="MovieStore.web.UpdateMember" Async="True" %>

<asp:Content ID="cupdateMember" ContentPlaceHolderID="cphMain" runat="server">
  <div class="container">
    <div class="row">
      <div class="col-md-12">
        <h1>Update Member</h1>
        <p>Please review all of the required fields </p>

        <asp:ValidationSummary ID="vsError" HeaderText="The following error(s) occured:" runat="server" DisplayMode="BulletList" CssClass="alert alert-danger" />

        <div class="card">
          <div class="card-header">
            Member Information
          </div>
          <div class="card-body">

            <!--First Name-->
            <div class="form-group">
              <asp:Label ID="lblFirstName" runat="server" AssociatedControlID="tboxFirstName" Text="* First Name" />
              <div class="input-group mb-3">
                <div class="input-group-prepend">
                  <span class="input-group-text">
                    <span class="fa fa-user"></span>
                  </span>
                </div>
                <asp:TextBox ID="tboxFirstName" MaxLength="30" runat="server" CssClass="form-control" />
                <ajaxToolkit:FilteredTextBoxExtender ID="ftboxeFirstName" runat="server" FilterMode="ValidChars"
                  ValidChars="  " FilterType="LowercaseLetters, UppercaseLetters, Custom" TargetControlID="tboxFirstName" />
              </div>
              <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="tboxFirstName"
                ErrorMessage="First Name is required." SetFocusOnError="true" EnableClientScript="true" Display="None" />
            </div>
            <!--Middle-->
            <div class="form-group">
              <asp:Label ID="lblMiddleName" runat="server" AssociatedControlID="tboxMiddleName" Text="Middle Name" />
              <div class="input-group mb-3">
                <div class="input-group-prepend">
                  <span class="input-group-text">
                    <span class="fa fa-user"></span>
                  </span>
                </div>
                <asp:TextBox ID="tboxMiddleName" MaxLength="30" runat="server" CssClass="form-control" />
                <ajaxToolkit:FilteredTextBoxExtender ID="ftboxeMiddleName" runat="server" FilterMode="ValidChars"
                  ValidChars="  " FilterType="LowercaseLetters, UppercaseLetters, Custom" TargetControlID="tboxMiddleName" />
              </div>

            </div>
            <!--last name-->
            <div class="form-group">
              <asp:Label ID="lblLastName" runat="server" AssociatedControlID="tboxLastName" Text="* Last Name" />
              <div class="input-group mb-3">
                <div class="input-group-prepend">
                  <span class="input-group-text">
                    <span class="fa fa-user"></span>
                  </span>
                </div>
                <asp:TextBox ID="tboxLastName" MaxLength="30" runat="server" CssClass="form-control" />
                <ajaxToolkit:FilteredTextBoxExtender ID="ftboxeLastName" runat="server" FilterMode="ValidChars"
                  ValidChars="  " FilterType="LowercaseLetters, UppercaseLetters, Custom" TargetControlID="tboxLastName" />
              </div>
              <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="tboxLastName"
                ErrorMessage="Last Name is required." SetFocusOnError="true" EnableClientScript="true" Display="None" />
            </div>
            <!--Profile Picture-->
            <div class="form-group">
              <asp:Label ID="lblProfilePicture" runat="server" AssociatedControlID="fupProfilePicture" Text="* Profile Picture" />
              <div class="input-group mb-3">
                <div class="input-group-prepend">
                  <span class="input-group-text">
                    <span class="fa fa-image"></span>
                  </span>
                </div>
                <asp:FileUpload ID="fupProfilePicture" runat="server" AllowMultiple="false" CssClass="form-control" />
              </div>
              <asp:CustomValidator ID="cvProfilePicture" runat="server" ControlToValidate="fupProfilePicture" ErrorMessage="Invalid picture selection" Display="None" SetFocusOnError="true" OnServerValidate="cvProfilePicture_ServerValidate" />
            </div>
          </div>

        </div>

        <div class="clearfix">&nbsp;</div>

        <div class="form-group">
          <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false" OnClick="btnCancel_Click" CssClass="btn btn-dark btn-sm" />
          <asp:Button ID="btnSave" runat="server" Text="Save" ToolTip="save" OnClick="btnSave_Click" CssClass="btn btn-success btn-sm" />
        </div>
      </div>
    </div>
  </div>
</asp:Content>
