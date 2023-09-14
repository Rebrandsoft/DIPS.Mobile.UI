using System.Collections.ObjectModel;
using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Components.Content;
using DIPS.Mobile.UI.Components.Content.DataTemplateSelectors;
using DIPS.Mobile.UI.Components.Dividers;
using DIPS.Mobile.UI.Components.Labels;
using DIPS.Mobile.UI.Components.ListItems;
using DIPS.Mobile.UI.Components.ListItems.Extensions;
using DIPS.Mobile.UI.Components.Selection;
using DIPS.Mobile.UI.Converters.ValueConverters;
using DIPS.Mobile.UI.Effects.Touch;
using CheckBox = DIPS.Mobile.UI.Components.CheckBoxes.CheckBox;
using CollectionView = DIPS.Mobile.UI.Components.Lists.CollectionView;
using ActivityIndicator = DIPS.Mobile.UI.Components.Loading.ActivityIndicator;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Label = DIPS.Mobile.UI.Components.Labels.Label;
using RadioButton = DIPS.Mobile.UI.Components.Selection.RadioButton;

namespace DIPS.Mobile.UI.Components.Pickers.ItemPicker
{
    internal class ItemPickerBottomSheet : BottomSheet
    {
        private readonly ItemPicker m_itemPicker;
        private readonly List<SelectableItemViewModel> m_originalItems;

        public ItemPickerBottomSheet(ItemPicker itemPicker)
        {
            m_itemPicker = itemPicker;
            m_originalItems = new List<SelectableItemViewModel>();

            if (m_itemPicker.ItemsSource != null)
            {
                foreach (var item in m_itemPicker.ItemsSource)
                {
                    m_originalItems.Add(new SelectableItemViewModel(
                        item.GetPropertyValue(m_itemPicker.ItemDisplayProperty)!,
                        item.Equals(m_itemPicker.SelectedItem), item));
                }
            }

            Items = new ObservableCollection<SelectableItemViewModel>(m_originalItems);

            SetBinding(HasSearchBarProperty,
                new Binding()
                {
                    Source = m_itemPicker.BottomSheetPickerConfiguration,
                    Path = nameof(BottomSheetPickerConfiguration.HasSearchBar)
                });

            var collectionView = new CollectionView()
            {
                ItemTemplate = new DataTemplate(LoadTemplate), Margin = Sizes.GetSize(SizeName.size_2)
            };

            collectionView.SetBinding(ItemsView.ItemsSourceProperty, new Binding(nameof(Items), source: this));

            Content = CreateContentControlForActivityIndicator(collectionView,
                m_itemPicker.BottomSheetPickerConfiguration);
        }

        private object LoadTemplate()
        {
            if (m_itemPicker.BottomSheetPickerConfiguration is
                {SelectableItemTemplate: not null} bottomSheetConfiguration)
            {
                return CreateConsumerView(bottomSheetConfiguration.SelectableItemTemplate);
            }

            return CreateDefaultView();
        }

        public static readonly BindableProperty ItemsProperty = BindableProperty.Create(
            nameof(Items),
            typeof(ObservableCollection<SelectableItemViewModel>),
            typeof(ItemPickerBottomSheet));

        public ObservableCollection<SelectableItemViewModel> Items
        {
            get => (ObservableCollection<SelectableItemViewModel>)GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        private IView CreateConsumerView(ControlTemplate selectableItemTemplate)
        {
            var contentView = new SelectableItemContentView() {ControlTemplate = selectableItemTemplate};
            contentView.SetBinding(SelectableItemContentView.ItemProperty,
                new Binding() {Path = nameof(SelectableItemViewModel.Item)});
            contentView.SetBinding(SelectableItemContentView.IsSelectedProperty,
                new Binding() {Path = nameof(SelectableItemViewModel.IsSelected)});
            Touch.SetCommand(contentView, new Command(() => ItemWasPicked(contentView)));
            return contentView;
        }

        private IView CreateDefaultView()
        {
            var radioButtonListItem = new RadioButtonListItem() {HasBottomDivider = true};
            radioButtonListItem.SetBinding(ListItem.TitleProperty,
                new Binding() {Path = nameof(SelectableItemViewModel.DisplayName)});
            radioButtonListItem.SetBinding(RadioButtonListItem.IsSelectedProperty,
                new Binding() {Path = nameof(SelectableItemViewModel.IsSelected)});
            radioButtonListItem.SelectedCommand = new Command(() => ItemWasPicked(radioButtonListItem));
            return radioButtonListItem;
        }

        private void ItemWasPicked(BindableObject tappedObject)
        {
            if (tappedObject.BindingContext is not SelectableItemViewModel selectableListItem) return;
            if (m_itemPicker.ItemsSource == null) return;

            object? theSelectedItem = null;
            foreach (var item in m_itemPicker.ItemsSource)
            {
                if (item.Equals(selectableListItem.Item))
                {
                    theSelectedItem = item;
                }
            }

            if (theSelectedItem != null && theSelectedItem.Equals(m_itemPicker.SelectedItem))
            {
                //Make sure people can not visually deselect the radio button
                if (!selectableListItem.IsSelected)
                {
                    selectableListItem.IsSelected = true;
                }
                
                return;
            }
            
            if (!selectableListItem.IsSelected)
            {
                return;
            }

            m_itemPicker.SelectedItem = theSelectedItem;
        }

        protected override void OnSearchTextChanged(string value)
        {
            base.OnSearchTextChanged(value);

            FilterItems(value);
        }

        private void FilterItems(string value)
        {
            var filterText = value;
            if (string.IsNullOrEmpty(filterText))
            {
                Items.Clear();
                foreach (var item in m_originalItems)
                {
                    Items.Add(item);
                }
            }
            else
            {
                var filteredItems = m_originalItems.Where(p => p.DisplayName.ToLower().Contains(filterText.ToLower()))
                    .ToList();
                Items.Clear();
                foreach (var item in filteredItems)
                {
                    Items.Add(item);
                }
            }
        }

        public static ContentControl CreateContentControlForActivityIndicator(CollectionView collectionView,
            BottomSheetPickerConfiguration? bottomSheetPickerConfiguration)
        {
            var contentControl = new ContentControl()
            {
                BindingContext = bottomSheetPickerConfiguration,
                TemplateSelector = new BooleanDataTemplateSelector()
                {
                    TrueTemplate = new DataTemplate(() => new ActivityIndicator()
                    {
                        IsRunning = true,
                        IsVisible = true,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        Margin = new Thickness() {Top = Sizes.GetSize(SizeName.size_5)}
                    }),
                    FalseTemplate = new DataTemplate(() => collectionView)
                }
            };
            contentControl.SetBinding(ContentControl.SelectorItemProperty,
                new Binding() {Path = nameof(BottomSheetPickerConfiguration.IsBusy), FallbackValue = false});

            return contentControl;
        }
    }

    internal class SelectableItemContentView : ContentView
    {
        public SelectableItemContentView()
        {
            BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent;
        }

        public static readonly BindableProperty ItemProperty = BindableProperty.Create(
            nameof(Item),
            typeof(object),
            typeof(SelectableItemViewModel));

        public object Item
        {
            get => (object)GetValue(ItemProperty);
            set => SetValue(ItemProperty, value);
        }

        public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(
            nameof(IsSelected),
            typeof(bool),
            typeof(SelectableItemContentView));

        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }
    }
}