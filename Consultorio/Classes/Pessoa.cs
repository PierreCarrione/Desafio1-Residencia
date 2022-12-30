using System;

/// <summary>
/// Classe abstrata que possui 3 variáveis(Cpf, Nome, DataNascimento).Possui um construtor sem parâmetros e um com a inicialização das variáveis.
/// </summary>
public abstract class Pessoa
{
	public string Cpf { get; set; }
	public string Nome { get; set; }
	public DateTime DataNasc { get; set; }

	public Pessoa(){}	

	public Pessoa(string cpf, string nome, DateTime dataNasc)
	{
		Cpf = cpf;
		Nome = nome;
		DataNasc = dataNasc;	
	}
}
