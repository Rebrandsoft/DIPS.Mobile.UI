using DIPS.Mobile.UI.API.Library;
using Microsoft.Maui.Platform;
using UIKit;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.BottomSheets.iOS;

public class BottomSheetViewController : UIViewController
{
    private readonly BottomSheetContainer m_container;
    private BottomBarView? m_bottomBar;

    public BottomSheetViewController(BottomSheet bottomSheet)
    {
        BottomSheet = bottomSheet;
        
        bottomSheet.ViewController = this;

        m_container = new BottomSheetContainer(bottomSheet);
    }
    
    public BottomSheet BottomSheet { get; }
    
    public async void ModifySearchbar(bool add)
    {
        // Delay to make sure the container is created
        await Task.Delay(1);
        
        m_container.ModifySearchbar(add);
    }
    
    public async void ModifyBottomBar(bool add)
    {
        // Delay to make sure the bottom bar view is created
        await Task.Delay(1);
        
        if (add)
        {
            m_bottomBar?.Show();
        }
        else
        {
            m_bottomBar?.Remove();
        }
    }

    public override void ViewDidLoad()
    {
        base.ViewDidLoad();

        if(View is null)
            return;
        
        m_container.AddToView(View, NavigationController?.NavigationBar);
        m_bottomBar = new BottomBarView(View, BottomSheet);
    }

    private UIBarButtonItem ToBarButtonItem(ToolbarItem toolbarItem)
    {
        toolbarItem.BindingContext = BottomSheet.BindingContext;
        if (toolbarItem.IconImageSource is FileImageSource fileImageSource)
        {
            return new UIBarButtonItem(UIImage.FromBundle(fileImageSource), UIBarButtonItemStyle.Plain, delegate 
            {
                toolbarItem.Command?.Execute(toolbarItem.CommandParameter);
            });
        }
        
        return new UIBarButtonItem(toolbarItem.Text, UIBarButtonItemStyle.Plain, delegate
        {
            toolbarItem.Command?.Execute(toolbarItem.CommandParameter);
        });
    }

    public void Opened()
    {
        BottomSheet.SendOpen();
    }

    internal static Positioning GetCurrentPosition(UISheetPresentationController sheetPresentationController)
    {
        return sheetPresentationController.SelectedDetentIdentifier switch
        {
            UISheetPresentationControllerDetentIdentifier.Unknown => Positioning.Fit,
            UISheetPresentationControllerDetentIdentifier.Medium => Positioning.Medium,
            UISheetPresentationControllerDetentIdentifier.Large => Positioning.Large,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public void SetPositioning()
    {
        this.SetPositioning(BottomSheet, m_container);
    }

    public void SetTitle()
    {
        NavigationItem.Title = BottomSheet.Title;
    }

    public void AddToolbarItems()
    {
        NavigationItem.RightBarButtonItems = BottomSheet.ToolbarItems.Select(ToBarButtonItem).ToArray();
    }
    
    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        BottomSheet.SendClose();
        BottomSheetService.RemoveFromStack(BottomSheet);
        BottomSheet.Handler?.DisconnectHandler();
    }
}