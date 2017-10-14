namespace MyEnterpriseUWPApp
{
    using System;
    using System.Threading.Tasks;

    using MyEnterpriseUWPApp.Services.Products;

    using Windows.ApplicationModel;
    using Windows.ApplicationModel.Activation;
    using Windows.ApplicationModel.Core;
    using Windows.Foundation;
    using Windows.Foundation.Metadata;
    using Windows.UI;
    using Windows.UI.ViewManagement;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    using WinUX.Diagnostics;
    using WinUX.Xaml;

    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : Application
    {
        private static IProductService productService;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += this.OnSuspending;
        }

        public static IProductService ProductService => productService ?? (productService = new ProductService());

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            try
            {
                await this.SetupApplicationShellAsync();
            }
            catch (Exception ex)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine(ex.ToString());
#endif
            }

            if (!(Window.Current.Content is Frame rootFrame))
            {
                rootFrame = new Frame { Language = Windows.Globalization.ApplicationLanguages.Languages[0] };
                rootFrame.NavigationFailed += this.OnNavigationFailed;

                Window.Current.Content = rootFrame;

                UIDispatcher.Initialize();
                await AppDiagnostics.Current.StartAsync();
                await ProductService.InitializeAsync();
            }

            if (rootFrame.Content == null)
            {
                rootFrame.Navigate(typeof(MainPage), e.Arguments);
            }

            Window.Current.Activate();
        }

        private async Task SetupApplicationShellAsync()
        {
            this.SetupDesktopShell();
            await this.SetupMobileShellAsync();
        }

        private async Task SetupMobileShellAsync()
        {
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                StatusBar statusBar = StatusBar.GetForCurrentView();
                if (statusBar != null)
                {
                    await statusBar.HideAsync();
                }
            }
        }

        private void SetupDesktopShell()
        {
            if (!ApiInformation.IsTypePresent("Windows.UI.ViewManagement.ApplicationView"))
            {
                return;
            }

            if (!ApiInformation.IsTypePresent("Windows.ApplicationModel.Core.CoreApplication"))
            {
                return;
            }

            CoreApplicationView coreApplicationView = CoreApplication.GetCurrentView();
            if (coreApplicationView?.TitleBar != null)
            {
                coreApplicationView.TitleBar.ExtendViewIntoTitleBar = true;
            }

            ApplicationView applicationView = ApplicationView.GetForCurrentView();
            if (applicationView?.TitleBar != null)
            {
                applicationView.TitleBar.ButtonBackgroundColor = Colors.Transparent;
                applicationView.TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            }
            
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.Auto;

            float logicalDpi = Windows.Graphics.Display.DisplayInformation.GetForCurrentView().LogicalDpi;
            Size desiredSize = new Size(700 * 96.0f / logicalDpi, 1024 * 96.0f / logicalDpi);

            ApplicationView.PreferredLaunchViewSize = desiredSize;
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            // TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
