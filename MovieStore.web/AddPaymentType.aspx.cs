using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MovieStore.web
{
  public partial class AddPaymentType : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      if (this.Page.IsValid)
      {
        this.Page.RegisterAsyncTask(new PageAsyncTask(SaveData));
      }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
      Response.Redirect("PaymentTypes.aspx");
    }
    private async Task SaveData()
    {
      MovieStore.business.PaymentType PaymentType = new MovieStore.business.PaymentType()
      {
        Name = this.tboxPaymentType.Text.Trim()
      };
      await PaymentType.addRecord();
      Response.Redirect("PaymentTypes.aspx");

    }
  }
}