using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls.Internals;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
[assembly: InternalsVisibleTo("DIPS.Mobile.UI.UnitTests")]

[assembly: Preserve]
//Add new namespaces below to make them visible when using Custom Namespace : https://learn.microsoft.com/en-us/dotnet/maui/xaml/namespaces/custom-prefix?view=net-maui-7.0
[assembly:Microsoft.Maui.Controls.XmlnsPrefix("http://dips.com/mobile.ui", "dui")]

[assembly: XmlnsDefinition("http://dips.com/mobile.ui", "DIPS.Mobile.UI.Components.Pages")]
[assembly: XmlnsDefinition("http://dips.com/mobile.ui", "DIPS.Mobile.UI.Resources.Colors")]
[assembly: XmlnsDefinition("http://dips.com/mobile.ui", "DIPS.Mobile.UI.Resources.Sizes")]
[assembly: XmlnsDefinition("http://dips.com/mobile.ui", "DIPS.Mobile.UI.Resources.Icons")]
[assembly: XmlnsDefinition("http://dips.com/mobile.ui", "DIPS.Mobile.UI.Components.Pickers")]
[assembly: XmlnsDefinition("http://dips.com/mobile.ui", "DIPS.Mobile.UI.Components.Pickers.DatePicker")]
[assembly: XmlnsDefinition("http://dips.com/mobile.ui", "DIPS.Mobile.UI.Components.Pickers.TimePicker")]
[assembly: XmlnsDefinition("http://dips.com/mobile.ui", "DIPS.Mobile.UI.Components.Pickers.DateAndTimePicker")]
[assembly: XmlnsDefinition("http://dips.com/mobile.ui", "DIPS.Mobile.UI.Components.Pickers.ItemPicker")]
[assembly: XmlnsDefinition("http://dips.com/mobile.ui", "DIPS.Mobile.UI.Components.ListItems")]
[assembly: XmlnsDefinition("http://dips.com/mobile.ui", "DIPS.Mobile.UI.Components.Chips")]
[assembly: XmlnsDefinition("http://dips.com/mobile.ui", "DIPS.Mobile.UI.Components.Buttons")]
[assembly: XmlnsDefinition("http://dips.com/mobile.ui", "DIPS.Mobile.UI.Components.FloatingActionButton.FloatingActionButtonMenu")]
[assembly: XmlnsDefinition("http://dips.com/mobile.ui", "DIPS.Mobile.UI.Components.ContextMenus")]
[assembly: XmlnsDefinition("http://dips.com/mobile.ui", "DIPS.Mobile.UI.Components.Lists")]
[assembly: XmlnsDefinition("http://dips.com/mobile.ui", "DIPS.Mobile.UI.Components.Labels")]
[assembly: XmlnsDefinition("http://dips.com/mobile.ui", "DIPS.Mobile.UI.Components.Images")]
[assembly: XmlnsDefinition("http://dips.com/mobile.ui", "DIPS.Mobile.UI.Components.CheckBoxes")]
[assembly: XmlnsDefinition("http://dips.com/mobile.ui", "DIPS.Mobile.UI.Components.BottomSheets")]
[assembly: XmlnsDefinition("http://dips.com/mobile.ui", "DIPS.Mobile.UI.Components.Searching")]
[assembly: XmlnsDefinition("http://dips.com/mobile.ui", "DIPS.Mobile.UI.Components.Progress")]
[assembly: XmlnsDefinition("http://dips.com/mobile.ui", "DIPS.Mobile.UI.Converters.ValueConverters")]
[assembly: XmlnsDefinition("http://dips.com/mobile.ui", "DIPS.Mobile.UI.Converters.MultiValueConverters")]
[assembly: XmlnsDefinition("http://dips.com/mobile.ui", "DIPS.Mobile.UI.Effects.DUITouchEffect")]

