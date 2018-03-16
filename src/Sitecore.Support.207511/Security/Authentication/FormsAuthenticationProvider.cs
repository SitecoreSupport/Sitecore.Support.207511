namespace Sitecore.Support.Security.Authentication
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
      var formsHelperFieldInfo =
        typeof(Sitecore.Security.Authentication.FormsAuthenticationProvider).GetField("helper", BindingFlags.Instance | BindingFlags.NonPublic);

      if (helperFieldInfo == null || formsHelperFieldInfo == null)
      {
        Sitecore.Diagnostics.Log.Error("Sitecore.Support: Failed to apply support patch 207511", this);
        return;
      }
      helperFieldInfo.SetValue(this, new AuthenticationHelper(this));
      Sitecore.Diagnostics.Log.Warn("Sitecore.Support: AuthenticationHelper has been overridden", this);
      formsHelperFieldInfo.SetValue(this, new FormsAuthenticationHelper(this));
      Sitecore.Diagnostics.Log.Warn("Sitecore.Support: FormsAuthenticationHelper has been overridden", this);
    }
  }
}