using System;
using System.Text.RegularExpressions;

/// <summary>
/// Classe responsável pelos métodos de validação de uma hora.
/// </summary>
public class ValidaHora
{
    private TimeSpan horaAbertura = new TimeSpan(08, 00, 00);
    private TimeSpan horaFechamento = new TimeSpan(19, 00, 00);

    /// <summary>
    /// Verifica se a hora passada como argumento está no formato DDDD(Numeral)
    /// </summary>
    /// <param name="hora"></param>
    /// <returns>True se sim.False se não.</returns>
    public bool VerificaFormatoHora(string hora)
    {
        return Regex.IsMatch(hora, @"^\d{4}$");
    }

    /// <summary>
    /// Verifica se a hora inicial é uma hora válida
    /// </summary>
    /// <param name="hora"></param>
    /// <returns>True se sim.False se não.</returns>
    public bool ValidaHoraIni(string hora)
    {
        int aux = int.Parse(hora);
        TimeSpan aux1 = new TimeSpan(aux / 100, aux % 100, 00);

        if (aux1.Hours >= horaAbertura.Hours && aux1.Hours < horaFechamento.Hours)
        {
            if (aux1.Minutes == 00 || aux1.Minutes == 30 || aux1.Minutes == 15 || aux1.Minutes == 45)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Verifica se a hora final é uma hora válida
    /// </summary>
    /// <param name="horaIni"></param>
    /// <param name="horaFim"></param>
    /// <returns>True se sim.False se não.</returns>
    public bool ValidaHoraFim(string horaIni, string horaFim)
    {
        int aux1 = int.Parse(horaIni);
        int aux2 = int.Parse(horaFim);
        TimeSpan auxIni = new TimeSpan(aux1 / 100, aux1 % 100, 00);
        TimeSpan auxFim = new TimeSpan(aux2 / 100, aux2 % 100, 00);

        if (auxFim.Hours >= horaAbertura.Hours && auxFim.Hours <= horaFechamento.Hours)
        {
            if (auxFim.Hours == 19)
            {
                if (auxFim.Minutes == 0)
                {
                    return true;
                }
            }
            else if (auxFim > auxIni)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Verifica se a hora passada como parâmetro existe na lista de agendamentos
    /// </summary>
    /// <param name="cpf"></param>
    /// <param name="data"></param>
    /// <param name="hora"></param>
    /// <param name="agds"></param>
    /// <returns>true se sim.False se não.</returns>
    public bool VerificaHoraAgendamento(string cpf, string data, string hora, List<Agendamentos> agds)
    {
        int aux = int.Parse(hora);
        TimeSpan auxHora = new TimeSpan(aux / 100, aux % 100, 00);
        DateTime auxData = DateTime.ParseExact(data, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        return agds.Any(x => (x.Cpf == cpf) && (x.DataConsulta.Date == auxData.Date) && (x.HoraInicial.Equals(auxHora)));
    }

    /// <summary>
    /// Verifica se a hora inicial está disponível para agendamento.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="hora"></param>
    /// <param name="agds"></param>
    /// <returns>True se estiver.Fala se não.</returns>
    public bool VerificaHoraIniDisp(string data, string hora, List<Agendamentos> agds)
    {
        DateTime auxData = DateTime.ParseExact(data, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        int aux = int.Parse(hora);
        TimeSpan auxHora = new TimeSpan(aux / 100, aux % 100, 00);

        if (agds.Any(x => x.DataConsulta == auxData))
        {
            var _aux = agds.FindAll(x => x.DataConsulta == auxData);
            if (_aux.Any(x => (auxHora >= x.HoraInicial) && (auxHora < x.HoraFinal)))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Verifica se a hora final está disponível para agendamento.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="horaFinal"></param>
    /// <param name="agds"></param>
    /// <returns>True se estiver.Fala se não.</returns>
    public bool VerificaHoraFimDisp(string data, string horaFinal, List<Agendamentos> agds)
    {
        DateTime auxData = DateTime.ParseExact(data, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        int aux1 = int.Parse(horaFinal);
        TimeSpan auxFinal = new TimeSpan(aux1 / 100, aux1 % 100, 00);

        if (agds.Any(x => x.DataConsulta == auxData))
        {
            var _aux = agds.FindAll(x => x.DataConsulta == auxData);
            if (_aux.Any(x => (auxFinal > x.HoraInicial) && (auxFinal <= x.HoraFinal)))
            {
                return false;
            }
        }

        return true;
    }
}
