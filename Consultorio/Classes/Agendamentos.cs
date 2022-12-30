using System;

/// <summary>
/// Classe que possui 4 variáveis(Cpf, DataConsulta, HoraInicial, HoraFinal) para a realização de um agendamento.
/// </summary>
public class Agendamentos
{
	public string Cpf { get; set; }
    public DateTime DataConsulta { get; set; }	
    public TimeSpan HoraInicial { get; set; }
    public TimeSpan HoraFinal { get; set; }

    public Agendamentos(){}
}
