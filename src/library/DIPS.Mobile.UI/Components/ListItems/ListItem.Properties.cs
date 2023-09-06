using System.ComponentModel;
using System.Windows.Input;
using DIPS.Mobile.UI.Components.ListItems.Options;
using DIPS.Mobile.UI.Components.ListItems.Options.Dividers;
using DIPS.Mobile.UI.Converters.ValueConverters;

namespace DIPS.Mobile.UI.Components.ListItems
{
    public partial class ListItem
    {
        public event EventHandler? Tapped;
    
        /// <summary>
        /// The parameter of <see cref="Command"/>
        /// </summary>
        public object? CommandParameter
        {
            get => (object)GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }
    
        /// <summary>
        /// The Command to be executed when the <see cref="ListItem"/> is tapped
        /// </summary>
        /// <remarks>Will automatically add touch effect on <see cref="ListItem"/></remarks>
        public ICommand? Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        /// Sets the Title of the <see cref="ListItem"/>
        /// </summary>
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        /// <summary>
        /// Sets the Icon of the <see cref="ListItem"/>, which will be displayed to the left of <see cref="Title"/>
        /// </summary>
        public ImageSource Icon
        {
            get => (ImageSource)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }
    
        /// <summary>
        /// Sets the subtitle of the <see cref="ListItem"/>, which will be displayed below the <see cref="Title"/>
        /// </summary>
        public string Subtitle
        {
            get => (string)GetValue(SubtitleProperty);
            set => SetValue(SubtitleProperty, value);
        }
    
        /// <summary>
        /// The item to be placed in-line to the title and subtitle
        /// </summary>
        [TypeConverter(typeof(InLineContentTypeConverter))]
        public IView InLineContent
        {
            get => (IView)GetValue(InLineContentProperty);
            set => SetValue(InLineContentProperty, value);
        }
    
        /// <summary>
        /// The item to be placed under <see cref="Title"/> <see cref="Subtitle"/> <see cref="Icon"/> and <see cref="InLineContent"/>
        /// </summary>
        [TypeConverter(typeof(UnderlyingContentTypeConverter))]
        public IView UnderlyingContent
        {
            get => (IView)GetValue(UnderlyingContentProperty);
            set => SetValue(UnderlyingContentProperty, value);
        }
    
        /// <summary>
        /// Sets the title of the list item that people will see 
        /// </summary>
        public Options.Title.TitleOptions TitleOptions
        {
            get => (Options.Title.TitleOptions)GetValue(TitleOptionsProperty);
            set => SetValue(TitleOptionsProperty, value);
        }

        /// <summary>
        /// Sets the subtitle that people will see below <see cref="InLineContentOptions"/>
        /// </summary>
        public Options.Subtitle.SubtitleOptions SubtitleOptions
        {
            get => (Options.Subtitle.SubtitleOptions)GetValue(SubtitleOptionsProperty);
            set => SetValue(SubtitleOptionsProperty, value);
        }

        /// <summary>
        /// Sets the icon that will be displayed to the left of <see cref="InLineContentOptions"/>
        /// </summary>
        public Options.Icon.IconOptions IconOptions
        {
            get => (Options.Icon.IconOptions)GetValue(IconOptionsProperty);
            set => SetValue(IconOptionsProperty, value);
        }

        /// <summary>
        /// Sets the <see cref="Microsoft.Maui.CornerRadius"/> of the list item
        /// </summary>
        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }
    
        /// <summary>
        /// Configures the <see cref="InLineContent"/>
        /// </summary>
        public Options.InLineContent.InLineContentOptions InLineContentOptions
        {
            get => (Options.InLineContent.InLineContentOptions)GetValue(InLineContentOptionsProperty);
            set => SetValue(InLineContentOptionsProperty, value);
        }
        
        /// <summary>
        /// Configures the top and bottom divider of the <see cref="ListItem"/>
        /// </summary>
        public DividersOptions DividersOptions
        {
            get => (DividersOptions)GetValue(DividersOptionsProperty);
            set => SetValue(DividersOptionsProperty, value);
        }
   
        /// <summary>
        /// Determines whether the <see cref="ListItem"/> should display a Divider on the top
        /// </summary>
        public bool HasTopDivider
        {
            get => (bool)GetValue(HasTopDividerProperty);
            set => SetValue(HasTopDividerProperty, value);
        }
    
