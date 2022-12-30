using System;
using System.Text.Json;

/// <summary>
/// Classe responsável pela persistencia dos dados do cadastro dos pacientes.
/// </summary>
public class PacienteDAO
{
	private string path = "Pacientes.json";
    public List<Paciente> Pacientes { get; set; }
    
    public PacienteDAO()
    {
        Pacientes = new List<Paciente>();
        if (!File.Exists(path))
        {
            using (StreamWriter sw = File.CreateText(path))
            {}
        }
    }        

	public void CadastrarPaciente(Paciente clt)
	{
		using (FileStream arquivo = File.Open(path, FileMode.Append))
		using (StreamWriter sw = new StreamWriter(arquivo))
		{
			string json = JsonSerializer.Serialize<Paciente>(clt);
			sw.WriteLine(json);
        }
    }

    public List<Paciente> RecuperarPacientes()
    {
        Pacientes.Clear();

        using (FileStream arquivo = File.Open(path, FileMode.Open))
        using (StreamReader sr = new StreamReader(arquivo))
        {
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                Pacientes.Add(JsonSerializer.Deserialize<Paciente>(line)!);
            }
        }
       
        return Pacientes;
    }

    public void AddAgendPaciente(Agendamentos agd)
    {
        Pacientes = RecuperarPacientes();
        int indice = Pacientes.FindIndex(x => x.Cpf == agd.Cpf);
        Pacientes[indice].Agendamento = agd;

        using (FileStream arquivo = File.Open(path, FileMode.Create))
        using (StreamWriter sw = new StreamWriter(arquivo))
        {
            for (int i = 0; i < Pacientes.Count; i++)
            {
                string json = JsonSerializer.Serialize<Paciente>(Pacientes[i]);
                sw.WriteLine(json);
            }
        }
    }

    public void ExcluiAgendPaciente(int indice)
    {
        Pacientes = RecuperarPacientes();
        Pacientes[indice].Agendamento = null;

        using (FileStream arquivo = File.Open(path, FileMode.Create))
        using (StreamWriter sw = new StreamWriter(arquivo))
        {
            for (int i = 0; i < Pacientes.Count; i++)
            {
                string json = JsonSerializer.Serialize<Paciente>(Pacientes[i]);
                sw.WriteLine(json);
            }
        }
    }

    public void ExcluirPaciente(string cpf)
	{
        Pacientes = RecuperarPacientes();
        int indice = Pacientes.FindIndex(x => x.Cpf == cpf);
        Pacientes.RemoveAt(indice);

        using (FileStream arquivo = File.Open(path, FileMode.Create))
        using (StreamWriter sw = new StreamWriter(arquivo))
        {
            for (int i = 0; i < Pacientes.Count; i++)
            {
                string json = JsonSerializer.Serialize<Paciente>(Pacientes[i]);
                sw.WriteLine(json);
            }
        }
    }
}
