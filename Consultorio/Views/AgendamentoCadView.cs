using System;

/// <summary>
/// Classe responsável em obter os dados do usuário e utilizar as classes validadoras para validar os mesmos.Realiza o cadastramento de um novo paciente.
/// </summary>
public class AgendamentoCadView
{
    public AgendamentoForm agendamento { set; get; }
    private ErroDAO erro;

    public AgendamentoCadView()
	{
        agendamento = new AgendamentoForm();
        erro = new ErroDAO();
    }

    public bool CadNovoAgendamento(ValidaCpf vldCpf , ValidaAgendamento vldAgd, ValidaData vldData, ValidaHora vldHora, List<Paciente> pcts, List<Agendamentos> agds)
    {
        bool flag = true;
        int tentativas = 0;

        //------------------------------------- Input CPF --------------------------------------\\
        Console.Write("CPF: ");
        agendamento.Cpf = Console.ReadLine();

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
            else if (vldAgd.VerificaAgendamentoCliente(pcts.Find(x => x.Cpf == agendamento.Cpf), agds, vldData))
            {
                Console.WriteLine("Erro: Já existe uma consulta futura agendada para esse paciente.");
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
                Console.Write("Erro: Data da consulta deve ser no formato DD/MM/AAAA.\n\nData da consulta: ");
                agendamento.Data = Console.ReadLine();
                erro.AddErro(new Erros("Data", agendamento.Data));
                tentativas++;
            }
            else if (!vldData.VerificaData(DateTime.ParseExact(agendamento.Data, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)))
            {
                Console.Write("Erro: Data da consulta deve ser maior ou igual a data atual.\n\nData da consulta: ");
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
            else if (!vldHora.VerificaHoraIniDisp(agendamento.Data, agendamento.HoraInicial, agds))
            {
                Console.Write("Erro: Hora inicial em conflito com um agendamento existente.\n\nHora inicial: ");
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


        //---------------------------------- Input Hora Final ----------------------------------\\
        Console.Write("Hora final: ");
        agendamento.HoraFinal = Console.ReadLine();
        flag = true;
        tentativas = 0;

        while (flag && tentativas < 3)
        {
            if (!vldHora.VerificaFormatoHora(agendamento.HoraFinal))
            {
                Console.Write("Erro: Hora deve ser no formato HHMM.\n\nHora final: ");
                agendamento.HoraFinal = Console.ReadLine();
                erro.AddErro(new Erros("Hora", agendamento.HoraFinal));
                tentativas++;
            }
            else if (!vldHora.ValidaHoraFim(agendamento.HoraInicial, agendamento.HoraFinal))
            {
                Console.Write("Erro: Hora deve estar entre 08 e 19 e os minutos aceitos são: 00, 15, 30, 45.\n\nHora final: ");
                agendamento.HoraFinal = Console.ReadLine();
                erro.AddErro(new Erros("Hora", agendamento.HoraFinal));
                tentativas++;
            }
            else if (!vldHora.VerificaHoraFimDisp(agendamento.Data, agendamento.HoraFinal, agds))
            {
                Console.Write("Erro: Hora final em conflito com um agendamento existente.\n\nHora final: ");
                agendamento.HoraFinal = Console.ReadLine();
                erro.AddErro(new Erros("Hora", agendamento.HoraFinal));
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
        //-------------------------------- Fim Input Hora Final --------------------------------\\

        return true;
    }
}
