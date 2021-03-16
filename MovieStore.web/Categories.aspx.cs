using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MovieStore.web
{
  public partial class Categories : System.Web.UI.Page
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

      await LoadCategories();

    }
    private async Task LoadCategories()
    {
      MovieStore.business.Category category = new MovieStore.business.Category();
      this.lvCategories.DataSource = await category.getRecords();
      this.lvCategories.DataBind();
    }
    protected void lbtnAddCategory_Click(object sender, EventArgs e)
    {
      Response.Redirect("AddCategory.aspx");
    }

    protected void lvCategories_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
      if (e.CommandName.ToLower().Trim().Equals("editcategory"))
      {
        Session.Add("categoryId", e.CommandArgument.ToString().Trim());

        Response.Redirect("UpdateCategory.aspx");
      }
    }
  }
}