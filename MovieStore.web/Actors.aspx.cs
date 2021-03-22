using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MovieStore.web
{
  public partial class Actors : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      this.Page.RegisterAsyncTask(new PageAsyncTask(LoadData));
    }
    private async Task LoadData()
    {
      if (!this.Page.IsPostBack)
      {

        MovieStore.business.Actor actor = new MovieStore.business.Actor();
        this.lvActors.DataSource = await actor.getActors();
        this.lvActors.DataBind();
      }

    }

    protected void lvActors_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
      if (e.CommandName.ToLower().Trim().Equals("editactor"))
      {
        Session.Add("actorid", e.CommandArgument.ToString().Trim());
        Response.Redirect("UpdateActor.aspx");
      }
    }

    protected void lbtnAddActor_Click(object sender, EventArgs e)
    {
      Response.Redirect("AddActor.aspx");

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
      this.Page.RegisterAsyncTask(new PageAsyncTask(SearchData));

    }
    private async Task SearchData()
    {
      MovieStore.business.Actor actor = new MovieStore.business.Actor()
      {
        FirstName = tboxFirstName.Text.Trim(),
        MiddleName = tboxMiddleName.Text.Trim(),
        LastName = tboxLastName.Text.Trim(),
      };
      this.lvActors.DataSource = await actor.searchActors();
      this.lvActors.DataBind();
    }
    protected async void btnClear_Click(object sender, EventArgs e)
    {
      MovieStore.business.Actor actor = new MovieStore.business.Actor();

      this.lvActors.DataSource = await actor.getActors();
      this.lvActors.DataBind();

      this.tboxFirstName.Text = "";
      this.tboxMiddleName.Text = "";
      this.tboxLastName.Text = "";
    }

  }
}