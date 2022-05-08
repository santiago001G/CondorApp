using CondorApp.Models;
using CondorApp.Views.Cobro;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CondorApp.ViewModels.Cobro
{
    public class ListadoCobroViewModel: BaseViewModel
    {
        #region Interfaces
        private IPopupNavigation _popup { get; set; }
        #endregion

        #region Propiedades

        private ObservableCollection<Cliente> clientes;
        private List<Cliente> clientesPendientes;
        private List<Cliente> clientesCobrados;
        private string tipoListado;

        public string TipoListado
        {
            get { return tipoListado; }
            set
            {
                tipoListado = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Cliente> Clientes
        {
            get { return clientes; }
            set
            {
                clientes = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #region Commands
        public Command CambiarTipoListaCommand { get; set; }

        public Command BuscarClienteCommand { get; set; }

        public Command AbrirUbicacionClienteCommand { get; set; }

        public Command AbrirInfoClienteCommand { get; set; }

        public Command AbrirDetalleCobroCommand { get; set; }

        public Command LlamarClienteCommand { get; set; }

        #endregion

        #region Constructor
        public ListadoCobroViewModel()
        {
            TipoListado = "pendientes";
            Clientes = new ObservableCollection<Cliente>();
            LlenarListaClientes();
            _popup = PopupNavigation.Instance;

            CambiarTipoListaCommand = new Command<string>(CambiarTipoListaCommandHandler);
            BuscarClienteCommand = new Command<string>(BuscarClienteCommandHandler);
            AbrirUbicacionClienteCommand = new Command<string>(AbrirUbicacionClienteCommandHandler);
            LlamarClienteCommand = new Command<string>(LlamarClienteCommandHandler);
            AbrirInfoClienteCommand = new Command<int>(AbrirInfoClienteCommandHandler);
            AbrirDetalleCobroCommand = new Command<int>(AbrirDetalleCobroCommandHandler);
        }

        #endregion

        #region Command Handlers
        private void CambiarTipoListaCommandHandler(string tipoListado)
        {
            if (TipoListado != tipoListado)
            {
                TipoListado = tipoListado;
                Clientes.Clear();

                if (TipoListado == "pendientes")
                {
                    foreach (var item in clientesPendientes)
                    {
                        Clientes.Add(item);
                    }
                }
                else
                {
                    foreach (var item in clientesCobrados)
                    {
                        Clientes.Add(item);
                    }
                }
            }
        }

        private void BuscarClienteCommandHandler(string nombreBusqueda)
        {
            if (string.IsNullOrEmpty(nombreBusqueda))
            {
                RestaurarListaClientes();

                return;
            }

            if (TipoListado == "pendientes")
            {
                clientes.Clear();
                var clientesBusqueda = clientesPendientes.Where(x => x.Nombre.Contains(nombreBusqueda)).ToList();

                foreach (var item in clientesBusqueda)
                {
                    Clientes.Add(item);
                }
            }
            else
            {
                clientes.Clear();
                var clientesBusqueda = clientesCobrados.Where(x => x.Nombre.ToLower().Contains(nombreBusqueda.ToLower())).ToList();
                foreach (var item in clientesBusqueda)
                {
                    Clientes.Add(item);
                }
            }
        }

        private async void AbrirUbicacionClienteCommandHandler(string ubicacion)
        {
            var arrayUbicacion = ubicacion.Split(';');

            if (Device.RuntimePlatform == Device.Android)
            {
                await Launcher.OpenAsync($"https://maps.google.com/?q={arrayUbicacion[0]},{arrayUbicacion[1]}");
            }
        }

        private void LlamarClienteCommandHandler(string numero)
        {
            if (!string.IsNullOrEmpty(numero))
            {
                try
                {
                    PhoneDialer.Open(numero);
                }
                catch (ArgumentNullException)
                {
                    // Number was null or white space
                }
                catch (FeatureNotSupportedException)
                {
                    // Phone Dialer is not supported on this device.
                }
                catch (Exception)
                {
                    // Other error has occurred.
                }
            }
        }

        private async void AbrirInfoClienteCommandHandler(int idCliente)
        {
            if (TipoListado == "pendientes")
            {
                var cliente = clientesPendientes.FirstOrDefault(x => x.Id == idCliente);
                await _popup.PushAsync(new InfoClientePopUpPage(cliente));
            }
            else
            {
                var cliente = clientesCobrados.FirstOrDefault(x => x.Id == idCliente);
                await _popup.PushAsync(new InfoClientePopUpPage(cliente));
            }
        }

        private async void AbrirDetalleCobroCommandHandler(int idCliente)
        {
            await Shell.Current.GoToAsync($"{nameof(DetalleCobroClientePage)}?{nameof(Cliente.Id)}={idCliente}");
        }

        #endregion

        #region Privates

        private void RestaurarListaClientes()
        {
            Clientes.Clear();
            if (TipoListado == "pendientes")
            {
                foreach (var item in clientesPendientes)
                {
                    Clientes.Add(item);
                }
            }
            else
            {
                foreach (var item in clientesCobrados)
                {
                    Clientes.Add(item);
                }
            }
        }

        private void LlenarListaClientes()
        {
            clientesPendientes = new List<Cliente>();
            clientesCobrados = new List<Cliente>();
            clientesCobrados.Add(new Cliente()
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
                Descripcion = "Cliente vive en el piso #5, tocar en la puerta gris"
            });

            clientesCobrados.Add(new Cliente()
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

            clientesPendientes.Add(new Cliente()
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

            foreach (var item in clientesPendientes)
            {
                Clientes.Add(item);
            }
        }
        #endregion
    }
}
