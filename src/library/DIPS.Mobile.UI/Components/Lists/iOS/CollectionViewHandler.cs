using Microsoft.Maui.Platform;
using UIKit;

namespace DIPS.Mobile.UI.Components.Lists;

public partial class CollectionViewHandler : Microsoft.Maui.Controls.Handlers.Items.CollectionViewHandler
{
    private static partial void MapShouldBounce(CollectionViewHandler handler,
        Microsoft.Maui.Controls.CollectionView virtualView)
    {
        if (handler.PlatformView.Subviews[0] is UICollectionView uiCollectionView)
        {
            if (virtualView is CollectionView collectionView)
            {
                uiCollectionView.Bounces = collectionView.ShouldBounce;
            }
        }
    }
}