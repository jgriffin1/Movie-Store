using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MovieStore.web
{
  public partial class UpdatePhoneInfo : System.Web.UI.Page
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
      MovieStore.business.Member member = new MovieStore.business.Member
      {
        Id = Convert.ToInt32(Session["memberId"].ToString().Trim())
      };
      this.lvPhones.DataSource = await member.getPhones();
      this.lvPhones.DataBind();

      var pt = new MovieStore.business.PhoneType();
      this.ddlPhoneTypes.DataSource = await pt.getRecords();
      this.ddlPhoneTypes.DataBind();

    }
    protected void lvPhones_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
      {
        if (e.CommandName.ToLower().Trim().Equals("editphone"))
        {
          this.pnlAddEditPhone.Visible = true;
          this.tboxPhoneNumber.Focus();

          Session.Add("phoneId", e.CommandArgument.ToString().Trim());
          Session.Add("action", "edit");

          this.Page.RegisterAsyncTask(new PageAsyncTask(LoadPhone));
        }
        else if (e.CommandName.ToLower().Trim().Equals("deletephone"))
        {
          Session.Add("phoneId", e.CommandArgument.ToString().Trim());
          this.Page.RegisterAsyncTask(new PageAsyncTask(DeleteData));
          this.pnlAddEditPhone.Visible = false;

          this.Page.RegisterAsyncTask(new PageAsyncTask(LoadData));

        }
      }
    }
    private async Task LoadPhone()
    {
      MovieStore.business.Member member = new MovieStore.business.Member
      {
        phone = new business.Phone
        {
          Id = Convert.ToInt32(Session["phoneId"].ToString().Trim())
        }
      };
      business.Phone phone = await member.getPhone();

      if (phone != null)
      {
        this.tboxPhoneNumber.Text = phone.PhoneNumber;
        this.ddlPhoneTypes.SelectedValue = phone.PhoneTypeId.ToString();
      }
    }
    protected void lbtnAddPhone_Click(object sender, EventArgs e)
    {
      this.pnlAddEditPhone.Visible = true;

      this.tboxPhoneNumber.Focus();

      Session.Add("action", "add");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      this.Page.RegisterAsyncTask(new PageAsyncTask(saveData));

    }
    private async Task saveData()
    {
      //if (this.Page.IsValid)
      //{
      if (Session["action"] != null && !string.IsNullOrEmpty(Session["action"].ToString().Trim()))
      {
        string action = Session["action"].ToString().Trim();

        MovieStore.business.Member member = new MovieStore.business.Member
        {
          Id = Convert.ToInt32(Session["memberId"].ToString().Trim()),
          phone = new business.Phone
          {
            PhoneNumber = this.tboxPhoneNumber.Text.Trim(),
            PhoneTypeId = Convert.ToInt32(this.ddlPhoneTypes.SelectedValue.Trim())
          }
        };
        if (action.Equals("add", StringComparison.OrdinalIgnoreCase))
        {

          await member.addPhone();

          this.pnlAddEditPhone.Visible = false;

          ClearData();
          await LoadData();

        }
        else if (action.Equals("edit", StringComparison.OrdinalIgnoreCase))
        {
          member.phone.Id = Convert.ToInt32(Session["phoneId"].ToString().Trim());
          await member.updatePhone();

          ClearData();

          this.pnlAddEditPhone.Visible = false;
        }
      }
      //}
    }
    private void ClearData()
    {
      this.tboxPhoneNumber.Text = "";
      this.ddlPhoneTypes.ClearSelection();

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
      this.pnlAddEditPhone.Visible = false;
      ClearData();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
      this.Page.RegisterAsyncTask(new PageAsyncTask(DeleteData));
    }
    private async Task DeleteData()
    {
      MovieStore.business.Member member = new MovieStore.business.Member
      {
        Id = Convert.ToInt32(Session["memberId"].ToString().Trim()),
        phone = new business.Phone
        {
          Id = Convert.ToInt32(Session["phoneId"].ToString().Trim())
        }
      };
      await member.deletePhone();
      await LoadData();
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
      Response.Redirect("Members.aspx", false);

    }
  }
}