using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MovieStore.web
{
  public partial class UpdatePhoneType : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!this.Page.IsPostBack)
      {
        this.Page.RegisterAsyncTask(new PageAsyncTask(LoadData));

      }

    }
    private async Task LoadData()
    {
      MovieStore.business.PhoneType phoneType = new MovieStore.business.PhoneType()
      {
        Id = Convert.ToInt32(Session["PhoneTypeId"].ToString().Trim())
      };
      phoneType = await phoneType.getRecord();

      if (phoneType != null)
      {
        this.tboxPhoneTypeName.Text = phoneType.Name;
      }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
      if (this.Page.IsValid)
      {
        this.Page.RegisterAsyncTask(new PageAsyncTask(SaveData));
      }

    }
    private async Task SaveData()
    {
      MovieStore.business.PhoneType PhoneType = new MovieStore.business.PhoneType()
      {
        Name = this.tboxPhoneTypeName.Text.Trim(),
        Id = Convert.ToInt32(Session["phoneTypeId"].ToString())

      };

      await PhoneType.updateRecord();
      Response.Redirect("PhoneTypes.aspx");

    }
     
    protected void btnCancel_Click(object sender, EventArgs e)
    {
      Response.Redirect("PhoneTypes.aspx");

    }
  }
}