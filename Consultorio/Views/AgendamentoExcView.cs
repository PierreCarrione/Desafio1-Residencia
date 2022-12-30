using System;

/// <summary>
/// Classe responsável em obter os dados do usuário e utilizar as classes validadoras para validar os mesmos.Realiza a exclusão de um paciente.
/// </summary>
public class AgendamentoExcView
{
    public AgendamentoForm agendamento { set; get; }
    private ErroDAO erro;

    public AgendamentoExcView()
	{
        agendamento = new AgendamentoForm();
        erro = new ErroDAO();
    }

    public bool ExcluirAgendamento(ValidaCpf vldCpf, ValidaAgendamento vldAgd, ValidaData vldData, ValidaHora vldHora, List<Paciente> pcts, List<Agendamentos> agds)
    {
        bool flag;
        int tentativas = 0;

        //------------------------------------- Input CPF --------------------------------------\\
        Console.Write("CPF: ");
        agendamento.Cpf = Console.ReadLine();
        flag = true;

        while (flag && tentativas < 3)
        {
            if (!vldCpf.VerificaCpf(agendamento.Cpf))
            {
                Console.Write("\nErro: CPF não é valido. Insira novamente um CPF válido.\n\nCPF: ");
                agendamento.Cpf = Console.ReadLine();
                erro.AddErro(new Erros("Cpf", agendamento.Cpf));
                tentativas++;
            }
            else if (!vldCpf.VerificaCpfExiste(agendamento.Cpf, pcts))
            {
                Console.WriteLine("Erro: Paciente não cadastrado.");
                return false;
            }
            else if (!vldAgd.VerificaAgendamentoCliente(pcts.Find(x => x.Cpf == agendamento.Cpf), agds, vldData))
            {
                Console.WriteLine("Erro: Esse paciente não possui consulta futura agendada para cancelar.");
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
        //------------------------------------ Fim Input CPF -----------------------------------\\


        //------------------------------------- Input Data -------------------------------------\\
        Console.Write("Data da consulta: ");
        agendamento.Data = Console.ReadLine();
        flag = true;
        tentativas = 0;

        while (flag && tentativas < 3)
        {
            if (!vldData.VerificaFormatoData(agendamento.Data))
            {
                Console.Write("Erro: Data da consulta deve ser no formato DD/MM/AAAA e deve ser maior ou igual a data atual.\n\nData da consulta: ");
                agendamento.Data = Console.ReadLine();
                erro.AddErro(new Erros("Data", agendamento.Data));
                tentativas++;
            }
            else if (!vldAgd.VerificaDataAgendamento(agendamento.Cpf, agendamento.Data, agds))
            {
                Console.Write("Erro: Esse paciente não possui agendamento nessa data.\n\nData da consulta: ");
                agendamento.Data = Console.ReadLine();
                erro.AddErro(new Erros("Data", agendamento.Data));
                tentativas++;
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
        //----------------------------------- Fim Input Data -----------------------------------\\


        //--------------------------------- Input Hora Inicial ---------------------------------\\
        Console.Write("Hora inicial: ");
        agendamento.HoraInicial = Console.ReadLine();
        flag = true;
        tentativas = 0;

        while (flag && tentativas < 3)
        {
            if (!vldHora.VerificaFormatoHora(agendamento.HoraInicial))
            {
                Console.Write("Erro: Hora deve ser no formato HHMM.\n\nHora inicial: ");
                agendamento.HoraInicial = Console.ReadLine();
                erro.AddErro(new Erros("Hora", agendamento.HoraInicial));
                tentativas++;
            }
            else if (!vldHora.ValidaHoraIni(agendamento.HoraInicial))
            {
                Console.Write("Erro: Hora deve estar entre 08 e 19 e os minutos aceitos são: 00, 15, 30, 45.\n\nHora inicial: ");
                agendamento.HoraInicial = Console.ReadLine();
                erro.AddErro(new Erros("Hora", agendamento.HoraInicial));
                tentativas++;
            }
            else if (!vldHora.VerificaHoraAgendamento(agendamento.Cpf, agendamento.Data, agendamento.HoraInicial, agds))
            {
                Console.Write("Erro: Paciente não possui consulta nessa hora, entre novamente com a hora correta.\n\nHora inicial: ");
                agendamento.HoraInicial = Console.ReadLine();
                erro.AddErro(new Erros("Hora", agendamento.HoraInicial));
                tentativas++;
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
        //------------------------------- Fim Input Hora Inicial -------------------------------\\
        return true;
    }
}
