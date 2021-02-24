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
      this.Page.RegisterAsyncTask(new PageAsyncTask(LoadData));
    }
    private async Task LoadData()
    {
      if (!this.Page.IsPostBack)
      {
        await LoadCategories();
      }
    }
    private async Task LoadCategories()
    {
      MovieStore.business.Category category = new MovieStore.business.Category();
      this.lvCategories.DataSource = await category.getRecords();
      this.lvCategories.DataBind();
    }
    protected void lbtnAddCategory_Click(object sender, EventArgs e)
    {

    }
  }
}