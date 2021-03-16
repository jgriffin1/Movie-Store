using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MovieStore.web
{
  public partial class CreditCardTypes : System.Web.UI.Page
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

      await LoadCreditCardTypes();

    }
    private async Task LoadCreditCardTypes()
    {
      MovieStore.business.CreditCardType creditcardtype = new MovieStore.business.CreditCardType();
      this.lvCreditCardTypes.DataSource = await creditcardtype.getRecords();
      this.lvCreditCardTypes.DataBind();
    }
    protected void lvCreditCardTypes_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
      if (e.CommandName.ToLower().Trim().Equals("editcreditcardtype"))
      {
        Session.Add("creditCardTypeId", e.CommandArgument.ToString().Trim());

        Response.Redirect("updateCreditCardType.aspx");
      }
    }

    protected void lbtnAddCreditCardType_Click(object sender, EventArgs e)
    {
      Response.Redirect("AddCreditCardType.aspx");

    }
  }
}