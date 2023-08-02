﻿using DIPS.Mobile.UI.Resources.Sizes;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Lists;

public partial class CollectionView : Microsoft.Maui.Controls.CollectionView
{
    private readonly Border m_extraSpaceBorder;

    public CollectionView()
    {
        BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent;
        //Adds a extra space in the bottom to make sure the last item is not placed at the very bottom of the page, this makes the last item more accessible for people.
        m_extraSpaceBorder ??= new Border() {BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent};
        Footer = m_extraSpaceBorder;
        SelectionMode = SelectionMode.None;
    }

    private void TrySetItemSpacing()
    {
        var oldItemsLayout = ItemsLayout;
        ItemsLayout = oldItemsLayout switch
        {
            LinearItemsLayout linearItemsLayout => new LinearItemsLayout(linearItemsLayout.Orientation)
            {
                ItemSpacing = ItemSpacing
            },
            GridItemsLayout gridItemsLayout => new GridItemsLayout(gridItemsLayout.Span, gridItemsLayout.Orientation)
            {
                HorizontalItemSpacing = ItemSpacing, VerticalItemSpacing = ItemSpacing,
            },
            _ => null
        };
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

#if __IOS__
        //TODO: Remove .NET 8 when this is fixed: https://github.com/dotnet/maui/issues/7315
        if (Parent is ScrollView scrollView) //To fix an issue where the width of the collectionview has the wrong width on iOS.
        {
            WidthRequest = scrollView.Bounds.Width;
        }
#endif

        if (HasAdditionalSpaceAtTheEnd && Footer == m_extraSpaceBorder)
        {
            AddExtraSpaceAtTheEnd(height);
        }
    }

    private void AddExtraSpaceAtTheEnd(double height)
    {
#if __IOS__ //Collectionviews height is bigger than the visual height of the collection view
        var visibleSize = height - Bounds.Y;
        var nonVisibleSize = height - visibleSize;
        m_extraSpaceBorder.HeightRequest =
            nonVisibleSize +
            ((visibleSize) /
             3); //The border has to be as big as the non visible size + one third of the visible size
#elif __ANDROID__
        m_extraSpaceBorder.HeightRequest = height/3; //The border has to be the one third of the visible size
#endif
    }
}