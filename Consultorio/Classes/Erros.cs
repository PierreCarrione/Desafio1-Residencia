using System;

/// <summary>
/// Classe que possui 2 variáveis(campo, valorDigitado) para descrever o tipo do erro e o valor que foi digitado errado.
/// </summary>
public class Erros
{
    public string campo { get; set; }
    public string valorDigitado { get; set; }

    public Erros(string tipoErro, string mensagem)
    {
        this.campo = tipoErro;
        this.valorDigitado = mensagem;
    }
}
