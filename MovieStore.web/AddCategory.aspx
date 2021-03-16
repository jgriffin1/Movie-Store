<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AddCategory.aspx.cs" Inherits="MovieStore.web.AddCategory" Async="true" %>

<asp:Content ID="cAddCategory" ContentPlaceHolderID="cphMain" runat="server">
  <div class="container">
    <div class="row">
      <div class="col-md-12">
        <h1>Add Movie Category</h1>

        <p>Please fill out all required fields and click save.</p>

        <asp:ValidationSummary ID="vsError" HeaderText="The following error(s) occured:" runat="server" DisplayMode="BulletList" CssClass="alert alert-danger" />
        <div class="card">
          <div class="card-header">
            Category Information
          </div>
          <div class="card-body">
            <div class="form-group">
              <asp:Label ID="lblCategoryName" runat="server" Text="* Category Name" AssociatedControlID="tboxCategoryName" />
              <div class="input-group-mb-3">
                <div class="input-group-prepend">
                  <span class="input-group-text">
                    <span class="fa fa-book"></span>
                  </span>
                
                <asp:TextBox ID="tboxCategoryName" runat="server" MaxLength="35"  CssClass="form-control" />
                <ajaxToolkit:FilteredTextBoxExtender ID="ftbeCategoryName" runat="server" FilterMode="ValidChars" ValidChars="  " FilterType="LowercaseLetters, UppercaseLetters, Numbers, Custom" TargetControlID="tboxCategoryName" />
              </div>
                </div>
              <asp:RequiredFieldValidator ID="rfvCategoryName" runat="server" ControlToValidate="tboxCategoryName" ErrorMessage="Name is required." SetFocusOnError="true" EnableClientScript="true" Display="None" />
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
