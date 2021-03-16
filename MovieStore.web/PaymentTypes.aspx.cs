using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MovieStore.web
{
  public partial class PaymentTypes : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      this.Page.RegisterAsyncTask(new PageAsyncTask(LoadData));
    }
    private async Task LoadData()
    {
      if (!this.Page.IsPostBack)
      {
        MovieStore.business.PaymentType paymentType = new MovieStore.business.PaymentType();
        this.lvPaymentTypes.DataSource = await paymentType.getRecords();
        this.lvPaymentTypes.DataBind();

      }
    }

    protected void lvPaymentTypes_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
      if (e.CommandName.ToLower().Trim().Equals("editpaymenttype"))
      {
        Session.Add("PaymentTypeId", e.CommandArgument.ToString().Trim());

        Response.Redirect("UpdatePaymentType.aspx");
      }
    }

    protected void lbtnAddPaymentType_Click(object sender, EventArgs e)
    {
      Response.Redirect("AddPaymentType.aspx");

    }
  }
}