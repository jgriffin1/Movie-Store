﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MovieStore.Web
{
    public partial class MainMenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (Session["roleName"] != null && !string.IsNullOrEmpty(Session["roleName"].ToString().Trim()))
                {
                    if (Session["roleName"].ToString().Trim().Equals("user", StringComparison.CurrentCultureIgnoreCase))
                    {
                        this.phUserMenu.Visible = true;
                        this.phAdminMenu.Visible = false;
                    }
                    else
                    {
                        this.phUserMenu.Visible = false;
                        this.phAdminMenu.Visible = true;
                    }
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            Response.Redirect("Logout.aspx");
        }
    }
}