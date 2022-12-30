using System;

/// <summary>
/// Classe responsável pelos métodos de validação de um cpf. 
/// </summary>
public class ValidaCpf
{
    /// <summary>
    /// Verifica se o cpf passado é um cpf válido
    /// </summary>
    /// <param name="cpf"></param>
    /// <returns>True se sim. False se não.</returns>
    public bool VerificaCpf(string cpf)
    {
        
        string auxCpf = cpf.ToString();
        if (auxCpf.Length != 11)
        {
            return false;
        }
        int freq = auxCpf.Count(f => (f == auxCpf[0]));
        int dvJ = int.Parse(auxCpf[0].ToString()) * 10 + int.Parse(auxCpf[1].ToString()) * 9 +
            int.Parse(auxCpf[2].ToString()) * 8 + int.Parse(auxCpf[3].ToString()) * 7 +
            int.Parse(auxCpf[4].ToString()) * 6 + int.Parse(auxCpf[5].ToString()) * 5 +
            int.Parse(auxCpf[6].ToString()) * 4 + int.Parse(auxCpf[7].ToString()) * 3 + int.Parse(auxCpf[8].ToString()) * 2;
        int dvK = int.Parse(auxCpf[0].ToString()) * 11 + int.Parse(auxCpf[1].ToString()) * 10 +
            int.Parse(auxCpf[2].ToString()) * 9 + int.Parse(auxCpf[3].ToString()) * 8 +
            int.Parse(auxCpf[4].ToString()) * 7 + int.Parse(auxCpf[5].ToString()) * 6 +
            int.Parse(auxCpf[6].ToString()) * 5 + int.Parse(auxCpf[7].ToString()) * 4 +
            int.Parse(auxCpf[8].ToString()) * 3 + int.Parse(auxCpf[9].ToString()) * 2;

        //Se cpf for com todos os numeros iguais ou de tamanho diferente de 11
        if (cpf.ToString().Length != 11 || freq == 11)
        {
            return false;
        }
        //Verifica se o digito J ou K para o resto da divisão entre 0 e 1 é diferente de 0.
        if (((dvJ % 11 == 0 || dvJ % 11 == 1) && int.Parse(auxCpf[9].ToString()) != 0) || ((dvK % 11 == 0 || dvK % 11 == 1) && int.Parse(auxCpf[10].ToString()) != 0))
        {
            return false;
        }
        //Verifica se o resto da divisão está entre 2 a 10 e se o valor J ou K é igual a 11-resto
        if ((dvJ % 11 > 10 || dvJ % 11 < 0) || (dvK % 11 > 10 || dvK % 11 < 0) || (11 - dvJ % 11 != int.Parse(auxCpf[9].ToString())) || (11 - dvK % 11 != int.Parse(auxCpf[10].ToString())))
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// Verifica se o cpf passado como argumento consta na lista de pacientes cadastrados.
    /// </summary>
    /// <param name="cpf"></param>
    /// <param name="pcts"></param>
    /// <returns>True se sim. False se não.</returns>
    public bool VerificaCpfExiste(string cpf, List<Paciente> pcts)
    {
        return pcts.Any(x => x.Cpf == cpf);
    }
}
