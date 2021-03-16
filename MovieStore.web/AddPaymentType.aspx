<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AddPaymentType.aspx.cs" Inherits="MovieStore.web.AddPaymentType" Async="true" %>
<asp:Content ID="cAddPaymentType" ContentPlaceHolderID="cphMain" runat="server">
   <div class="container">
    <div class="row">
      <div class="col-md-12">
        <h1>Add Payment Type</h1>

        <p>Please fill out all required fields and click save.</p>

        <asp:ValidationSummary ID="vsError" HeaderText="The following error(s) occured:" runat="server" DisplayMode="BulletList" CssClass="alert alert-danger" />
        <div class="card">
          <div class="card-header">
            Payment Type Information
          </div>
          <div class="card-body">
            <div class="form-group">
              <asp:Label ID="lblPaymentTypeName" runat="server" Text="* Payment Type Name" AssociatedControlID="tboxPaymentType" />
              <div class="input-group-mb-3">
                <div class="input-group-prepend">
                  <span class="input-group-text">
                    <span class="fa fa-book"></span>
                  </span>

                  <asp:TextBox ID="tboxPaymentType" runat="server" MaxLength="35" CssClass="form-control" />
                  <ajaxToolkit:FilteredTextBoxExtender ID="ftboxePaymentType" runat="server" FilterMode="ValidChars" 
                    ValidChars="  " FilterType="LowercaseLetters, UppercaseLetters, Numbers, Custom" 
                    TargetControlID="tboxPaymentType" />
                </div>
                
              </div>
              <asp:RegularExpressionValidator ID="revPaymentType" runat="server" ControlToValidate="tboxPaymentType"
                  display="None" EnableClientScript="true" ErrorMessage="Invalid Entry. Maximum of 10 characters." ValidationExpression="^.{1,10}$"
                  cssclass="text-danger" SetFocusOnError="true" />
              <asp:RequiredFieldValidator ID="rfvPaymentTypeName" runat="server" ControlToValidate="tboxPaymentType" ErrorMessage="Payment Type is required." 
                SetFocusOnError="true" EnableClientScript="true" Display="None" />
            </div>
          </div>
        </div>
        <div class="clearfix">&nbsp; </div>
        <div class="form-group">
          <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false" OnClick="btnCancel_Click" CssClass="btn btn-dark btn-sm" />
          <asp:Button ID="btnSave" runat="server" Text="Save" ToolTip="save" OnClick="btnSave_Click" CssClass="btn btn-success btn-sm" />
        </div>
      </div>
    </div>
  </div>
</asp:Content>
