using System.Windows.Input;
using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.Saving;

public class SaveViewSamplesViewModel : ViewModel
{
    private bool m_isChecked;
    private bool m_isProgressing;

    public SaveViewSamplesViewModel()
    {
        SaveCommand = new Command(async () =>
        {
            IsProgressing = true;
            await Task.Delay(1500);
            IsProgressing = false;
            IsChecked = !IsChecked;
        });
        CompletedCommand = new Command(() => Shell.Current.Navigation.PopAsync());
    }

    public bool IsChecked
    {
        get => m_isChecked; 
        private set => RaiseWhenSet(ref m_isChecked, value);
    }

    public bool IsProgressing
    {
        get => m_isProgressing;
        set => RaiseWhenSet(ref m_isProgressing, value);
    }

    public ICommand SaveCommand { get; }
    public ICommand CompletedCommand { get; }
}