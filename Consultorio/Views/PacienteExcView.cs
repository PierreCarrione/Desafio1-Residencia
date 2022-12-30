using System;

/// <summary>
/// Classe responsável em obter os dados do usuário e utilizar as classes validadoras para validar os mesmos.Realiza a exclusão de um paciente.
/// </summary>
public class PacienteExcView
{
    public PacienteForm Form { get; set; }
    private ErroDAO erro;

    public PacienteExcView()
	{
        Form = new PacienteForm();
        erro = new ErroDAO();
    }

    public bool ExcluirPaciente(ValidaCpf vldCpf, ValidaAgendamento vldAgend,ValidaData vldData ,List<Paciente> pcts, List<Agendamentos> agds)
    {
        bool flag;
        int tentativas = 0;
        Console.Write("CPF: ");
        Form.Cpf = Console.ReadLine();
        flag = true;

        while (flag && tentativas < 3)
        {
            if (!vldCpf.VerificaCpf(Form.Cpf))
            {
                Console.Write("\nErro: CPF não é valido. Insira novamente um CPF válido.\n\nCPF: ");
                Form.Cpf = Console.ReadLine();
                erro.AddErro(new Erros("Cpf", Form.Cpf));
                tentativas++;
            }
            else if (!vldCpf.VerificaCpfExiste(Form.Cpf, pcts))
            {
                Console.WriteLine("Erro: Paciente não cadastrado.");
                return false;
            }
            else if (vldAgend.VerificaAgendamentoCliente(pcts.Find(x => x.Cpf == Form.Cpf), agds, vldData))
            {
                Console.WriteLine("Erro: Paciente possui uma consulta agendada.");
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

        return true;
    }
}
