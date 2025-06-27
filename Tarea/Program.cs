// See https://aka.ms/new-console-template for more information

using System;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

class Program{
private static readonly HttpClient client = new HttpClient();

    static async Task Main(string[] args)
    {
    await GetTareas();
    
}
private static async Task GetTareas()
{
    var url = "https://jsonplaceholder.typicode.com/todos/";
    var response = await client.GetAsync(url);
    response.EnsureSuccessStatusCode();
    var responseBody = await response.Content.ReadAsStringAsync();

    List<Tarea> tareas = JsonSerializer.Deserialize<List<Tarea>>(responseBody);

    foreach (var tarea in tareas)
    {
        string estado = tarea.Completo ? "Completada" : "Pendiente";
        Console.WriteLine($"- {tarea.Titulo} [{estado}]");
    }

    // Guardar todas las tareas en un archivo JSON
    string jsonCompleto = JsonSerializer.Serialize(tareas, new JsonSerializerOptions { WriteIndented = true });
    await File.WriteAllTextAsync("tareas.json", jsonCompleto);
    Console.WriteLine("\nArchivo tareas.json guardado con éxito!");
}
}

