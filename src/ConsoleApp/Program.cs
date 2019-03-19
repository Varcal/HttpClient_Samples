using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Models;
using Newtonsoft.Json;

namespace ConsoleApp
{
    public class Program
    {
        public static async Task Main()
        {

            Pessoa? pessoa = new Pessoa();

            using (var client = new HttpClient { BaseAddress = new Uri("https://localhost:44336") })
            {
                var json = JsonConvert.SerializeObject(new Parametros() { Id = Guid.NewGuid(), Key = Guid.NewGuid().ToString() });
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                var result = await client.PostAsync("/api/values", data);
                var pessoaJson = result.Content.ReadAsStringAsync().Result;

                var pessoa = JsonConvert.DeserializeObject<Pessoa>(pessoaJson);
                Console.WriteLine($"{pessoa.Id} - {pessoa.Nome}");
                Console.ReadKey();//
            }
        }
    }
}
