using Android.Views;
using AndroidX.RecyclerView.Widget;
using Microsoft.Maui.Controls.Handlers.Items;

namespace DIPS.Mobile.UI.Components.Lists;

public partial class CollectionViewHandler : Microsoft.Maui.Controls.Handlers.Items.CollectionViewHandler
{
    protected override RecyclerView CreatePlatformView()
    {
        return new MauiRecyclerView(Context, GetItemsLayout, CreateAdapter);
    }
    
    private static partial void MapShouldBounce(CollectionViewHandler handler,
        Microsoft.Maui.Controls.CollectionView virtualView)
    {
        if (virtualView is CollectionView collectionView)
        {
            handler.PlatformView.OverScrollMode = collectionView.ShouldBounce ? OverScrollMode.Always : OverScrollMode.Never;
        }
 
    }
}