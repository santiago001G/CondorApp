using CondorApp.ViewModels;
using CondorApp.Views;
using CondorApp.Views.Cobro;
using CondorApp.Views.Venta;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CondorApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ListadoCobroPage), typeof(ListadoCobroPage));
            Routing.RegisterRoute(nameof(InicioVentaPage), typeof(InicioVentaPage));
            Routing.RegisterRoute(nameof(DetalleCobroClientePage), typeof(DetalleCobroClientePage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//HomePage");
        }
    }
}
