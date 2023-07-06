using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.Saving.SaveView;

public partial class SaveView
{
    /// <summary>
    /// The action to be executed when <see cref="SaveView"/> started saving
    /// </summary>
    public event Action? OnSavingStarted;
    
    /// <summary>
    /// Determines whether the <see cref="SaveView"/> is currently saving or not
    /// </summary>
    public bool IsSaving
    {
        get => (bool)GetValue(IsSavingProperty);
        set => SetValue(IsSavingProperty, value);
    }

    /// <summary>
    /// The text to be displayed when <see cref="IsSaving"/> is true
    /// </summary>
    public string SavingText
    {
        get => (string)GetValue(SavingTextProperty);
        set => SetValue(SavingTextProperty, value);
    }

    /// <summary>
    /// Determines whether the <see cref="SaveView"/> has completed saving or not
    /// </summary>
    public bool IsSavingCompleted
    {
        get => (bool)GetValue(IsSavingCompletedProperty);
        set => SetValue(IsSavingCompletedProperty, value);
    }

    /// <summary>
    /// The text to be displayed when <see cref="IsSaving"/> is true
    /// </summary>
    public string SavingCompletedText
    {
        get => (string)GetValue(SavingCompletedTextProperty);
        set => SetValue(SavingCompletedTextProperty, value);
    }

    /// <summary>
    /// The command to be executed when <see cref="IsSavingCompleted"/> is true and all animations are finished
    /// </summary>
    public ICommand SavingCompletedCommand
    {
        get => (ICommand)GetValue(SavingCompletedCommandProperty);
        set => SetValue(SavingCompletedCommandProperty, value);
    }
    
    public static readonly BindableProperty IsSavingProperty = BindableProperty.Create(
        nameof(IsSaving),
        typeof(bool),
        typeof(SaveView),
        false,
        propertyChanged: OnIsSavingChanged);

    public static readonly BindableProperty SavingTextProperty = BindableProperty.Create(
        nameof(SavingText),
        typeof(string),
        typeof(SaveView));

    public static readonly BindableProperty IsSavingCompletedProperty = BindableProperty.Create(
        nameof(IsSavingCompleted),
        typeof(bool),
        typeof(SaveView),
        false,
        propertyChanged: OnIsSavingCompletedChanged);

    public static readonly BindableProperty SavingCompletedTextProperty = BindableProperty.Create(
        nameof(SavingCompletedText),
        typeof(string),
        typeof(SaveView));

    public static readonly BindableProperty SavingCompletedCommandProperty = BindableProperty.Create(
        nameof(SavingCompletedCommand),
        typeof(ICommand), 
        typeof(SaveView));
}