using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MovieStore.web
{
  public partial class UpdateCreditCardType : System.Web.UI.Page
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
      MovieStore.business.CreditCardType creditCardType = new MovieStore.business.CreditCardType()
      {
        Id = Convert.ToInt32(Session["creditCardTypeId"].ToString().Trim())
      };
      creditCardType = await creditCardType.getRecord();

      if (creditCardType != null)
      {
        this.tboxCreditCardType.Text = creditCardType.Name;
      }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
      Response.Redirect("CreditCardTypes.aspx");

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
      MovieStore.business.CreditCardType creditCardType = new MovieStore.business.CreditCardType()
      {
        Name = this.tboxCreditCardType.Text.Trim(),
        Id = Convert.ToInt32(Session["creditCardTypeId"].ToString())

      };

      await creditCardType.updateRecord();
      Response.Redirect("CreditCardTypes.aspx");

    }
  }
}