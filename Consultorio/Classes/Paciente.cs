using System;

/// <summary>
/// Classe que herda a classe Pessoa e que possui uma variável do tipo "Agendamentos" que irá armazenar a última consulta do paciente.Possui um construtor sem parâmetros
/// e um com a inicialização das variáveis.
/// </summary>
public class Paciente: Pessoa
{
    public Agendamentos Agendamento { get; set; }

    public Paciente(){}

    public Paciente(string cpf, string nome, DateTime dataNasc) 
        :base(cpf, nome,dataNasc){}

}
