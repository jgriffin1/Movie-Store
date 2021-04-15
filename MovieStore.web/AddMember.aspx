<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AddMember.aspx.cs" Inherits="MovieStore.web.AddMember" Async="true"%>
<asp:Content ID="cAddMember" ContentPlaceHolderID="cphMain" runat="server">
  <div class="container">
    <div class="row">
      <div class="col-md-12">
        <h1>Add Member</h1>

        <p>Please fill out all required fields and click save.</p>

        <asp:ValidationSummary ID="vsError" HeaderText="The following error(s) occured:" runat="server" DisplayMode="BulletList" CssClass="alert alert-danger" />
        <div class="card">
          <div class="card-header">
            Member Information
          </div>
          <div class="card-body">
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

            <div class="form-group">
              <asp:Label ID="lblUsername" runat="server" AssociatedControlID="tboxUsername" Text="* UserName" />
              <div class="input-group mb-3">
                <div class="input-group-prepend">
                  <span class="input-group-text">
                    <span class="fa fa-user"></span>
                  </span>
                </div>
                <asp:TextBox ID="tboxUsername" MaxLength="30" runat="server" CssClass="form-control" />
                <ajaxToolkit:FilteredTextBoxExtender ID="ftboxeUsername" runat="server" FilterMode="ValidChars"
                  ValidChars="  _-!?#$%^=&<>()*" FilterType="LowercaseLetters, UppercaseLetters, Numbers, Custom" TargetControlID="tboxUsername" />
              </div>
              <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="tboxUsername"
                ErrorMessage="Username is required." SetFocusOnError="true" EnableClientScript="true" Display="None" />
            </div>

            <div class="form-group">
              <asp:Label ID="lblPassword" runat="server" AssociatedControlID="tboxPassword" Text="* Password" />
              <div class="input-group mb-3">
                <div class="input-group-prepend">
                  <span class="input-group-text">
                    <span class="fa fa-lock"></span>
                  </span>
                </div>
                <asp:TextBox ID="tboxPassword" TextMode="Password" MaxLength="30" runat="server" CssClass="form-control" />
                <ajaxToolkit:FilteredTextBoxExtender ID="ftboxePassword" runat="server" FilterMode="ValidChars"
                  ValidChars="  !@#$%^&*()_-`~=+<>,.?/\|'" FilterType="LowercaseLetters, UppercaseLetters, Numbers, Custom" TargetControlID="tboxPassword" />
              </div>
              <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="tboxPassword"
                ErrorMessage="Password is required." SetFocusOnError="true" EnableClientScript="true" Display="None" />
            </div>

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
              <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tboxAddress2"
                ErrorMessage="Username is required." SetFocusOnError="true" EnableClientScript="true" Display="None" />--%>
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

            <!-- Phone Type-->
            <div class="form-group">
              <asp:Label ID="lblPhoneTYpe" runat="server" AssociatedControlID="ddlPhoneTypes" Text="Phone Type" />
              <div class="input-group mb-3">
                <div class="input-group-prepend">
                  <span class="input-group-text">
                    <span class="fa fa-phone"></span>
                  </span>
                </div>
                <asp:DropDownList ID="ddlPhoneTypes" runat="server" DataTextField="Name" DataValueField="Id" CssClass="form-control" />
              </div>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlPhoneTypes"
                ErrorMessage="Please Select a phone type." InitialValue="" SetFocusOnError="true" EnableClientScript="true" Display="None" />
            </div>

            <!-- Phone Number-->
            <div class="form-group">
              <asp:Label ID="lblPhoneNumber" runat="server" AssociatedControlID="tboxPhoneNumber" Text="* Phone Number" />
              <div class="input-group mb-3">
                <div class="input-group-prepend">
                  <span class="input-group-text">
                    <span class="fa fa-phone"></span>
                  </span>
                </div>
                <asp:TextBox ID="tboxPhoneNumber" MaxLength="14" runat="server" CssClass="form-control" />
                <ajaxToolkit:MaskedEditExtender id="meePhoneNumber" runat="server" TargetControlID="tboxPhoneNumber" AcceptNegative="None" MaskType="Number" InputDirection="LeftToRight" Mask="(999) 999-9999"/>
                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterMode="ValidChars"
                  ValidChars=" 0123456789-_()" FilterType="Custom, Numbers" TargetControlID="tboxPhoneNumber" />
              </div>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator6" InitialValue="(___) ___-____" runat="server" ControlToValidate="tboxPhoneNumber"
                ErrorMessage="Phone Number is required." SetFocusOnError="true" EnableClientScript="true" Display="None" />
            </div>

            <!-- Role Type-->
            <div class="form-group">
              <asp:Label ID="lblRoleType" runat="server" AssociatedControlID="ddlRoles" Text="* Role" />
              <div class="input-group mb-3">
                <div class="input-group-prepend">
                  <span class="input-group-text">
                    <span class="fa fa-key"></span>
                  </span>
                </div>
                <asp:DropDownList ID="ddlRoles" runat="server" DataTextField="Name" DataValueField="Id" CssClass="form-control" />
              </div>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlRoles"
                ErrorMessage="Please Select a role type." InitialValue="" SetFocusOnError="true" EnableClientScript="true" Display="None" />
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
              <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="fupProfilePicture" ErrorMessage="Profile picture is required" SetFocusOnError="true" />
              <asp:CustomValidator ID="cvProfilePicture" runat="server" ControlToValidate="fupProfilePicture" ErrorMessage="Invalid picture selection" Display="None" SetFocusOnError="true" OnServerValidate="cvProfilePicture_ServerValidate" />
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
