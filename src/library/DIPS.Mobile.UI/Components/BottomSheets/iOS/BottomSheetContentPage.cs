using DIPS.Mobile.UI.Components.BottomSheets;
using Microsoft.Maui.Platform;
using UIKit;

namespace DIPS.Mobile.UI.Components.BottomSheets.iOS;

internal class BottomSheetContentPage : ContentPage
{
    private UIViewController? m_viewController;
    private readonly BottomSheet m_bottomSheet;
    private UISheetPresentationController? m_sheetPresentationController;

    public BottomSheetContentPage(BottomSheet bottomSheet)
    {
        m_bottomSheet = bottomSheet;
        bottomSheet.VerticalOptions = LayoutOptions.Start;

        SetupViewController();
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        m_bottomSheet.WillClose += Close;
    }

    private void UnSubscribeEvents()
    {
        m_bottomSheet.WillClose -= Close;
    }

    private void Close(object sender, EventArgs e)
    {
        m_viewController?.DismissViewController(true, null);
    }

    private void SetupViewController()
    {
        var mauiContext = Application.Current?.MainPage?.Window.Handler.MauiContext;
        if (mauiContext == null)
        {
            return;
        }
        
        m_viewController = this.ToUIViewController(mauiContext);
        var view = new BottomSheetViewContainer(m_bottomSheet, mauiContext);
        if (view.BackgroundColor == null)
        {
            view.BackgroundColor = UIColor.SystemBackground;
        }

        if (m_viewController == null) return;

        m_viewController.RestorationIdentifier = BottomSheetService.BottomSheetRestorationIdentifier;
        m_viewController.ModalPresentationStyle = UIModalPresentationStyle.PageSheet;

        if (OperatingSystem.IsIOSVersionAtLeast(15)) //Can use bottom sheet
        {
            if (m_viewController.SheetPresentationController == null) return;
            if (m_viewController.SheetPresentationController != null)
            {
                m_sheetPresentationController = m_viewController.SheetPresentationController;


                var prefferedDetent = UISheetPresentationControllerDetent.CreateMediumDetent();
                var prefferedDetentIdentifier = UISheetPresentationControllerDetentIdentifier.Medium;

                if (m_bottomSheet.ShouldFitToContent)
                {
                    if (OperatingSystem.IsIOSVersionAtLeast(16)) //Can fit to content by setting a custom detent
                    {
                        prefferedDetent = UISheetPresentationControllerDetent.Create("prefferedDetent",
                            _ =>
                            {
                                return (nfloat)(m_bottomSheet.Bounds.Height + Padding.Top + Padding.Bottom);
                            });
                        prefferedDetentIdentifier = UISheetPresentationControllerDetentIdentifier.Unknown;
                    }
                    else if
                        (OperatingSystem
                         .IsIOSVersionAtLeast(15)) //Select large detent if the content is bigger than medium detent
                    {
                        if (Content.Height >
                            Height /
                            2) //if the content is larger than half the screen when the detent is medium, it means that something is outside of bounds
                        {
                            prefferedDetent = UISheetPresentationControllerDetent.CreateLargeDetent();
                            prefferedDetentIdentifier = UISheetPresentationControllerDetentIdentifier.Large;
                        }
                    }
                }

                if (OperatingSystem.IsIOSVersionAtLeast(15))
                {
                    m_sheetPresentationController.Detents = (m_bottomSheet.ShouldFitToContent)
                        ? new[] {prefferedDetent}
                        : new[] {prefferedDetent, UISheetPresentationControllerDetent.CreateLargeDetent(),};

                    //Add grabber
                    m_sheetPresentationController.PrefersGrabberVisible = true;

                    m_sheetPresentationController.SelectedDetentIdentifier = prefferedDetentIdentifier;

                    m_sheetPresentationController.PrefersScrollingExpandsWhenScrolledToEdge = true;

                    var bottom = (UIApplication.SharedApplication.KeyWindow?.SafeAreaInsets.Bottom) == 0
                            ? DIPS.Mobile.UI.Resources.Sizes.Sizes.GetSize(Sizes.Sizes.SizeName.size_4) //There is a physical home button
                            : DIPS.Mobile.UI.Resources.Sizes.Sizes.GetSize(Sizes.Sizes.SizeName.size_1) //There is no phyiscal home button, but we need some air between the safe area and the content
                        ;
                    Padding = new Thickness(0, DIPS.Mobile.UI.Resources.Sizes.Sizes.GetSize(Sizes.Sizes.SizeName.size_4), 0,
                        bottom); //Respect grabber and make sure we add some padding to the bottom, depending on if Safe Area (non physical home button) is visible.
                }
                
                m_viewController.View = view;
            }
        }
    }

    internal async Task Open()
    {
        var currentViewController = Platform.GetCurrentUIViewController();
        if (m_viewController != null && currentViewController != null)
        {
            await currentViewController.PresentViewControllerAsync(m_viewController, true);
        }
    }

    protected override void OnDisappearing()
    {
        UnSubscribeEvents();
        base.OnDisappearing();
        m_bottomSheet.SendDidClose();
    }
}