using CondorApp.Helpers;
using CondorApp.Services;
using CondorApp.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("FontAwesome-Regular.ttf", Alias = "FontAwesome_Regular")]
[assembly: ExportFont("FontAwesome-Solid.ttf", Alias = "FontAwesome_Solid")]
[assembly: ExportFont("Poppins-Regular.ttf")]
[assembly: ExportFont("Poppins-Bold.ttf")]
[assembly: ExportFont("Poppins-SemiBold.ttf")]
namespace CondorApp
{

    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            SetAppTheme();

            DependencyService.Register<CobroService>();
            DependencyService.Register<Repositorio>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void SetAppTheme()
        {
            var theme = Preferences.Get("theme", string.Empty);
            if (string.IsNullOrEmpty(theme) || theme == "light")
            {
                Application.Current.UserAppTheme = OSAppTheme.Dark;
            }
            else
            {
                Application.Current.UserAppTheme = OSAppTheme.Light;
            }
        }
    }
}
