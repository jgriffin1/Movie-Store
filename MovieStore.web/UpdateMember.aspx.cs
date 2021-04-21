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
  public partial class UpdateMember : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      this.Page.RegisterAsyncTask(new PageAsyncTask(LoadData));

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      if (this.Page.IsValid)
      {
        this.Page.RegisterAsyncTask(new PageAsyncTask(SaveData));
      }
    }
    private async Task LoadData()
    {
      if (!this.Page.IsPostBack)
      {
        business.Member member = new business.Member
        {
          Id = Convert.ToInt32(Session["memberId"].ToString().Trim())
        };
        member = await member.getMember();
        if (member != null)
        {
          this.tboxFirstName.Text = member.FirstName;
          this.tboxMiddleName.Text = member.MiddleName;
          this.tboxLastName.Text = member.LastName;
        }
      }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
      Response.Redirect("Members.aspx",false);

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
        Id=Convert.ToInt32(Session["memberId"].ToString().Trim()),
        FirstName = tboxFirstName.Text.Trim(),
        MiddleName = tboxMiddleName.Text.Trim(),
        LastName = tboxLastName.Text.Trim(),


      };
      if (this.fupProfilePicture.HasFile)
      {
        member.ProfilePicture = convertStreamToByteArray(this.fupProfilePicture.PostedFile.InputStream);
      }

      await member.updatePerson();
      Response.Redirect("Members.aspx", false);

    }

    protected void cvProfilePicture_ServerValidate(object source, ServerValidateEventArgs args)
    {
      if (this.fupProfilePicture.HasFile)
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

}