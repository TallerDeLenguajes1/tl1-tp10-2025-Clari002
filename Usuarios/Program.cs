// See https://aka.ms/new-console-template for more information
using System;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

class Program
{
    private static readonly HttpClient client = new HttpClient();

    static async Task Main(string[] args)
    {
        await GetUsuarios();
    }

    private static async Task GetUsuarios()
    {
        var url = "https://jsonplaceholder.typicode.com/users/";
        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();

        List<Usuario> usuarios = JsonSerializer.Deserialize<List<Usuario>>(responseBody);

        Console.WriteLine("Primeros 5 usuarios:");
        Console.WriteLine("---------------------");

        for (int i = 0; i < 5; i++)
        {
            var user = usuarios[i];
            Console.WriteLine($"Nombre: {user.name}");
            Console.WriteLine($"Email: {user.email}");
            Console.WriteLine($"Domicilio: {user.address}");
            Console.WriteLine();
        }

        // Guardar todos los usuarios en archivo JSON
        string jsonCompleto = JsonSerializer.Serialize(usuarios, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync("usuarios.json", jsonCompleto);
        Console.WriteLine("Usuarios guardados en usuarios.json");
    }
}
