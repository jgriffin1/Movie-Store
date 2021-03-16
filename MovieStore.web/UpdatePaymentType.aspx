<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" Async="true" CodeBehind="UpdatePaymentType.aspx.cs" Inherits="MovieStore.web.UpdatePaymentType" %>
<asp:Content ID="cUpdatePaymentType" ContentPlaceHolderID="cphMain" runat="server">
  <div class="container">
    <div class="row">
      <div class="col-md-12">
        <h1>Update Payment Type</h1>

        <p>Please fill out all required fields and click save.</p>

        <asp:ValidationSummary ID="vsError" HeaderText="The following error(s) occured:" runat="server" DisplayMode="BulletList" CssClass="alert alert-danger" />
        <div class="card">
          <div class="card-header">
            Payment Types
          </div>
          <div class="card-body">
            <div class="form-group">
              <asp:Label ID="lblPaymentTypeName" runat="server" Text="* Payment Type Name" AssociatedControlID="tboxPaymentTypeName" />
              <div class="input-group-mb-3">
                <div class="input-group-prepend">
                  <span class="input-group-text">
                    <span class="fa fa-book"></span>
                  </span>

                  <asp:TextBox ID="tboxPaymentTypeName" runat="server" MaxLength="35" CssClass="form-control" />
                  <ajaxToolkit:FilteredTextBoxExtender ID="ftbePaymentTypeName" runat="server" FilterMode="ValidChars" ValidChars="  " FilterType="LowercaseLetters, UppercaseLetters, Numbers, Custom" TargetControlID="tboxPaymentTypeName" />
                </div>
              </div>
              <asp:RequiredFieldValidator ID="rfvPaymentTypeName" runat="server" ControlToValidate="tboxPaymentTypeName" ErrorMessage="Name is required." SetFocusOnError="true" EnableClientScript="true" Display="None" />
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
</asp:Con
</asp:Content>
