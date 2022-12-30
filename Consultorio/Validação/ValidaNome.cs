using System;
using System.Text.RegularExpressions;

/// <summary>
/// Classe responsável pelos métodos de validação de um nome
/// </summary>
public class ValidaNome
{
    /// <summary>
    /// Verfica se o nome possui pelo menos 5 caracteres
    /// </summary>
    /// <param name="nome"></param>
    /// <returns>True se sim.False se não.</returns>
    public bool VerificaNome(string nome)
    {
        return Regex.IsMatch(nome, @"^\w{5,}(\s?\w+)*$");
    }
}
