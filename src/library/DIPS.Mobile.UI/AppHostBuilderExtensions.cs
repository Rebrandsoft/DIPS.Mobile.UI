using DIPS.Mobile.UI.Components.ContextMenus;
using DIPS.Mobile.UI.Components.Images;
using DIPS.Mobile.UI.Components.Searching;

#if __ANDROID__
using DIPS.Mobile.UI.Components.Buttons.Android;
using DIPS.Mobile.UI.Components.Searching.Android;
#elif __IOS__
using DIPS.Mobile.UI.Components.Searching.iOS;
#endif

using Microsoft.Maui.LifecycleEvents;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;
using ContextMenuPlatformEffect = DIPS.Mobile.UI.Components.ContextMenus.ContextMenuPlatformEffect;
using Image = DIPS.Mobile.UI.Components.Images.Image;

namespace DIPS.Mobile.UI;

public static class AppHostBuilderExtensions
{
    // ReSharper disable once IdentifierTypo
    // ReSharper disable once InconsistentNaming
    public static MauiAppBuilder UseDIPSUI(
        this MauiAppBuilder builder)
    {
        //Initializers
        DUI.Init();
#if __ANDROID__
        builder.ConfigureLifecycleEvents(events =>
        {
            events.AddAndroid(android => android
                .OnCreate((activity, _) =>  API.Library.Android.DUI.Init(activity)));
            events.AddAndroid(android => android.OnPause(activity => _ = DUI.RemoveViewsLocatedOnTopOfPage()));
        });
#elif __IOS__
        builder.ConfigureLifecycleEvents(events =>
        {
            events.AddiOS(ios => ios.OnResignActivation(application => _ = DUI.RemoveViewsLocatedOnTopOfPage()));
        });
#endif
        
        //Handlers
        builder.ConfigureMauiHandlers(handlers =>
        {
            handlers.AddHandler(typeof(Image), typeof(ImageHandler));
#if __ANDROID__
            handlers.AddHandler(typeof(InternalSearchBar), typeof(InternalSearchBarHandler));
            handlers.AddHandler(typeof(IndeterminateProgressBar), typeof(IndeterminateProgressBarHandler));
            handlers.AddHandler(typeof(Button), typeof(ButtonHandler));
#elif __IOS__
            handlers.AddHandler(typeof(InternalSearchBar), typeof(InternalSearchBarHandler));
#endif
        });

        builder.ConfigureEffects(effects =>
        {
            effects.Add(typeof(ContextMenuEffect), typeof(ContextMenuPlatformEffect));
        });

        return builder;
    }
}