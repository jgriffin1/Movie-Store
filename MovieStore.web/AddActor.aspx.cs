using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MovieStore.web
{
  public partial class AddActor : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
      Response.Redirect("Actors.aspx");

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      if (this.Page.IsValid)
      {
        this.Page.RegisterAsyncTask(new PageAsyncTask(SaveData));
      }
    }
    private async Task SaveData()
    {
      MovieStore.business.Actor actor = new MovieStore.business.Actor()
      {
        FirstName = tboxFirstName.Text.Trim(),
         MiddleName=tboxMiddleName.Text.Trim(),
          LastName=tboxLastName.Text.Trim()
      }; 
      await actor.addPerson();
      Response.Redirect("Actors.aspx", false);

    }
  }
}