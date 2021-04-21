using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MovieStore.web
{
  public partial class UpdateActor : System.Web.UI.Page
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
      MovieStore.business.Actor actor = new MovieStore.business.Actor()
      {
        Id = Convert.ToInt32(Session["actorid"].ToString().Trim())
      };
      actor = await actor.getActor();

      if (actor != null)
      {
        this.tboxFirstName.Text = actor.FirstName;
        this.tboxMiddleName.Text = actor.MiddleName;
        this.tboxLastName.Text = actor.LastName;
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
      if (this.Page.IsValid)
      {
        MovieStore.business.Actor actor = new MovieStore.business.Actor()
        {
          Id = Convert.ToInt32(Session["actorId"].ToString()),
          FirstName = tboxFirstName.Text.Trim(),
          MiddleName = tboxMiddleName.Text.Trim(),
          LastName = tboxLastName.Text.Trim(),
        };

        await actor.updatePerson();
        Response.Redirect("Actors.aspx", false);
      }
      

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
      Response.Redirect("Actors.aspx");

    }
  }
}