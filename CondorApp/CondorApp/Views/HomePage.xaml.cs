using CondorApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CondorApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            this.BindingContext = new HomeViewModel();
        }
    }
}