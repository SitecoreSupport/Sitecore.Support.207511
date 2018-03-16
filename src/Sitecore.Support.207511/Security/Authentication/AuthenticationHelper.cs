using System;
using System.Web;
using Sitecore.Diagnostics;
using Sitecore.Security.Accounts;
using Sitecore.Security.Authentication;

namespace Sitecore.Support.Security.Authentication
{
  public class AuthenticationHelper : Sitecore.Security.Authentication.AuthenticationHelper
  {
    public AuthenticationHelper(AuthenticationProvider provider) : base(provider)
    {

    }

    public override void SetActiveUser(User user)
    {
      Assert.ArgumentNotNull(user, "user");
      HttpContext current = HttpContext.Current;
      if (current != null)
      {
        try
        {
          current.User = user;
        }
        catch (NullReferenceException nre)
        {
          Log.Error("Sitecore.Support: failed to set HttpContext.Current.User. Most likely the code is executed on a separate thread and does not have associated HttpContext.", nre, this);
        }

      }
      System.Threading.Thread.CurrentPrincipal = user;
    }
  }
}