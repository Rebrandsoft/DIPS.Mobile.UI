using Android.OS;
using Android.Views;
using DIPS.Mobile.UI.API.Library;
using Microsoft.Maui.Platform;
using Fragment = Android.App.Fragment;
using View = Android.Views.View;

namespace DIPS.Mobile.UI.Components.SystemMessage;

internal class SystemMessageFragment : Fragment
{
    private readonly SystemMessage m_systemMessage;

    public SystemMessageFragment(SystemMessage systemMessage)
    {
        m_systemMessage = systemMessage;
    }
    
    public override View OnCreateView(LayoutInflater inflater, ViewGroup? container, Bundle? savedInstanceState)
    {
        return m_systemMessage.ToPlatform(DUI.GetCurrentMauiContext!);
    }
}