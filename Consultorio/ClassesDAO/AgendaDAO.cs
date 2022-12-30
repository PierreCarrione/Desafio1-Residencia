using System;
using System.Text.Json;

/// <summary>
/// Classe que é responsável pela persistencia dos agendamentos.
/// </summary>
public class AgendaDAO
{
    private string path = "Horarios.json";
    public static List<Agendamentos> Horarios { get; set; }

    public AgendaDAO()
    {
        Horarios = new List<Agendamentos>();
        if (!File.Exists(path))
        {
            using (StreamWriter sw = File.CreateText(path))
            {}
        }
    }

    public void CadastrarHorario(Agendamentos agenda)
    {
        using (FileStream arquivo = File.Open(path, FileMode.Append))
        using (StreamWriter sw = new StreamWriter(arquivo))
        {
            string json = JsonSerializer.Serialize<Agendamentos>(agenda);
            sw.WriteLine(json);
        }
    }

    public List<Agendamentos> RecuperarAgendamentos()
    {
        Horarios.Clear();//Garante que Horarios sempre terá as informações sempre atualizadas
        using (FileStream arquivo = File.Open(path, FileMode.Open))
        using (StreamReader sr = new StreamReader(arquivo))
        {
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                Horarios.Add(JsonSerializer.Deserialize<Agendamentos>(line)!);
            }
        }
        return Horarios;
    }

    public void ExcluirHorario(string cpf, DateTime data, TimeSpan horaInicial)
    {
        Horarios = RecuperarAgendamentos();
        int indice = Horarios.FindIndex(dat => (dat.Cpf == cpf) && (dat.DataConsulta == data) && (dat.HoraInicial == horaInicial));
        Horarios.RemoveAt(indice);

        using (FileStream arquivo = File.Open(path, FileMode.Create))
        using (StreamWriter sw = new StreamWriter(arquivo))
        {
            for (int i = 0; i < Horarios.Count; i++)
            {
                string json = JsonSerializer.Serialize<Agendamentos>(Horarios[i]);
                sw.WriteLine(json);
            }
        }
    }

    public void ExcluirHorarios(string cpf)
    {
        Horarios = RecuperarAgendamentos();
        Horarios.RemoveAll(x => x.Cpf == cpf);

        using (FileStream arquivo = File.Open(path, FileMode.Create))
        using (StreamWriter sw = new StreamWriter(arquivo))
        {
            for (int i = 0; i < Horarios.Count; i++)
            {
                string json = JsonSerializer.Serialize<Agendamentos>(Horarios[i]);
                sw.WriteLine(json);
            }
        }
    }
}