        /// <summary>
        /// Determines whether the <see cref="ListItem"/> should display a Divider on the bottom
        /// </summary>
        public bool HasBottomDivider
        {
            get => (bool)GetValue(HasBottomDividerProperty);
            set => SetValue(HasBottomDividerProperty, value);
        }
    
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
            nameof(CornerRadius),
            typeof(CornerRadius),
            typeof(ListItem),
            propertyChanged: ((bindable, _, _) => ((ListItem)bindable).SetCornerRadius()));
    
        public static readonly BindableProperty HasTopDividerProperty = BindableProperty.Create(
            nameof(HasTopDivider),
            typeof(bool),
            typeof(ListItem),
            propertyChanged: ((bindable, _, _) => ((ListItem)bindable).AddDivider(true)));

        public static readonly BindableProperty HasBottomDividerProperty = BindableProperty.Create(
            nameof(HasBottomDivider),
            typeof(bool),
            typeof(ListItem),
            propertyChanged: ((bindable, _, _) => ((ListItem)bindable).AddDivider(false)));

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
            nameof(CommandParameter),
            typeof(object),
            typeof(ListItem));
    
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            nameof(Command),
            typeof(ICommand),
            typeof(ListItem),
            propertyChanged: (bindable, _, _) => ((ListItem)bindable).SetTouchIsEnabled());
    
        public static new readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(
            nameof(BackgroundColor),
            typeof(Color),
            typeof(ListItem));
    
        public static readonly BindableProperty UnderlyingContentProperty = BindableProperty.Create(
            nameof(UnderlyingContent),
            typeof(IView),
            typeof(ListItem),
            propertyChanged: (bindable, _, _) => ((ListItem)bindable).AddUnderlyingContent());
    
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(ListItem), 
            propertyChanged: (bindable, _, _) => ((ListItem)bindable).AddTitle());
    
        public static readonly BindableProperty SubtitleProperty = BindableProperty.Create(
            nameof(Subtitle),
            typeof(string),
            typeof(ListItem), 
            propertyChanged: (bindable, _, _) => ((ListItem)bindable).AddSubtitle());

        public static readonly BindableProperty IconProperty = BindableProperty.Create(
            nameof(Icon),
            typeof(ImageSource),
            typeof(ListItem),
            propertyChanged: (bindable, _, _) => ((ListItem)bindable).AddIcon());

        public static readonly BindableProperty InLineContentProperty = BindableProperty.Create(
            nameof(InLineContent),
            typeof(IView),
            typeof(ListItem),
            propertyChanged: (bindable, _, _) => ((ListItem)bindable).AddInLineContent());
        
        public new Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        #region OptionBindableProperties

        public static readonly BindableProperty TitleOptionsProperty = BindableProperty.Create(
            nameof(TitleOptions),
            typeof(Options.Title.TitleOptions),
            typeof(ListItem),
            defaultValueCreator: CreateOptionsAndBind<Options.Title.TitleOptions>,
            propertyChanged: (bindable, _, newValue) => ((Options.Title.TitleOptions)newValue).Bind(((ListItem)bindable)));

        public static readonly BindableProperty SubtitleOptionsProperty = BindableProperty.Create(
            nameof(SubtitleOptions),
            typeof(Options.Subtitle.SubtitleOptions),
            typeof(ListItem),
            defaultValueCreator: CreateOptionsAndBind<Options.Subtitle.SubtitleOptions>,
            propertyChanged: (bindable, _, newValue) => ((Options.Subtitle.SubtitleOptions)newValue).Bind(((ListItem)bindable)));
    
        public static readonly BindableProperty IconOptionsProperty = BindableProperty.Create(
            nameof(IconOptions),
            typeof(Options.Icon.IconOptions),
            typeof(ListItem),
            defaultValueCreator: CreateOptionsAndBind<Options.Icon.IconOptions>,
            propertyChanged: (bindable, _, newValue) => ((Options.Icon.IconOptions)newValue).Bind(((ListItem)bindable)));

        public static readonly BindableProperty InLineContentOptionsProperty = BindableProperty.Create(
            nameof(InLineContentOptions),
            typeof(Options.InLineContent.InLineContentOptions),
            typeof(ListItem),
            defaultValueCreator: CreateOptionsAndBind<Options.InLineContent.InLineContentOptions>,
            propertyChanged: (bindable, _, newValue) => ((Options.InLineContent.InLineContentOptions)newValue).Bind(((ListItem)bindable)));

        public static readonly BindableProperty DividersOptionsProperty = BindableProperty.Create(
            nameof(DividersOptions),
            typeof(DividersOptions),
            typeof(ListItem),
            defaultValueCreator: CreateOptionsAndBind<DividersOptions>,
            propertyChanged: (bindable, _, newValue) => ((DividersOptions)newValue).Bind((ListItem)bindable));
    
        private static T CreateOptionsAndBind<T>(BindableObject bindable) where T : ListItemOptions, new()
        {
            if (bindable is not ListItem listItem)
                return default!;
        
            var options = new T();
            options.Bind(listItem);

            return options;
        }

        #endregion
    
    }
}
