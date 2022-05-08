using CondorApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CondorApp.Helpers;
using System.Linq;
using System.Net.Http;

namespace CondorApp.Services
{
    public class CobroService: BaseService
    {
        public async Task<Tuple<List<Cliente>,List<Cliente>>> ObtenerClientesPendientes()
        {
                Repositorio repo = new Repositorio();

                var responseHttp = await repo.Get<Tuple<List<Cliente>, List<Cliente>>>($"{API_URL}/clientes/clientesPendientes");

                if (!responseHttp.Error)
                {
                    return responseHttp.Response;
                }

                return null;
        }

    }
}
