using System;

/// <summary>
/// Classe intermediária entre a classe "Paciente" e o input do usuário.Possui as mesmas variáveis de "Paciente" porém em formato string, para realizar a validação.
/// </summary>
public class PacienteForm
{
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public string Nasc { get; set; }


    //Conversão dos dados validados para o tipo Paciente
    public Paciente ParseToCliente()
    {
        Paciente aux = new Paciente();
        aux.Nome = Nome;
        aux.Cpf = Cpf;
        aux.DataNasc = DateTime.ParseExact(Nasc, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        return aux;
    }
}
