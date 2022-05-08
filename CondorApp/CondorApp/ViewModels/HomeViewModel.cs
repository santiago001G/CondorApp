using CondorApp.Views.Cobro;
using CondorApp.Views.Venta;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CondorApp.ViewModels
{
    public class HomeViewModel
    {
        public Command CobroCommand { get; }
        public Command VentaCommand { get; }
        public Command ResumenCobroCommand { get; }
        public Command ResumenVentaCommand { get; }

        public HomeViewModel()
        {
            VentaCommand = new Command(OnVentaCommand);
            CobroCommand = new Command(OnCobroCommand);
        }
        private async void OnCobroCommand(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(ListadoCobroPage)}");
        }

        private async void OnVentaCommand(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(InicioVentaPage)}");
        }
    }
}
