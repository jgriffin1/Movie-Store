using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MovieStore.web
{
  public partial class UpdateAddressInfo : System.Web.UI.Page
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
      this.lvAddresses.DataSource = await member.getAddresses();
      this.lvAddresses.DataBind();
    }
    protected void lbtnAddAddress_Click(object sender, EventArgs e)
    {
      this.pnlAddEditAddress.Visible = true;

      this.tboxAddress1.Focus();

      Session.Add("action", "add");

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
      this.pnlAddEditAddress.Visible = false;
      ClearData();
    }
    private void ClearData()
    {
      this.tboxAddress1.Text = "";
      this.tboxAddress2.Text = "";
      this.tboxCity.Text = "";
      this.tboxZipCode.Text = "";
      this.ddlStates.ClearSelection();

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
      this.Page.RegisterAsyncTask(new PageAsyncTask(saveData));


    }
    public async Task saveData()
    {
      //if (this.Page.IsValid)
      //{
      if (Session["action"] != null && !string.IsNullOrEmpty(Session["action"].ToString().Trim()))
      {
        string action = Session["action"].ToString().Trim();

        MovieStore.business.Member member = new MovieStore.business.Member
        {
          Id = Convert.ToInt32(Session["memberId"].ToString().Trim()),
          Address = new business.Address
          {
            AddressLine1 = this.tboxAddress1.Text.Trim(),
            AddressLine2 = this.tboxAddress2.Text.Trim(),
            City = this.tboxCity.Text.Trim(),
            State = this.ddlStates.SelectedValue.Trim(),
            ZipCode = this.tboxZipCode.Text.Trim()
          }
        };

        if (action.Equals("add", StringComparison.OrdinalIgnoreCase))
        {
          member.Address.IsPrimary = true;

          await member.addAddress();

          this.pnlAddEditAddress.Visible = false;

          ClearData();
          this.pnlAddEditAddress.Visible = false;

        }
        else if (action.Equals("edit", StringComparison.OrdinalIgnoreCase))
        {
          member.Address.Id = Convert.ToInt32(Session["addressId"].ToString().Trim());

          await member.updateAddress();

          ClearData();

          this.pnlAddEditAddress.Visible = false;
        }
      }
      this.pnlAddEditAddress.Visible = false;

      this.Page.RegisterAsyncTask(new PageAsyncTask(LoadData));

      //}

    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
      Response.Redirect("Members.aspx", false);
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
        Address = new business.Address
        {
          Id = Convert.ToInt32(Session["addressId"].ToString().Trim())
        }
      };
      await member.deleteAddress();
      await LoadData();
      //ScriptManager.RegisterStartupScript(this.upnlDeleteModal, this.upnlDeleteModal.GetType(), "hide",
      //  "$(function () { $('" + this.pnlDeleteModal.ClientID + "').modal('show'); });", true);

      //this.upnlDeleteModal.Update();
    }
    protected void lvAddresses_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
      if (e.CommandName.ToLower().Trim().Equals("editaddress"))
      {
        this.pnlAddEditAddress.Visible = true;
        this.tboxAddress1.Focus();

        Session.Add("addressId", e.CommandArgument.ToString().Trim());
        Session.Add("action", "edit");

        this.Page.RegisterAsyncTask(new PageAsyncTask(LoadAddress));

      }
      else if (e.CommandName.ToLower().Trim().Equals("deleteaddress"))
      {
        Session.Add("addressId", e.CommandArgument.ToString().Trim());
        this.Page.RegisterAsyncTask(new PageAsyncTask(DeleteData));
        this.pnlAddEditAddress.Visible = false;

        this.Page.RegisterAsyncTask(new PageAsyncTask(LoadData));

        //Session.Add("addressId", e.CommandArgument.ToString().Trim());
        //ScriptManager.RegisterStartupScript(this.upnlDeleteModal, this.upnlDeleteModal.GetType(), "show",
        //  "$(function () { $('" + this.pnlDeleteModal.ClientID + "').modal('show'); });", true);
        //this.upnlDeleteModal.Update();

      }
      else if (e.CommandName.ToLower().Trim().Equals("setasprimaryaddress"))
      {
        Session.Add("addressId", e.CommandArgument.ToString().Trim());

        this.Page.RegisterAsyncTask(new PageAsyncTask(SetAddressAsPrimary));

        this.Page.RegisterAsyncTask(new PageAsyncTask(LoadData));
      }
      //else if (e.CommandName.ToLower().Trim().Equals("addaddress"))
      //{
      //  Session.Add("addressId", e.CommandArgument.ToString().Trim());

      //  this.Page.RegisterAsyncTask(new PageAsyncTask(SetAddressAsPrimary));

      //  this.Page.RegisterAsyncTask(new PageAsyncTask(LoadData));
      //}

    }
    private async Task LoadAddress()
    {
      MovieStore.business.Member member = new MovieStore.business.Member
      {
        Address = new business.Address
        {
          Id = Convert.ToInt32(Session["addressId"].ToString().Trim())
        }
      };
      var address = await member.getAddress();
      if (address != null)
      {
        this.tboxAddress1.Text = address.AddressLine1;
        this.tboxAddress2.Text = address.AddressLine2;
        this.tboxZipCode.Text = address.ZipCode;
        this.tboxCity.Text = address.City;
        this.ddlStates.SelectedValue = address.State;
      }

    }
    private async Task SetAddressAsPrimary()
    {
      MovieStore.business.Member member = new MovieStore.business.Member
      {
        Id = Convert.ToInt32(Session["memberId"].ToString().Trim()),
        Address = new business.Address
        {
          Id = Convert.ToInt32(Session["addressId"].ToString().Trim())
        }
      };
      await member.setAddressAsPrimary();

    }
  }
}