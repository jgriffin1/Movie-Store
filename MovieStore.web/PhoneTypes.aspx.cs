using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MovieStore.web
{
  public partial class PhoneTypes : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      this.Page.RegisterAsyncTask(new PageAsyncTask(LoadData));
    }
    private async Task LoadData()
    {
      if (!this.Page.IsPostBack)
      {
        MovieStore.business.PhoneType phoneType = new MovieStore.business.PhoneType();
        this.lvPhoneTypes.DataSource = await phoneType.getRecords();// new MovieStore.business.PhoneType().getRecords();
        this.lvPhoneTypes.DataBind();

      }
    }
    protected void lvPhoneTypes_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
      if (e.CommandName.ToLower().Trim().Equals("editphonetype"))
      {
        Session.Add("phoneTypeId", e.CommandArgument.ToString().Trim());

        Response.Redirect("UpdatePhoneType.aspx");
      }
    }

    protected void lbtnAddPhoneType_Click(object sender, EventArgs e)
    {
      Response.Redirect("AddPhoneType.aspx");
    }
  }
}