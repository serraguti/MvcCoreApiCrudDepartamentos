using MvcCoreApiCrudDepartamentos.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MvcCoreApiCrudDepartamentos.Services
{
    public class ServiceApiDepartamentos
    {
        private MediaTypeWithQualityHeaderValue Header;
        private string UrlApi;

        public ServiceApiDepartamentos(IConfiguration configuration)
        {
            this.Header =
                new MediaTypeWithQualityHeaderValue("application/json");
            this.UrlApi = configuration.GetValue<string>
                ("ApiUrls:ApiCrudDepartamentos");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient()) 
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Departamento>> GetDepartamentosAsync()
        {
            string request = "/api/departamentos";
            List<Departamento> departamentos =
                await this.CallApiAsync<List<Departamento>>(request);
            return departamentos;
        }

        public async Task<Departamento> FindDepartamentoAsync(int id)
        {
            string request = "/api/departamentos/" + id;
            Departamento departamento =
                await this.CallApiAsync<Departamento>(request);
            return departamento;
        }

        //LOS METODOS DE ACCION NO SUELEN SER GENERICOS,
        //YA QUE NORMALMENTE, CADA UNO RECIBE DISTINTOS PARAMETROS
        public async Task DeleteDepartamentoAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/departamentos/" + id;
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                //NO NECESITA EL HEADER PORQUE NO DEVUELVE NADA
                HttpResponseMessage response =
                    await client.DeleteAsync(request);
                //SI DESEAMOS PERSONALIZAR LA EXPERIENCIA DEVOLVIENDO
                //ALGUN VALOR PARA LA PETICION
                //return response.StatusCode;
            }
        }

        public async Task InsertDepartamentoAsync
            (int id, string nombre, string localidad)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/departamentos";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                //TENEMOS QUE ENVIAR UN OBJETO JSON
                //NOS CREAMOS UN OBJETO DE LA CLASE DEPARTAMENTO
                Departamento departamento = new Departamento();
                departamento.IdDepartamento = id;
                departamento.Nombre = nombre;
                departamento.Localidad = localidad;
                //CONVERTIMOS EL OBJETO A JSON
                string json = JsonConvert.SerializeObject(departamento);
                //PARA ENVIAR DATOS (data) AL SERVICIO SE UTILIZA
                //LA CLASE StringContent, DONDE DEBEMOS INDICAR
                //LOS DATOS, SU ENCODING Y SU TIPO
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(request, content);
            }
        }

        public async Task UpdateDepartamentoAsync
            (int id, string nombre, string localidad)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/departamentos";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Departamento departamento =
                    new Departamento
                    {
                        IdDepartamento = id,
                        Nombre = nombre
                    ,
                        Localidad = localidad
                    };
                string json = JsonConvert.SerializeObject(departamento);
                StringContent content = new StringContent
                    (json, Encoding.UTF8, "application/json");
                await client.PutAsync(request, content);
            }
        }
    }
}
