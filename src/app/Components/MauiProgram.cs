﻿using DIPS.Mobile.UI.API.Builder;
using DIPS.Mobile.UI.API.Library;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;

namespace Components;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseDIPSUI();
        
        builder.ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Bold.ttf", "Header");
            fonts.AddFont("OpenSans-SemiBold.ttf", "UI");
            fonts.AddFont("OpenSans-Regular.ttf", "Body");
        });
        
        //Close things when the app is going to background and back again
#if __ANDROID__
        builder.ConfigureLifecycleEvents(events =>
        {
            events.AddAndroid(android => android.OnResume(_ => DUI.RemoveViewsLocatedOnTopOfPage()));
        });

#elif __IOS__
 builder.ConfigureLifecycleEvents(events =>
        {
            events.AddiOS(ios => ios.OnResignActivation(_ => DUI.RemoveViewsLocatedOnTopOfPage()));
        });
#endif
#if DEBUG
        builder.Logging.AddDebug();
        DUI.IsDebug = true;
#endif
        return builder.Build();
    }
}

