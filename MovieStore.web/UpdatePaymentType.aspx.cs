using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MovieStore.web
{
  public partial class UpdatePaymentType : System.Web.UI.Page
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
      MovieStore.business.PaymentType paymentType = new MovieStore.business.PaymentType()
      {
        Id = Convert.ToInt32(Session["PaymentTypeId"].ToString().Trim())
      };
      paymentType = await paymentType.getRecord();

      if (paymentType != null)
      {
        this.tboxPaymentTypeName.Text = paymentType.Name;
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
      MovieStore.business.PaymentType PaymentType = new MovieStore.business.PaymentType()
      {
        Name = this.tboxPaymentTypeName.Text.Trim(),
        Id = Convert.ToInt32(Session["PaymentTypeId"].ToString())

      };

      await PaymentType.updateRecord();
      Response.Redirect("PaymentTypes.aspx", false);

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
      Response.Redirect("PaymentTypes.aspx", false);

    }
  }
}