using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace MovieStore.web
{
  public class Global : System.Web.HttpApplication
  {
    protected void Application_Start(object sender, EventArgs e)
    {
    }
    protected void Application_Error(object sender, EventArgs e)
    {
      Exception ex = Server.GetLastError();
    
        //Session.Add("errorMessages", ex != null ? ex.ToString() : "");

     
      
    }
  }
}