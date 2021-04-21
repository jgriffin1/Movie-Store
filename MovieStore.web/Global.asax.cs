using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

      HttpException httpException = (HttpException)ex;

      int httpCode = httpException.GetHttpCode();

      switch (httpCode)
      {
        case (404): 
          Response.Redirect("Error404.aspx");
          break;
        case (403):
          Response.Redirect("Error403.aspx");
          break;
        default:
          Response.Redirect("Error.aspx");
          break;
      }

      //Session.Add("errorMessages", ex != null ? ex.ToString() : "");



    }
  }
}