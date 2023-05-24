using DIPS.Mobile.UI.Components.ContextMenus;
using DIPS.Mobile.UI.Extensions;

namespace DIPS.Mobile.UI.Components.Pickers.ItemPicker
{
    public partial class ItemPicker
    {
        private static void UpdateContextMenuItems(ItemPicker itemPicker)
        {
            if (itemPicker.m_contextMenu == null || itemPicker.SelectedItem == null ||
                itemPicker.m_contextMenu.ItemsSource!.FirstOrDefault() is not ContextMenuGroup contextMenuGroup)
            {
                return;
            }

            var contextMenuItem = contextMenuGroup.ItemsSource?.FirstOrDefault(i =>
                i.Title != null && i.Title.Equals(itemPicker.SelectedItem.GetPropertyValue(itemPicker.ItemDisplayProperty),
                    StringComparison.InvariantCultureIgnoreCase));
            if (contextMenuItem != null)
            {
                contextMenuItem.IsChecked = true;
            }
        }

        private void AddContextMenuItems()
        {
            if (ItemsSource == null)
            {
                return;
            }

            var group = new ContextMenuGroup() {IsCheckable = true};
            foreach (var obj in ItemsSource)
            {
                var itemDisplayName = obj.GetPropertyValue(ItemDisplayProperty);
                group.ItemsSource?.Add(new ContextMenuItem()
                {
                    Title = itemDisplayName, IsChecked = SelectedItem == obj
                });
            }

            m_contextMenu.ItemsSource!.Clear();
            m_contextMenu.ItemsSource.Add(group);
        }

        private void SetSelectedItemBasedOnContextMenuItem(ContextMenuItem item)
        {
            SelectedItem = GetItemFromDisplayProperty(item.Title!);
        }
    }
}