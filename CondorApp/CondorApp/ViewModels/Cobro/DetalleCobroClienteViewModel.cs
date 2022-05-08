using CondorApp.Models;
using CondorApp.Views.Cobro;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace CondorApp.ViewModels.Cobro
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public class DetalleCobroClienteViewModel:BaseViewModel
    {
        #region Interfaces
        private IPopupNavigation _popup { get; set; }
        #endregion

        #region Properties

        private Cliente cliente;
        private ObservableCollection<ProductoCliente> productosCliente;
        private int id;
        private bool esPagadoHoy;
        private int IdProductoCobrar = 0;
        private decimal valorPagadoHoy;
        private decimal deudaTotal;
        private int cantidadArticulos;

        public Cliente Cliente
        {
            get { return cliente; }
            set
            {
                cliente = value;
                OnPropertyChanged();
            }
        }
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                CargarCliente(value);
            }
        }
        public ObservableCollection<ProductoCliente> ProductosCliente
        {
            get { return productosCliente; }
            set
            {
                productosCliente = value;
                OnPropertyChanged();
            }
        }
        public bool EsPagadoHoy
        {
            get { return esPagadoHoy; }
            set
            {
                esPagadoHoy = value;
                OnPropertyChanged();
            }
        }
        public decimal ValorPagadoHoy
        {
            get { return valorPagadoHoy; }
            set
            {
                valorPagadoHoy = value;
                OnPropertyChanged();
            }
        }
        public decimal DeudaTotal
        {
            get { return deudaTotal; }
            set
            {
                deudaTotal = value;
                OnPropertyChanged();
            }
        }
        public int CantidadArticulos
        {
            get { return cantidadArticulos; }
            set 
            { 
                cantidadArticulos = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #region Commands
        public Command AbrirPopUpCuotaCommand { get; set; }

        public Command AbrirPopUpCuotaProductoCommand { get; set; }

        public Command AbrirPopUpHistoricoProductoCommand { get; set; }

        public Command EliminarCuotaCommand { get; set; }

        public Command EditarCuotaCommand { get; set; }

        public Command FinalizarCobroCommand { get; set; }
        #endregion

        #region Constructor

        public DetalleCobroClienteViewModel()
        {
            _popup = PopupNavigation.Instance;
            AbrirPopUpCuotaCommand = new Command(AbrirPopUpCuotaCommandHandler);
            AbrirPopUpHistoricoProductoCommand = new Command(AbrirPopUpHistoricoProductoCommandHandler);
            AbrirPopUpCuotaProductoCommand = new Command<int>(AbrirPopUpCuotaProductoCommandHandler);
            EliminarCuotaCommand = new Command(EliminarCuotaCommandHandler);
            EditarCuotaCommand = new Command(EditarCuotaCommandHandler);
            FinalizarCobroCommand = new Command(FinalizarCobroCommandHandler);
        }

        #endregion

        #region Command Handlers
        
        private async void AbrirPopUpCuotaCommandHandler()
        {
            MessagingCenter.Subscribe<Page, decimal>(this, "ValorCuota", (p, valor) => {
                RegistrarCuota(valor);
            });

            await _popup.PushAsync(new IngresoCuotaPopUpPage());
        }

        private async void AbrirPopUpCuotaProductoCommandHandler(int idProducto)
        {
            IdProductoCobrar = idProducto;

            MessagingCenter.Subscribe<Page, decimal>(this, "ValorCuota", (p, valor) => {
                RegistrarCuotaProducto(valor, idProducto);
            });

            string nombreProducto = ProductosCliente
                .FirstOrDefault(x => x.IdMercaderia == idProducto)
                .NombreProducto;

            await _popup.PushAsync(new IngresoCuotaPopUpPage(idProducto, nombreProducto));
        }

        private async void AbrirPopUpHistoricoProductoCommandHandler()
        {

            MessagingCenter.Subscribe<Page, decimal>(this, "ValorCuota", (p, valor) => {
                RegistrarCuota(valor);
            });

            await _popup.PushAsync(new IngresoCuotaPopUpPage());
        }

        private void EliminarCuotaCommandHandler()
        {
            EsPagadoHoy = false;
            DeudaTotal = DeudaTotal - ValorPagadoHoy;
            ValorPagadoHoy = 0;
        }

        private async void EditarCuotaCommandHandler()
        {
            MessagingCenter.Subscribe<Page, decimal>(this, "ValorCuota", (p, valor) => {
                ActualizarCuota(valor);
            });

            await _popup.PushAsync(new IngresoCuotaPopUpPage());
        }

        private async void FinalizarCobroCommandHandler()
        {
            await Shell.Current.GoToAsync($"//{nameof(ListadoCobroPage)}");
        }

        #endregion

        #region Privates

        private void RegistrarCuota(decimal valorCuota)
        {
            if (valorCuota >0)
            {
                EsPagadoHoy = true;
                ValorPagadoHoy = valorCuota;
                DeudaTotal = 250 - valorCuota;
            }

            MessagingCenter.Unsubscribe<Page, decimal>(this, "ValorCuota");
        }

        private void RegistrarCuotaProducto(decimal valorCuota,int idProducto)
        {
            if (valorCuota > 0)
            {
                EsPagadoHoy = true;
                ValorPagadoHoy = valorCuota;
                DeudaTotal = 250 - valorCuota;
            }

            MessagingCenter.Unsubscribe<Page, decimal>(this, "ValorCuota");
        }

        private void ActualizarCuota(decimal valorCuota)
        {
            if (valorCuota > 0)
            {
                EsPagadoHoy = true;
                ValorPagadoHoy = valorCuota;
                DeudaTotal = 250 - valorCuota;
            }

            MessagingCenter.Unsubscribe<Page, decimal>(this, "ValorCuota");
        }

        private void CargarCliente(int idClienteBuscar)
        {
            List<Cliente> listaClientesMock = new List<Cliente>();
            List<ProductoCliente> listaProductosMock = new List<ProductoCliente>();

            listaClientesMock.Add(new Cliente()
            {
                Id = 1,
                Nombre = "Andrés Rodriguez Peralta",
                EstadoPagos = "Al día",
                ValorCuota = 25,
                PagadoHoy = true,
                Latitud = "-74.1818474",
                Logitud = "4.6241701",
                Ubicacion = "4.6241701;-74.1818474",
                Telefono = "3222910995",
                PeridicidadCobro = "Semanal",
                Direccion = "Calle Falsa 12345678",
                Descripcion = "Cliente vive en el piso #5, tocar en la puerta gris",
            });

            listaClientesMock.Add(new Cliente()
            {
                Id = 2,
                Nombre = "Pepe Mujica Rodriguez",
                EstadoPagos = "Al día",
                ValorCuota = 50,
                PagadoHoy = true,
                Latitud = "-74.1818474",
                Logitud = "4.6241701",
                Ubicacion = "4.6241701;-74.1818474",
                Telefono = "3222910995",
                PeridicidadCobro = "Semanal",
                Direccion = "Calle Falsa 12345678",
                Descripcion = "Cliente vive en el piso #5, tocar en la puerta gris"

            });

            listaClientesMock.Add(new Cliente()
            {
                Id = 3,
                Nombre = "Maria Auxiliadora Montero",
                EstadoPagos = "Mora",
                ValorCuota = 12,
                PagadoHoy = false,
                Latitud = "-74.1818474",
                Logitud = "4.6241701",
                Ubicacion = "4.6241701;-74.1818474",
                Telefono = "3222910995",
                PeridicidadCobro = "Semanal",
                Direccion = "Calle Falsa 12345678",
                Descripcion = "Cliente vive en el piso #5, tocar en la puerta gris"
            });

            listaProductosMock.Add(new ProductoCliente
            {
                Id = 1,
                IdCliente = 1,
                NombreProducto = "Cubrelecho doble Jacquard",
                ValorCuota = 10.0M,
                Anotaciones = "Anotacion",
                EstadoPago = "Al día",
                FechaCompra = DateTime.Now,
                Retirado = "Retirado",
                IdMercaderia = 1,
                ValorCompra = 20.0M
            });

            listaProductosMock.Add(new ProductoCliente
            {
                Id = 2,
                IdCliente = 1,
                NombreProducto = "Sábana 1/2 plaza",
                ValorCuota = 20.0M,
                Anotaciones = "Anotacion",
                EstadoPago = "Al día",
                FechaCompra = DateTime.Now,
                Retirado = "Retirado",
                IdMercaderia = 2,
                ValorCompra = 20.0M
            });

            this.Cliente = listaClientesMock.FirstOrDefault(x => x.Id == idClienteBuscar);
            EsPagadoHoy = Cliente.PagadoHoy;
            CantidadArticulos = listaProductosMock.Count();

            if (Cliente != null)
            {
                ProductosCliente = new ObservableCollection<ProductoCliente>();
                foreach (var producto in listaProductosMock)
                {
                    productosCliente.Add(producto);
                }
            }
        }
        #endregion

    }
}
