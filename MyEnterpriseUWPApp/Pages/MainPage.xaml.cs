namespace MyEnterpriseUWPApp
{
    using MyEnterpriseUWPApp.ViewModels;

    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.ViewModel = new MainPageViewModel();
            this.DataContext = this.ViewModel;
        }

        public MainPageViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.ViewModel.OnNavigatedTo();
        }
    }
}