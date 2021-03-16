<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" async="true" CodeBehind="CreditCardTypes.aspx.cs" Inherits="MovieStore.web.CreditCardTypes" %>

<asp:Content ID="cCreditCardTypes" ContentPlaceHolderID="cphMain" runat="server">
  <div class="container">
    <div class="row">
      <div class="col-md-12">
        <h1>Credit Card Types</h1>

        <p>Please review information below</p>

        <asp:ListView ID="lvCreditCardTypes" runat="server" OnItemCommand="lvCreditCardTypes_ItemCommand" ItemPlaceholderID="ItemContainer" ClientIDMode="AutoID" ItemType="MovieStore.business.CreditCardType">
          <LayoutTemplate>

            <table class="table">
              <thead>
                <th>Name</th>
                <%--<th>Created On</th>
                <th>Updated On</th>--%>
                <th>Actions</th>
              </thead>
              <tbody>
                <asp:PlaceHolder ID="ItemContainer" runat="server" />
              </tbody>
              <tfoot>
                <tr>
                  <td colspan="4">
                    <asp:LinkButton ID="lbtnAddCreditCardType" runat="server" Text="Add Credit Card Type" ToolTip="Add Credit Card Type" OnClick="lbtnAddCreditCardType_Click" />
                  </td>
                </tr>
              </tfoot>
            </table>
          </LayoutTemplate>
          <ItemTemplate>
            <tr>
              <td>
                <asp:Label ID="lblName" runat="server" Text='<%#: Item.Name %>' /></td>
              <%--<td>
                <asp:Label ID="lblDateCreated" runat="server" Text='<%#: Item.DateCreated.ToString("MM/dd/yyyy") %>' /></td>
              <td>
                <asp:Label ID="lblDateUpdated" runat="server" Text='<%#: Item.DateUpdated.HasValue ? Item.DateUpdated.Value.ToString("MM/dd/yyyy") : "" %>' /></td>--%>
              <td>
                <asp:LinkButton ID="lbtnEdit" runat="server" Text="Edit" ToolTip="Edit" CausesValidation="false" CommandName="editcreditcardtype" CommandArgument="<%#: Item.Id %>" /></td>
            </tr>
          </ItemTemplate>
          <EmptyDataTemplate>
            <p>No Record has been added yet.</p>
          </EmptyDataTemplate>
        </asp:ListView>
      </div>
    </div>
  </div>

</asp:Content>
