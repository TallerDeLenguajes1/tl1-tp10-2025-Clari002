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
        await GetPerritos();
    }

    private static async Task GetPerritos()
    {
        var url = "https://dog.ceo/api/breeds/image/random/5";

        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();

        RespuestaDog resultado = JsonSerializer.Deserialize<RespuestaDog>(body);

        Console.WriteLine(" Imágenes aleatorias de perros:");
        foreach (var imagen in resultado.message)
        {
            Console.WriteLine(imagen);
        }

        // Guardar en archivo JSON
        string jsonGuardado = JsonSerializer.Serialize(resultado, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync("perros.json", jsonGuardado);

        Console.WriteLine("\n Datos guardados en perros.json");
    }
}

