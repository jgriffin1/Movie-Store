<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="UpdateAddressInfo.aspx.cs" Inherits="MovieStore.web.UpdateAddressInfo" Async="true" %>

<asp:Content ID="cpUpdateAddressInfo" ContentPlaceHolderID="cphMain" runat="server">
  <%--<script type="text/javascript">
    function openModal() {
      $('pnlDeleteModal').modal('show');
    }
  </script>--%>

  <asp:UpdatePanel ID="upnlAddressInfo" runat="server">
    <ContentTemplate>
      <div class="container">
        <div class="row">
          <div class="col-md-12">
            <h1>Address Information</h1>
            <p>Please review information below.</p>

            <asp:ListView ID="lvAddresses" runat="server" ItemPlaceholderID="ItemContainer" ClientIDMode="AutoID" ItemType="MovieStore.business.Address" OnItemCommand="lvAddresses_ItemCommand">
              <LayoutTemplate>
                <div id="data" class="bootstrap-replace-table-responsive">
                  <table class="table">
                    <thead>
                      <tr>
                        <th>Is Primary</th>
                        <th>Address Line 1</th>
                        <th>Address Line 2</th>
                        <th>City</th>
                        <th>State</th>
                        <th>Zip Code</th>
                        <th>Actions</th>
                      </tr>
                    </thead>
                    <tbody>
                      <asp:PlaceHolder ID="ItemContainer" runat="server" />
                    </tbody>
                    <tfoot>
                      <tr>
                        <td colspan="6">
                          <asp:LinkButton ID="lbtnAddAddress" runat="server" Text="Add Address" ToolTip="Add Address" OnClick="lbtnAddAddress_Click" />
                        </td>
                      </tr>
                    </tfoot>
                  </table>
                </div>
              </LayoutTemplate>
              <ItemTemplate>
                <tr>
                  <td data-label="Is Primary">
                    <asp:Label ID="lblIsPrimary" runat="server" Text='<%# Item.IsPrimary == true ? "Yes" : "No" %>' />
                  </td>
                  <td data-label="Address Line 1">
                    <asp:Label ID="lblAddressLine1" runat="server" Text='<%#: Item.AddressLine1 %>' />
                  </td>
                  <td data-label="Address Line 2">
                    <asp:Label ID="lblAddressLine2" runat="server" Text='<%#: Item.AddressLine2 %>' />
                  </td>
                  <td data-label="City">
                    <asp:Label ID="lblCity" runat="server" Text='<%#: Item.City %>' />
                  </td>
                  <td data-label="State">
                    <asp:Label ID="lblState" runat="server" Text='<%#: Item.State %>' />
                  </td>
                  <td data-label="Zip Code">
                    <asp:Label ID="lblZipCode" runat="server" Text='<%#: Item.ZipCode %>' />
                  </td>
                  <td data-label="Actions">
                    <asp:LinkButton ID="lbtnEditAddress" runat="server" Text="Edit" ToolTip="Edit"
                      CommandArgument='<%#: Item. Id %>' CommandName="EditAddress" />
                    |
              <asp:LinkButton ID="IbtnDeleteAddress" runat="server" Text="Delete" ToolTip="Delete"
                CommandArgument='<%#: Item.Id %>' CommandName="DeleteAddress" />
                    |
              <asp:LinkButton ID="IbtnSetAsPrimaryAddress" runat="server" Text="Set as Primary" ToolTip="Set as Primary"
                CommandArgument='<%#: Item.Id %>' CommandName="SetAsPrimaryAddress" />
                  </td>
                </tr>
              </ItemTemplate>
            </asp:ListView>

            <asp:Panel ID="pnlAddEditAddress" runat="server" Visible="false" CssClass="card">
              <div class="card-header">
                Address Information
              </div>
              <div class="card-body">
                <div class="form-group">
                  <asp:Label ID="lblAdresss1" runat="server" AssociatedControlID="tboxAddress1" Text="* Address" />
                  <div class="input-group mb-3">
                    <div class="input-group-prepend">
                      <span class="input-group-text">
                        <span class="fa fa-user"></span>
                      </span>
                    </div>
                    <asp:TextBox ID="tboxAddress1" MaxLength="30" runat="server" CssClass="form-control" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterMode="ValidChars"
                      ValidChars="  _-!?#$%^=&<>()*" FilterType="LowercaseLetters, UppercaseLetters, Numbers, Custom" TargetControlID="tboxAddress1" />
                  </div>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tboxAddress1"
                    ErrorMessage="Username is required." SetFocusOnError="true" EnableClientScript="true" Display="None" />
                </div>

                <div class="form-group">
                  <asp:Label ID="lblAddress2" runat="server" AssociatedControlID="tboxAddress2" Text="Address 2" />
                  <div class="input-group mb-3">
                    <div class="input-group-prepend">
                      <span class="input-group-text">
                        <span class="fa fa-user"></span>
                      </span>
                    </div>
                    <asp:TextBox ID="tboxAddress2" MaxLength="30" runat="server" CssClass="form-control" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterMode="ValidChars"
                      ValidChars="  _-!?#$%^=&<>()*" FilterType="LowercaseLetters, UppercaseLetters, Numbers, Custom" TargetControlID="tboxAddress2" />
                  </div>
                </div>

                <div class="form-group">
                  <asp:Label ID="lblCity" runat="server" AssociatedControlID="tboxCity" Text="* City" />
                  <div class="input-group mb-3">
                    <div class="input-group-prepend">
                      <span class="input-group-text">
                        <span class="fa fa-user"></span>
                      </span>
                    </div>
                    <asp:TextBox ID="tboxCity" MaxLength="30" runat="server" CssClass="form-control" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterMode="ValidChars"
                      ValidChars="  _-!?#$%^=&<>()*" FilterType="LowercaseLetters, UppercaseLetters, Numbers, Custom" TargetControlID="tboxCity" />
                  </div>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tboxCity"
                    ErrorMessage="City is required." SetFocusOnError="true" EnableClientScript="true" Display="None" />
                </div>

                <div class="form-group">
                  <asp:Label ID="lblState" runat="server" AssociatedControlID="ddlStates" Text="State" />
                  <div class="input-group mb-3">
                    <div class="input-group-prepend">
                      <span class="input-group-text">
                        <span class="fa fa-addresss-book"></span>
                      </span>
                    </div>
                    <asp:DropDownList ID="ddlStates" runat="server" CssClass="form-control">
                      <asp:ListItem Value=""></asp:ListItem>
                      <asp:ListItem Value="AL">Alabama</asp:ListItem>
                      <asp:ListItem Value="AK">Alaska</asp:ListItem>
                      <asp:ListItem Value="AZ">Arizona</asp:ListItem>
                      <asp:ListItem Value="AR">Arkansas</asp:ListItem>
                      <asp:ListItem Value="CA">California</asp:ListItem>
                      <asp:ListItem Value="CO">Colorado</asp:ListItem>
                      <asp:ListItem Value="CT">Connecticut</asp:ListItem>
                      <asp:ListItem Value="DC">District of Columbia</asp:ListItem>
                      <asp:ListItem Value="DE">Delaware</asp:ListItem>
                      <asp:ListItem Value="FL">Florida</asp:ListItem>
                      <asp:ListItem Value="GA">Georgia</asp:ListItem>
                      <asp:ListItem Value="HI">Hawaii</asp:ListItem>
                      <asp:ListItem Value="ID">Idaho</asp:ListItem>
                      <asp:ListItem Value="IL">Illinois</asp:ListItem>
                      <asp:ListItem Value="IN">Indiana</asp:ListItem>
                      <asp:ListItem Value="IA">Iowa</asp:ListItem>
                      <asp:ListItem Value="KS">Kansas</asp:ListItem>
                      <asp:ListItem Value="KY">Kentucky</asp:ListItem>
                      <asp:ListItem Value="LA">Louisiana</asp:ListItem>
                      <asp:ListItem Value="ME">Maine</asp:ListItem>
                      <asp:ListItem Value="MD">Maryland</asp:ListItem>
                      <asp:ListItem Value="MA">Massachusetts</asp:ListItem>
                      <asp:ListItem Value="MI">Michigan</asp:ListItem>
                      <asp:ListItem Value="MN">Minnesota</asp:ListItem>
                      <asp:ListItem Value="MS">Mississippi</asp:ListItem>
                      <asp:ListItem Value="MO">Missouri</asp:ListItem>
                      <asp:ListItem Value="MT">Montana</asp:ListItem>
                      <asp:ListItem Value="NE">Nebraska</asp:ListItem>
                      <asp:ListItem Value="NV">Nevada</asp:ListItem>
                      <asp:ListItem Value="NH">New Hampshire</asp:ListItem>
                      <asp:ListItem Value="NJ">New Jersey</asp:ListItem>
                      <asp:ListItem Value="NM">New Mexico</asp:ListItem>
                      <asp:ListItem Value="NY">New York</asp:ListItem>
                      <asp:ListItem Value="NC">North Carolina</asp:ListItem>
                      <asp:ListItem Value="ND">North Dakota</asp:ListItem>
                      <asp:ListItem Value="OH">Ohio</asp:ListItem>
                      <asp:ListItem Value="OK">Oklahoma</asp:ListItem>
                      <asp:ListItem Value="OR">Oregon</asp:ListItem>
                      <asp:ListItem Value="PA">Pennsylvania</asp:ListItem>
                      <asp:ListItem Value="RI">Rhode Island</asp:ListItem>
                      <asp:ListItem Value="SC">South Carolina</asp:ListItem>
                      <asp:ListItem Value="SD">South Dakota</asp:ListItem>
                      <asp:ListItem Value="TN">Tennessee</asp:ListItem>
                      <asp:ListItem Value="TX">Texas</asp:ListItem>
                      <asp:ListItem Value="UT">Utah</asp:ListItem>
                      <asp:ListItem Value="VT">Vermont</asp:ListItem>
                      <asp:ListItem Value="VA">Virginia</asp:ListItem>
                      <asp:ListItem Value="WA">Washington</asp:ListItem>
                      <asp:ListItem Value="WV">West Virginia</asp:ListItem>
                      <asp:ListItem Value="WI">Wisconsin</asp:ListItem>
                      <asp:ListItem Value="WY">Wyoming</asp:ListItem>
                    </asp:DropDownList>
                  </div>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlStates"
                    ErrorMessage="Please Select a State." InitialValue="" SetFocusOnError="true" EnableClientScript="true" Display="None" />
                </div>

                <div class="form-group">
                  <asp:Label ID="lblZipCode" runat="server" AssociatedControlID="tboxZipCode" Text="* Zip Code" />
                  <div class="input-group mb-3">
                    <div class="input-group-prepend">
                      <span class="input-group-text">
                        <span class="fa fa-user"></span>
                      </span>
                    </div>
                    <asp:TextBox ID="tboxZipCode" MaxLength="30" runat="server" CssClass="form-control" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterMode="ValidChars"
                      ValidChars="0123456789" FilterType="Custom, Numbers" TargetControlID="tboxZipCode" />
                  </div>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tboxZipCode"
                    ErrorMessage="Zip Code is required." SetFocusOnError="true" EnableClientScript="true" Display="None" />
                </div>
                <div class="clearfix">&nbsp;</div>

                <div class="form-group">
                  <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false" OnClick="btnCancel_Click" CssClass="btn btn-dark btn-sm" />
                  <asp:Button ID="btnSave" runat="server" Text="Save" ToolTip="Save" CausesValidation="false" OnClick="btnSave_Click" CssClass="btn btn-success btn-sm" />
                </div>
              </div>


            </asp:Panel>
            <div class="clearfix">&nbsp;</div>

            <div class="form-group">
              <asp:Button ID="btnReturn" runat="server" Text="Return" ToolTip="Return" CausesValidation="false" OnClick="btnReturn_Click" CssClass="btn btn-primary btn-sm" />
            </div>
          </div>
        </div>
      </div>
    </ContentTemplate>
  </asp:UpdatePanel>

  <asp:Panel CssClass="modal fade" runat="server" ID="pnlDeleteModal" ClientIDMode="Static" TabIndex="-1" role="dialog" aria-labelledby="deleteModalLabel">
    <div class="modal-dialog modal-xl" role="document">
      <div class="modal-content">
        <asp:UpdatePanel ID="upnlDeleteModal" runat="server" UpdateMode="Conditional">
          <ContentTemplate>
            <div class="modal-header">
              <h4 class="modal-title" id="deleteModalLabel">Delete Address
              </h4>
              <button type="button" class="close" data-dismiss="modal">&times;</button>
            </div>
            <div class="modal-footer">
              <asp:Button ID="btnDelete" runat="server" Text="Delete" ToolTip="Delete" CausesValidation="false" OnClick="btnDelete_Click" CssClass="btn btn-danger btn-sm" />
            </div>
          </ContentTemplate>
        </asp:UpdatePanel>
      </div>
    </div>
  </asp:Panel>
</asp:Content>
