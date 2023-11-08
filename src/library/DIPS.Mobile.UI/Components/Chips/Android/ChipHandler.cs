using Android.Content.Res;
using Android.Runtime;
using Android.Text;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.Chips.Android;
using Java.Interop;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using TextAlignment = Android.Views.TextAlignment;


// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Chips;

public partial class ChipHandler : ViewHandler<Chip, Google.Android.Material.Chip.Chip>
{
    protected override Google.Android.Material.Chip.Chip CreatePlatformView() => new Google.Android.Material.Chip.Chip(Context);

    protected override void ConnectHandler(Google.Android.Material.Chip.Chip platformView)
    {
        base.ConnectHandler(platformView);
        platformView.SetPadding(8, 2, 8, 2);
        platformView.TextAlignment = (TextAlignment) Microsoft.Maui.TextAlignment.Center;
        platformView.SetTextColor(Colors.GetColor(ColorName.color_system_black).ToPlatform());
        platformView.TextSize = Sizes.GetSize(SizeName.size_4);
        platformView.ChipCornerRadius = 24;
        platformView.SetEnsureMinTouchTargetSize(false); //Remove extra margins around the chip, this is added to get more space to hit the chip but its not necessary : https://stackoverflow.com/a/57188310
        platformView.Click += OnChipTapped;
    }

    private void OnChipTapped(object? sender, EventArgs e)
    {
        OnChipTapped();
    }

    protected override void DisconnectHandler(Google.Android.Material.Chip.Chip platformView)
    {
        base.DisconnectHandler(platformView);
        platformView.Click -= OnChipTapped;
    }

    private static partial void MapTitle(ChipHandler handler, Chip chip)
    {
        handler.PlatformView.Text = chip.Title;
        handler.PlatformView.Ellipsize = TextUtils.TruncateAt.End;
    }

    private static partial void MapHasCloseButton(ChipHandler handler, Chip chip)
    {
        if (handler.VirtualView.HasCloseButton)
        {
            handler.PlatformView.CloseIconVisible = true;
            handler.PlatformView.SetOnCloseIconClickListener(new ChipCloseListener(handler));
            DUI.TryGetResourceId(Icons.GetIconName(handler.CloseIconName), out var id, defType:"drawable");
            if (id != 0)
            {
                var drawable = Platform.AppContext.Resources?.GetDrawable(id);
                handler.PlatformView.CloseIcon = drawable;
            }
        }
        else
        {
            handler.PlatformView.CloseIconVisible = false;
            handler.PlatformView.SetOnCloseIconClickListener(null);
        }
        
    }

    private static partial void MapColor(ChipHandler handler, Chip chip)
    {
        if (handler.VirtualView.Color == null) return;
        handler.PlatformView.ChipBackgroundColor = new ColorStateList(CreateColorStates(handler.VirtualView.Color, out var colors), colors);
    }

    private static int[][] CreateColorStates(Color color, out int[] colors)
    {
        var states = new[]
        {
            new[] {global::Android.Resource.Attribute.StateEnabled}, // enabled
            new[] {-global::Android.Resource.Attribute.StateEnabled}, // disabled
            new[] {-global::Android.Resource.Attribute.StateChecked}, // unchecked
            new[] {global::Android.Resource.Attribute.StateChecked}, // checked
            new[] {global::Android.Resource.Attribute.StateChecked} // pressed
        };

        colors = new int[]
        {
            color.ToPlatform(),
            color.ToPlatform(),
            color.ToPlatform(),
            color.ToPlatform(),
            color.ToPlatform()
        };
        return states;
    }

    private static partial void MapCloseButtonColor(ChipHandler handler, Chip chip)
    {
        if (handler.VirtualView.CloseButtonColor == null) return;
        handler.PlatformView.CloseIcon?.SetTint(handler.VirtualView.CloseButtonColor.ToPlatform());

    }

    private static partial void MapCornerRadius(ChipHandler handler, Chip chip)
    {
        handler.PlatformView.ChipCornerRadius = (float) (handler.VirtualView.CornerRadius*DeviceDisplay.MainDisplayInfo.Density);
    }

    private static partial void MapBorderColor(ChipHandler handler, Chip chip)
    {
        if (chip.BorderColor == null) return;

        handler.PlatformView.ChipStrokeColor =
            new ColorStateList(CreateColorStates(chip.BorderColor, out var colors), colors);
    }

    private static partial void MapBorderWidth(ChipHandler handler, Chip chip)
    {
        handler.PlatformView.ChipStrokeWidth = (float) chip.BorderWidth;
    }
    
    private static partial void MapTitleColor(ChipHandler handler, Chip chip)
    {
        if (chip.TitleColor is null) return;
        handler.PlatformView.SetTextColor(chip.TitleColor.ToPlatform());
        handler.PlatformView.CheckedIconTint = new ColorStateList(CreateColorStates(handler.VirtualView.TitleColor, out var colors), colors);
    }

    private static partial void MapIsToggleable(ChipHandler handler, Chip chip)
    {
        if (!handler.VirtualView.IsToggleable)
            return;
        
        handler.PlatformView.Checkable = true;
        DUI.TryGetResourceId(Icons.GetIconName(handler.ToggledIconName), out var id, defType:"drawable");
        if (id is not 0)
        {
            var drawable = Platform.AppContext.Resources?.GetDrawable(id);
            handler.PlatformView.CheckedIcon = drawable;
        }
        handler.PlatformView.SetOnCheckedChangeListener(new OnToggledChangedListener(handler));
    }
    
    private static partial void MapIsToggled(ChipHandler handler, Chip chip)
    {
        //Make sure not to mess with close button
        if (handler.VirtualView.HasCloseButton)
            return;

        if (!handler.VirtualView.IsToggleable)
            return;
        
        if (handler.VirtualView.IsToggled)
        {
            handler.PlatformView.Checked = true;
            handler.PlatformView.CheckedIconVisible = true;
        }
        else
        {
            handler.PlatformView.Checked = false;
            handler.PlatformView.CheckedIconVisible = false;
        }
        
    }
}