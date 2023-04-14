using DIPS.Mobile.UI.Components.ContextMenus;
using UIKit;

namespace DIPS.Mobile.UI.Helpers.ContextMenus.iOS;

 internal static class ContextMenuHelper
    {
        internal static Dictionary<ContextMenuItem, UIMenuElement> CreateMenuItems(
            IEnumerable<ContextMenuItem> contextMenuItems,
            ContextMenu contextMenu, ContextMenuGroup menuGroup = null)
        {
            var dict = new Dictionary<ContextMenuItem, UIMenuElement>();
            var items = contextMenuItems.ToArray();
            foreach (var contextMenuItem in items)
            {
                UIMenuElement uiMenuElement;
                if (contextMenuItem is ContextMenuGroup contextMenuGroup) //Recursively add menu items from a group
                {
                    contextMenuGroup.Parent = contextMenu;
                    //Inherit isCheckable context menu group group to all menu items in the group
                    foreach (var c in contextMenuGroup.ItemsSource)
                    {
                        c.IsCheckable = contextMenuGroup.IsCheckable;
                    }

                    var newDict = CreateMenuItems(contextMenuGroup.ItemsSource, contextMenu, contextMenuGroup);
                    if (items.Count(i => i is ContextMenuGroup) >
                        1) //If there is more than one group, add the group title and group the items
                    {
                        uiMenuElement = UIMenu.Create(contextMenuGroup.Title, newDict.Select(k => k.Value).ToArray());
                    }
                    else //Only one group, add this to the root of the menu so the user does not have to tap an extra time to get to the items.
                    {
                        foreach (var newD in newDict)
                        {
                            dict.Add(newD.Key, newD.Value);
                        }
                        continue;
                    }
                }

                else
                {
                    UIImage image = null;

                    if (!string.IsNullOrEmpty(contextMenuItem.Icon))
                    {
                        image = UIImage.FromBundle(contextMenuItem.Icon.File);
                    }
                    
                    if(!string.IsNullOrEmpty(contextMenuItem.iOSOptions.SystemIconName)) //Override image with SF Symbols if this is what the consumer wants
                    {

                        var systemImage = UIImage.GetSystemImage(contextMenuItem.iOSOptions.SystemIconName);
                        image = systemImage ?? image;
                    }
                    
                    var uiAction = UIAction.Create(contextMenuItem.Title, image, null,
                        uiAction => OnMenuItemClick(uiAction, contextMenuItem, contextMenu));

                    if (contextMenuItem.IsChecked)
                    {
                        contextMenu.ResetIsCheckedForTheRest(contextMenuItem);
                    }
                    
                    SetCorrectUiActionState(contextMenuItem, uiAction); //Setting the correct check mark if it can

                    uiMenuElement = uiAction;

                    if (menuGroup != null)
                    {
                        contextMenuItem.Parent = menuGroup;
                    }
                    else
                    {
                        contextMenuItem.Parent = contextMenu;
                    }
                }

                dict.Add(contextMenuItem, uiMenuElement);
            }

            return dict;
        }

        private static void OnMenuItemClick(UIAction action, ContextMenuItem contextMenuItem,
            ContextMenu contextMenu)
        {
            
            if (contextMenuItem.IsCheckable)
            {
                if (contextMenuItem.Parent is not ContextMenuGroup || !contextMenuItem.IsChecked)
                { //Only check if if unchecked if its a part of a group
                    contextMenu.ResetIsCheckedForTheRest(contextMenuItem);

                    contextMenuItem.IsChecked =
                        !contextMenuItem
                            .IsChecked; //Can not change the visuals when the menu is showing as the items are immutable when they are showing    
                }
            }

            contextMenuItem.SendClicked(contextMenu);
        }

        private static void SetCorrectUiActionState(ContextMenuItem contextMenuItem, UIAction uiAction)
        {
            if (contextMenuItem.IsCheckable)
            {
                uiAction.State = contextMenuItem.IsChecked ? UIMenuElementState.On : UIMenuElementState.Off;
            }
        }
    }