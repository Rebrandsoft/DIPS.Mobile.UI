using System.Reflection;
using DIPS.Mobile.UI.Components.Resources.LocalizedStrings;

namespace DIPS.Mobile.UI.Components;
using Extensions;

public class MainPage : DIPS.Mobile.UI.Components.Pages.ContentPage
    {
        public MainPage()
        {
            Title = "Components";
            var sampleTypes = Enum.ToList<SampleType>();
            Content = new DIPS.Mobile.UI.Components.Lists.ListView()
            {
                HasUnevenRows = true,
                ItemsSource = sampleTypes,
                ItemTemplate = new DataTemplate(() => new ViewCell() {View = new NavigateToSamplesButton()}),
                SeparatorVisibility = SeparatorVisibility.None
            };
        }
    }

    public enum SampleType
    {
        Resources,
        Components,
    }

    public class ResourceSample : Sample
    {
        public ResourceSample(string resourceName) : base(resourceName)
        {
        }
    }

    public class ComponentSample : Sample
    {
        public ComponentSample(string name) : base(name)
        {
        }
    }

    public abstract class Sample : Attribute
    {
        private readonly string m_resourceResourceName; 
        protected Sample(string resourceName)
        {
            m_resourceResourceName = resourceName;
        }

        public string Name => LocalizedStrings.ResourceManager.GetString(m_resourceResourceName);
    }

    public class NavigateToSamplesButton : Button
    {
        private SampleType m_sampleType;

        public NavigateToSamplesButton()
        {
            Margin = 5;
            SetBinding(TextProperty, new Binding() {Path = ""});
            Command = new Command(TryNavigateToSamplesPage);
        }

        private void TryNavigateToSamplesPage() {
            var samples = new Dictionary<Func<Page>, Sample>();
            switch (m_sampleType)
            {
                case SampleType.Resources:
                    samples = GetSamples<ResourceSample>();
                    break;
                case SampleType.Components:
                    samples = GetSamples<ComponentSample>();
                    break;
            }

            if (samples.Any())
            {
                Application.Current?.MainPage?.Navigation.PushAsync(new SamplesPage(m_sampleType, samples));
            }
            else
            {
                Application.Current?.MainPage?.DisplayAlert("No samples",
                    $"Theres no samples for {m_sampleType.ToString()} yet.", "Ok");
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (BindingContext is SampleType sampleType)
            {
                m_sampleType = sampleType;
            }
        }

        public Dictionary<Func<Page>, Sample> GetSamples<TSample>() where TSample : Sample
        {
            var samples = new Dictionary<Func<Page>, Sample>();
            var types = Assembly.GetExecutingAssembly().GetTypes().OrderBy(t => t.Name);
            foreach (var type in types)
            { 
                if (type.GetCustomAttributes(typeof(TSample), true).Length > 0)
                {
                    var sample = type.GetCustomAttributes(typeof(TSample), true).First() as TSample;
                    samples.Add(() =>
                    {
                        if (Activator.CreateInstance(type) is Page page)
                        {
                            return page;
                        }

                        return null;
                    }, sample);
                }
            }

            return samples;
        }
    }