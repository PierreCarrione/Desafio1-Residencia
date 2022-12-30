using System;
using System.Text.Json;

/// <summary>
/// Classe responsável pela persistencia dos erros gerados ao decorrer da execução.
/// </summary>
public class ErroDAO
{
    private string path = "Erros.json";

    public ErroDAO()
	{
        if (!File.Exists(path))
        {
            using (StreamWriter sw = File.CreateText(path))
            { }
        }
    }

    public void AddErro(Erros erro)
    {
        using (FileStream arquivo = File.Open(path, FileMode.Append))
        using (StreamWriter sw = new StreamWriter(arquivo))
        {
            string json = JsonSerializer.Serialize<Erros>(erro);
            sw.WriteLine(json);
        }
    }
}
