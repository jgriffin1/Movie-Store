using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MovieStore.web
{
  public partial class AddMember : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      this.Page.RegisterAsyncTask(new PageAsyncTask(LoadDDLs));

    }
    private async Task LoadDDLs()
    {
      if (!this.Page.IsPostBack)
      {
        MovieStore.business.Role role = new MovieStore.business.Role();
        this.ddlRoles.DataSource = await role.getRecords();
        this.ddlRoles.DataBind();

        this.ddlRoles.Items.Insert(0, new ListItem("", ""));

        this.ddlPhoneTypes.DataSource = await new MovieStore.business.PhoneType().getRecords();
        this.ddlPhoneTypes.DataBind();
      }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
      Response.Redirect("Members.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
      if (this.Page.IsValid)
      {
        this.Page.RegisterAsyncTask(new PageAsyncTask(SaveData));
      }
    }
    public byte[] convertStreamToByteArray(Stream inputStream)
    {
      using (MemoryStream ms = new MemoryStream())
      {
        inputStream.CopyTo(ms);
        return ms.ToArray();
      }
    }
    private async Task SaveData()
    {
      MovieStore.business.Member member = new MovieStore.business.Member()
      {
        FirstName = tboxFirstName.Text.Trim(),
        MiddleName = tboxMiddleName.Text.Trim(),
        LastName = tboxLastName.Text.Trim(),
        Address = new business.Address
        {
          AddressLine1 = tboxAddress1.Text.Trim(),
          AddressLine2 = tboxAddress2.Text.Trim(),
          City = tboxCity.Text.Trim(),
          State = ddlStates.SelectedValue,
          ZipCode = tboxZipCode.Text.Trim(),
          IsPrimary = true
        },
        IsActive = true,
        Password = tboxPassword.Text,
        Phones = new List<business.Phone> {
           new business.Phone
           {
             PhoneNumber = tboxPhoneNumber.Text.Trim(),
             PhoneTypeId = Convert.ToInt32(ddlPhoneTypes.SelectedValue),

           } 
        },
        ProfilePicture = convertStreamToByteArray(this.fupProfilePicture.PostedFile.InputStream),
        RoleId = Convert.ToInt32(this.ddlRoles.SelectedValue.Trim()),
        UserName = tboxUsername.Text.Trim(),



      };
      await member.addPerson();
      Response.Redirect("Members.aspx", false);

    }

    protected void cvProfilePicture_ServerValidate(object source, ServerValidateEventArgs args)
    {
      int ImageMinumLength = 512;
      int ImageMaximumLength = 200000;
      if (!this.fupProfilePicture.PostedFile.InputStream.CanRead)
      {
        args.IsValid = false;
      }
      else
      {
        if (this.fupProfilePicture.PostedFile.ContentLength < ImageMinumLength)
        {
          args.IsValid = false;
        }
        else
        {
          if (this.fupProfilePicture.PostedFile.ContentLength > ImageMaximumLength)
          {
            args.IsValid = false;
          }
          else
          {
            if (!string.Equals(this.fupProfilePicture.PostedFile.ContentType, "image/jpg", StringComparison.OrdinalIgnoreCase) &&
              !string.Equals(this.fupProfilePicture.PostedFile.ContentType, "image/jpeg", StringComparison.OrdinalIgnoreCase) &&
              !string.Equals(this.fupProfilePicture.PostedFile.ContentType, "image/png", StringComparison.OrdinalIgnoreCase) &&
              !string.Equals(this.fupProfilePicture.PostedFile.ContentType, "image/gif", StringComparison.OrdinalIgnoreCase) &&
              !string.Equals(this.fupProfilePicture.PostedFile.ContentType, "image/x-png", StringComparison.OrdinalIgnoreCase) &&
              !string.Equals(this.fupProfilePicture.PostedFile.ContentType, "image/pjpeg", StringComparison.OrdinalIgnoreCase)
              )
            {
              args.IsValid = false;
            }
            else
            {
              var fileExtension = Path.GetExtension(this.fupProfilePicture.PostedFile.FileName);

              if (!string.Equals(fileExtension, ".jpg", StringComparison.OrdinalIgnoreCase) &&
                  !string.Equals(fileExtension, ".png", StringComparison.OrdinalIgnoreCase) &&
                  !string.Equals(fileExtension, ".gif", StringComparison.OrdinalIgnoreCase) &&
                  !string.Equals(fileExtension, ".jpeg", StringComparison.OrdinalIgnoreCase)
                )
              {
                args.IsValid = false;
              }
            }
          }
        }
      }
    }
  }
}