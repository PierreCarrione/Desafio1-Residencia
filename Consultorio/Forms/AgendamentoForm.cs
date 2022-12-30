using System;

/// <summary>
/// Classe intermediária entre a classe "Agendamentos" e o input do usuário.Possui as mesmas variáveis de "Agendamento" porém em formato string, para realizar a validação.
/// </summary>
public class AgendamentoForm
{
	public string Cpf { get; set; }
	public string Data { get; set; }
    public string HoraInicial { get; set; }
    public string HoraFinal { get; set; }


    //Conversão dos dados validados para o tipo Agendamento
    public Agendamentos ParseToAgendamento()
    {
        Agendamentos aux = new Agendamentos();
        aux.Cpf = Cpf;
        aux.DataConsulta = DateTime.ParseExact(Data, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        aux.HoraInicial = new TimeSpan(int.Parse(HoraInicial) /100, int.Parse(HoraInicial) % 100, 00);
        aux.HoraFinal = new TimeSpan(int.Parse(HoraFinal) / 100, int.Parse(HoraFinal) % 100, 00);

        return aux;
    }

    public Agendamentos ParseToAgendamentoSemFinal()
    {
        Agendamentos aux = new Agendamentos();
        aux.Cpf = Cpf;
        aux.DataConsulta = DateTime.ParseExact(Data, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        aux.HoraInicial = new TimeSpan(int.Parse(HoraInicial) / 100, int.Parse(HoraInicial) % 100, 00);

        return aux;
    }
}
