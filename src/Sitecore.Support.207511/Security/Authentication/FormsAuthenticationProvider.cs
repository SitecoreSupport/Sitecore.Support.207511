﻿namespace Sitecore.Support.Security.Authentication
{
  using System.Reflection;
  using Sitecore.Security.Authentication;
  public class FormsAuthenticationProvider : Sitecore.Security.Authentication.FormsAuthenticationProvider
  {
    public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
    {
      base.Initialize(name, config);
      var helperFieldInfo =
        typeof(AuthenticationProvider).GetField("helper", BindingFlags.Instance | BindingFlags.NonPublic);
      if (helperFieldInfo == null)
      {
        Sitecore.Diagnostics.Log.Error("Failed to apply support patch 207511", this);
        return;
      }
      helperFieldInfo.SetValue(this, new FormsAuthenticationHelper(this));
    }
  }
}