using System;

/// <summary>
/// Classe responsável pelos métodos de validação de um agendamento.
/// </summary>
public class ValidaAgendamento
{
    /// <summary>
    /// Método que verifica se o paciente possui uma consulta futura
    /// </summary>
    /// <param name="pct"></param>
    /// <param name="agds"></param>
    /// <param name="validData"></param>
    /// <returns>True se possui. False se não.</returns>
    public bool VerificaAgendamentoCliente(Paciente pct, List<Agendamentos> agds, ValidaData validData)
    {
        var aux = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0);
        if (agds.Any(x => (x.Cpf == pct.Cpf) && validData.VerificaData(x.DataConsulta) && x.HoraInicial > aux))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Verifica se o cpf e a data passada estão na lista de agendamentos
    /// </summary>
    /// <param name="cpf"></param>
    /// <param name="data"></param>
    /// <param name="agds"></param>
    /// <returns>True caso sim. False caso não.</returns>
    public bool VerificaDataAgendamento(string cpf, string data, List<Agendamentos> agds)
    {
        DateTime aux = DateTime.ParseExact(data, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        return agds.Any(x => x.Cpf == cpf && x.DataConsulta.Date == aux.Date);
    }
}
