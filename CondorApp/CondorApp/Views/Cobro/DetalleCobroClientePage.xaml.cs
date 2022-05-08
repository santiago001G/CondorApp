using CondorApp.ViewModels.Cobro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CondorApp.Views.Cobro
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalleCobroClientePage : ContentPage
    {
        public DetalleCobroClientePage()
        {
            InitializeComponent();
            this.BindingContext = new DetalleCobroClienteViewModel();
        }
    }
}