using System;

/// <summary>
/// Classe responsável em obter os dados do usuário e utilizar as classes validadoras para validar os mesmos.Realiza o cadastramento de um novo agendamento.
/// </summary>
public class PacienteCadView
{
    public PacienteForm Form { get; set; }
    private ErroDAO erro;

    public PacienteCadView()
    {
        Form = new PacienteForm();
        erro = new ErroDAO();
    }

    public bool CadNovoCliente(ValidaNome vldNome, ValidaCpf vldCpf, ValidaData vldData, List<Paciente> pcts)
    {
        bool flag;
        int tentativas = 0;

        //------------------------------------- Input Nome -------------------------------------\\
        Console.Write("Nome: ");
        Form.Nome = Console.ReadLine();
        flag = vldNome.VerificaNome(Form.Nome);

        while (!flag && tentativas < 3)
        {
            Console.Write("\nErro: Nome deve ter pelo menos 5 caracteres.\n\nNome: ");
            Form.Nome = Console.ReadLine();
            erro.AddErro(new Erros("Nome", Form.Nome));
            flag = vldNome.VerificaNome(Form.Nome);
            tentativas++;
            if (tentativas >= 3)
            {
                return false;
            }
        }
        //----------------------------------- Fim Input Nome -----------------------------------\\


        //------------------------------------- Input CPF --------------------------------------\\
        Console.Write("CPF: ");
        Form.Cpf = Console.ReadLine();
        flag = true;
        tentativas = 0;

        while (flag && tentativas < 3)
        {
            if (vldCpf.VerificaCpfExiste(Form.Cpf, pcts))
            {
                Console.WriteLine("Erro: CPF já cadastrado.");
                return false;
            }
            else if (!vldCpf.VerificaCpf(Form.Cpf))
            {
                Console.Write("\nErro: CPF não é valido. Insira novamente um CPF válido.\n\nCPF: ");
                erro.AddErro(new Erros("Cpf", Form.Cpf));
                tentativas++;
                Form.Cpf = Console.ReadLine();
            }
            else
            {
                flag = false;
            }
            if (tentativas >= 3)
            {
                return false;
            }

        }
        //------------------------------------ Fim Input CPF -----------------------------------\\


        //------------------------------------- Input Nasc -------------------------------------\\
        Console.Write("Data de nascimento: ");
        Form.Nasc = Console.ReadLine();
        flag = true;
        tentativas = 0;

        while (flag && tentativas < 3)
        {
            if (!vldData.VerificaFormatoData(Form.Nasc))
            {
                Console.Write("Erro: Data de nascimento deve ser no formato DD/MM/AAAA.\n\nData de nascimento: ");
                erro.AddErro(new Erros("Data Nascimento", Form.Nasc));
                tentativas++;
                Form.Nasc = Console.ReadLine();
            }
            else if (vldData.VerificaData(DateTime.ParseExact(Form.Nasc, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)))
            {
                Console.WriteLine("Erro: Paciente deve ter pelo menos 13 anos.");
                return false;
            }
            else
            {
                flag = false;
            }
            if (tentativas >= 3)
            {
                return false;
            }
        }
        //----------------------------------- Fim Input Nasc -----------------------------------\\

        return true;
    }
}
