using CondorApp.Models;
using Xamarin.Forms.Xaml;

namespace CondorApp.Views.Cobro
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfoClientePopUpPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        private Cliente _clienteMostrarInfo;

        public InfoClientePopUpPage()
        {
            InitializeComponent();
        }

        public InfoClientePopUpPage(Cliente ClienteMostrarInfo)
        {
            _clienteMostrarInfo = ClienteMostrarInfo;            
            InitializeComponent();
            this.BindingContext = _clienteMostrarInfo;
        }
    }
}