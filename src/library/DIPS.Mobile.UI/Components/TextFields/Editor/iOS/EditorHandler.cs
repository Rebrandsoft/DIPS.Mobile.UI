using Microsoft.Maui.Platform;
using UIKit;

namespace DIPS.Mobile.UI.Components.TextFields.Editor;

public partial class EditorHandler
{
    protected override void ConnectHandler(MauiTextView platformView)
    {
        base.ConnectHandler(platformView);

        platformView.VerticalTextAlignment = TextAlignment.Start;
        platformView.Started += OnFocus;
    }

    private static partial void MapShouldUseDefaultPadding(EditorHandler handler, Editor editor)
    {
        if(editor.ShouldUseDefaultPadding)
            return;

        handler.PlatformView.TextContainer.LineFragmentPadding = 0;
        handler.PlatformView.TextContainerInset = UIEdgeInsets.Zero;
    }


    private async void OnFocus(object? sender, EventArgs e)
    {
        if(!((VirtualView as Editor)!).ShouldSelectAllTextOnFocused)
            return;
        
        await Task.Delay(1);
        PlatformView.SelectAll(null);  
    }

    private static partial void MapShouldSelectTextOnTapped(EditorHandler handler, Editor entry)
    {
    }

    private static partial void MapHasBorder(EditorHandler handler, Editor entry)
    {
    }

    protected override void DisconnectHandler(MauiTextView platformView)
    {
        base.DisconnectHandler(platformView);
        
        platformView.Started -= OnFocus;
    }

}