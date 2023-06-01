﻿using System.Collections.ObjectModel;
using Components.ComponentsSamples.ContextMenus;
using Components.Resources.LocalizedStrings;
using Components.Services;
using DIPS.Mobile.UI.Components.FloatingActionButtons.ExtendedFloatingActionButton;
using DIPS.Mobile.UI.Components.FloatingActionButtons.FloatingNavigationButton;

namespace Components;

public partial class App : Application
{
    private readonly AppCenterService m_appCenterService;

    public App()
    {
        InitializeComponent();

        var shell = new Shell();
        var tabBar = new TabBar();
        var tab = new Tab();
        
        tab.Items.Add(new ShellContent()
        {
            ContentTemplate =
                new DataTemplate(() => new MainPage(new List<SampleType> {SampleType.Resources, SampleType.Components}.OrderBy(s => s.ToString()),
                    REGISTER_YOUR_SAMPLES_HERE.RegisterSamples()))
        });
        tabBar.Items.Add(tab);
        shell.Items.Add(tabBar);
        MainPage = shell;

        m_appCenterService = new AppCenterService();
    }

    protected override void OnStart()
    {
        _ = TryGetLatestVersion();

        FloatingNavigationButtonService.AddFloatingNavigationButton(config =>
        {
            config.AddNavigationButton("Hei", new Command(() => { }));
            config.AddPageThatHidesButton(typeof(ContextMenuSamples));
        });
        
        base.OnStart();
    }

    private async Task<bool> TryGetLatestVersion()
    {
#if DEBUG
        return true;
#endif
        
        var release = await m_appCenterService.GetLatestVersion();
        if (release != null)
        {
            var latestVersion = new Version(release.Version);
            var currentVersion = AppInfo.Version;
            if (currentVersion >= latestVersion)
            {
                return false;
            }

            if (Current?.MainPage == null) return true;
            var wantToDownload = await Current.MainPage.DisplayAlert(LocalizedStrings.New_version,
                LocalizedStrings.New_version_message, LocalizedStrings.Download, LocalizedStrings.Cancel);
            if (!wantToDownload)
            {
                return false;
            }

            await Launcher.OpenAsync(release.InstallUri);
            return true;
        }

        return false;
    }

    protected override void OnResume()
    {
        _ = TryGetLatestVersion();
        base.OnResume();
    }
}