using CondorApp.ViewModels.Cobro;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CondorApp.Views.Cobro
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListadoCobroPage : ContentPage
    {
        public ListadoCobroPage()
        {
            InitializeComponent();
            this.BindingContext = new ListadoCobroViewModel();
        }
    }
}