using CondorApp.Models;
using CondorApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CondorApp.ViewModels
{
    public class ListadoCobroViewModel: BaseViewModel
    {
        private ObservableCollection<Cliente> clientes;


        public ObservableCollection<Cliente> Clientes
        {
            get { return clientes; }
            set { clientes = value; }
        }

        public ListadoCobroViewModel()
        {
            Clientes = new ObservableCollection<Cliente>();
            TestLoad();
        }

        private async void TestLoad()
        {
            CobroService cobroService = new CobroService();
            var tuplaClientes = await cobroService.ObtenerClientesPendientes();

            foreach (var item in tuplaClientes.Item1)
            {
                Clientes.Add(item);
            }
        }
    }
}
