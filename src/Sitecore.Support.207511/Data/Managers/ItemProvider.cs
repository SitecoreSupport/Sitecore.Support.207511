using Sitecore.Collections;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.SecurityModel;

namespace Sitecore.Support.Data.Managers
{
  public class ItemProvider : Sitecore.Data.Managers.ItemProvider
  {
    protected override Item ApplySecurity(Item item, SecurityCheck securityCheck)
    {
      Assert.ArgumentNotNull(item, "item");
      if (securityCheck == SecurityCheck.Disable || !item.Database.SecurityEnabled || item.Access.CanRead())
      {
        return item;
      }
      return null;
    }

    protected override ItemList ApplySecurity(ItemList children, SecurityCheck securityCheck)
    {
      Assert.ArgumentNotNull(children, "children");
      if (children.Count == 0 || securityCheck == SecurityCheck.Disable || !children[0].Database.SecurityEnabled || Context.User.IsAdministrator)
      {
        return children;
      }
      ItemList itemList = new ItemList();
      foreach (Item current in children)
      {
        if (current.Access.CanRead())
        {
          itemList.Add(current);
        }
      }
      return itemList;
    }

  }
}