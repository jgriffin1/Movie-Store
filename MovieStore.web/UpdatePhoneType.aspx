<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="UpdatePhoneType.aspx.cs" Inherits="MovieStore.web.UpdatePhoneType" Async="true" %>
<asp:Content ID="cUpdatePhoneType" ContentPlaceHolderID="cphMain" runat="server">
   <div class="container">
    <div class="row">
      <div class="col-md-12">
        <h1>Update Phone Type</h1>

        <p>Please fill out all required fields and click save.</p>

        <asp:ValidationSummary ID="vsError" HeaderText="The following error(s) occured:" runat="server" DisplayMode="BulletList" CssClass="alert alert-danger" />
        <div class="card">
          <div class="card-header">
            Phone Types
          </div>
          <div class="card-body">
            <div class="form-group">
              <asp:Label ID="lblPhoneTypeName" runat="server" Text="* Phone Type Name" AssociatedControlID="tboxPhoneTypeName" />
              <div class="input-group-mb-3">
                <div class="input-group-prepend">
                  <span class="input-group-text">
                    <span class="fa fa-book"></span>
                  </span>

                  <asp:TextBox ID="tboxPhoneTypeName" runat="server" MaxLength="35" CssClass="form-control" />
                  <ajaxToolkit:FilteredTextBoxExtender ID="ftbePhoneTypeName" runat="server" FilterMode="ValidChars" ValidChars="  " FilterType="LowercaseLetters, UppercaseLetters, Numbers, Custom" TargetControlID="tboxPhoneTypeName" />
                </div>
              </div>
              <asp:RequiredFieldValidator ID="rfvPhoneTypeName" runat="server" ControlToValidate="tboxPhoneTypeName" ErrorMessage="Name is required." SetFocusOnError="true" EnableClientScript="true" Display="None" />
            </div>
          </div>
        </div>
        <div class="clearfix">&nbsp; </div>

        <div class="form-group">
          <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false" OnClick="btnCancel_Click" CssClass="btn btn-dark btn-sm" />
          <asp:Button ID="btnUpdate" runat="server" Text="Save" ToolTip="save" OnClick="btnUpdate_Click" CssClass="btn btn-success btn-sm" />
        </div>
      </div>
    </div>
  </div>
</asp:Content>
