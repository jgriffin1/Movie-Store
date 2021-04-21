using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MovieStore.web
{
  public partial class UpdateCategory : System.Web.UI.Page
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
      MovieStore.business.Category category = new MovieStore.business.Category()
      {
        Id = Convert.ToInt32(Session["categoryId"].ToString().Trim())
      };
      category = await category.getRecord();

      if (category != null)
      {
        this.tboxCategoryName.Text = category.Name;
      }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
      Response.Redirect("Categories.aspx");

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
      MovieStore.business.Category category = new MovieStore.business.Category()
      {
        Name = this.tboxCategoryName.Text.Trim(),
        Id = Convert.ToInt32(Session["categoryId"].ToString())
        
      };
     
      await category.updateRecord();
      Response.Redirect("Categories.aspx", false);

    }
  }
}