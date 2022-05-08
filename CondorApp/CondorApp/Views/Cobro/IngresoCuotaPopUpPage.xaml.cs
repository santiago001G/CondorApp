using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CondorApp.Views.Cobro
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IngresoCuotaPopUpPage : PopupPage
    {
        public decimal Cuota { get; set; } = new decimal(0);
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }

        public bool EsCuotaProducto { get; set; } = false;

        public IngresoCuotaPopUpPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public IngresoCuotaPopUpPage(int idProducto, string nombreProucto)
        {
            InitializeComponent();

            IdProducto = idProducto;
            NombreProducto = nombreProucto;
            EsCuotaProducto = true;

            BindingContext = this;
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {

            MessagingCenter.Send<Page, decimal>(this, "ValorCuota", Cuota);

            Navigation.RemovePopupPageAsync(this);
        }

        private void Button_Clicked_1(object sender, System.EventArgs e)
        {
            MessagingCenter.Send<Page, decimal>(this, "ValorCuota", 0);
            Navigation.RemovePopupPageAsync(this);
        }
    }
}