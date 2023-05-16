namespace DIPS.Mobile.UI.Components.Images;

public partial class NativeIconHandler
{
    public NativeIconHandler() : base(PropertyMapper)
    {
        AppendPropertyMapper();
    }

    private partial void AppendPropertyMapper();

    public static IPropertyMapper<NativeIcon, NativeIconHandler> PropertyMapper = new PropertyMapper<NativeIcon, NativeIconHandler>(ViewMapper)
    {
        [nameof(NativeIcon.Color)] = TrySetImageColor
    };

    private static partial void TrySetSystemImage(NativeIconHandler nativeIconHandler, NativeIcon nativeIcon);

    private static partial void TrySetImageColor(NativeIconHandler nativeIconHandler, NativeIcon nativeIcon);

}