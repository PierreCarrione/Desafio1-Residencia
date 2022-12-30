using System;

/// <summary>
/// Classe responsável pelos métodos de validação de uma data.
/// </summary>
public class ValidaData
{ 
    /// <summary>
    /// Verifica se a data é maior ou igual a data atual
    /// </summary>
    /// <param name="data"></param>
    /// <returns>true se sim e false se não</returns>
    public bool VerificaData(DateTime data)
    {
        return data.Date >= DateTime.Now.Date;
    }

    /// <summary>
    /// Verifica se a string recebida como parâmetro consegue ser transformada no formato "dd/MM/yyyy" de data
    /// </summary>
    /// <param name="data"></param>
    /// <returns>True caso sim.False caso não.</returns>
    public bool VerificaFormatoData(string data)
    {
        try
        {
            DateTime aux;
            aux = DateTime.ParseExact(data, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            return true;
        }
        catch (Exception){}

        return false;
    }

    /// <summary>
    /// Verifica se idade é menor que 13 anos.
    /// </summary>
    /// <param name="data">Data a ser feito o cáculo da idade</param>
    /// <returns>True se sim. False se não.</returns>
    public bool VerificaIdade(DateTime data)
    {
        return ((DateTime.Now - data).Days / 365 < 13);
    }
}
