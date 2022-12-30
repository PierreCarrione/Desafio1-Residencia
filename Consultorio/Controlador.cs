using System;

/// <summary>
/// Classe responsável pela administração das classes. Responsável por comunicar a view com as demais classes.
/// </summary>
public class Controlador
{
    private PacienteDAO pctDao = new PacienteDAO();
    private AgendaDAO agendaDAO = new AgendaDAO();
    private ValidaNome vldNome = new ValidaNome();
    private ValidaCpf vldCpf = new ValidaCpf();
    private ValidaData vldData = new ValidaData();
    private ValidaHora vldHora = new ValidaHora();
    private ValidaAgendamento vldAgend = new ValidaAgendamento();
    private ListagemPacienteView listPacView = new ListagemPacienteView();
    private ListagemAgendaView listAgdView = new ListagemAgendaView();
    private PacienteCadView pctCadView = new PacienteCadView();
    private PacienteExcView pctExcView = new PacienteExcView();
    private AgendamentoCadView agdCadView = new AgendamentoCadView();
    private AgendamentoExcView agdExcView = new AgendamentoExcView();


    public void CadNovoPaciente()
    {
        bool aux = pctCadView.CadNovoCliente(vldNome, vldCpf, vldData, pctDao.RecuperarPacientes());

        if (aux)
        {
            Console.WriteLine("\nPaciente cadastrado com sucesso!");
            pctDao.CadastrarPaciente(pctCadView.Form.ParseToCliente());
            Thread.Sleep(2000);
        }
        else
        {
            Thread.Sleep(2000);
        }
    }

    public void CadAgendamento()
    {
        bool aux = agdCadView.CadNovoAgendamento(vldCpf, vldAgend, vldData, vldHora, pctDao.RecuperarPacientes(), agendaDAO.RecuperarAgendamentos());
        
        if (aux)
        {
            Console.WriteLine("\nAgendamento realizado com sucesso!");
            var _aux = pctDao.RecuperarPacientes().FindIndex(x => x.Cpf == agdCadView.agendamento.Cpf);
            pctDao.Pacientes[_aux].Agendamento = agdCadView.agendamento.ParseToAgendamento();//Como cada cliente só pode agendar uma consulta futura.Ele sempre fica com a ultima marcada
            agendaDAO.CadastrarHorario(pctDao.Pacientes[_aux].Agendamento);
            pctDao.AddAgendPaciente(pctDao.Pacientes[_aux].Agendamento);
            Thread.Sleep(2000);
        }
        else
        {
            Thread.Sleep(1000);
        }
    }

    public void ExcluirAgendamento()
    {
        bool aux = agdExcView.ExcluirAgendamento(vldCpf, vldAgend, vldData, vldHora, pctDao.RecuperarPacientes(), agendaDAO.RecuperarAgendamentos());

        if (aux)
        {
            Console.WriteLine("\nAgendamento excluído com sucesso!");
            var _aux = pctDao.RecuperarPacientes().FindIndex(x => x.Cpf == agdExcView.agendamento.Cpf);

            if (pctDao.Pacientes[_aux].Agendamento.DataConsulta.Date >= DateTime.Now.Date)
            {
                pctDao.ExcluiAgendPaciente(_aux);
            }

            var _aux1 = agdExcView.agendamento.ParseToAgendamentoSemFinal();
            agendaDAO.ExcluirHorario(_aux1.Cpf, _aux1.DataConsulta, _aux1.HoraInicial);
            Thread.Sleep(2000);
        }
        else
        {
            Thread.Sleep(2000);
        }
    }

    public void ExcluirPaciente()
    {
        bool aux = pctExcView.ExcluirPaciente(vldCpf, vldAgend, vldData, pctDao.RecuperarPacientes(), agendaDAO.RecuperarAgendamentos());

        if (aux)
        {
            Console.WriteLine("\nPaciente excluído com sucesso!");
            var _aux = pctDao.RecuperarPacientes().FindIndex(x => x.Cpf == pctExcView.Form.Cpf);
            pctDao.ExcluirPaciente(pctExcView.Form.Cpf);
            agendaDAO.ExcluirHorarios(pctExcView.Form.Cpf); 
            Thread.Sleep(2000);
        }
        else
        {
            Thread.Sleep(2000);
        }
    }

    public void ListPacOrdCpf()
    {
        listPacView.ListarOrdenadoCpf(pctDao.RecuperarPacientes());
    }

    public void ListPacOrdNome()
    {
        listPacView.ListarOrdenadoNome(pctDao.RecuperarPacientes());
    }
    public void ListAgedView()
    {
        char aux;
        bool flag = true;
        int tentativas = 0;

        Console.Write("Apresentar a agenda T-Toda ou P-Periodo: ");
        aux = Console.ReadLine()[0];

        while (flag && tentativas < 3)
        {
            if (aux == 'T' || aux == 't')
            {
                Console.Clear();
                listAgdView.ListarAgenda(agendaDAO.RecuperarAgendamentos(), pctDao.RecuperarPacientes());
                flag = false;
            }
            else if (aux == 'P' || aux == 'p')
            {
                bool flag2 = true;
                string dataIni = "";
                string dataFim = "";

                Console.Write("Data inicial: ");

                while (flag2)
                {
                    dataIni = Console.ReadLine();

                    if (!vldData.VerificaFormatoData(dataIni))
                    {
                        Console.Write("Data inválida ou no formato errado. Digite novamente a data no formato (DD/MM/AAAA).\nData inicial: ");
                        tentativas++;
                    }
                    else
                    {
                        flag2 = false;
                    }

                }

                flag2 = true;
                DateTime dataInicio = DateTime.ParseExact(dataIni, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                Console.Write("Data final: ");

                while (flag2)
                {
                    dataFim = Console.ReadLine();

                    if (!vldData.VerificaFormatoData(dataIni))
                    {
                        Console.Write("Data inválida ou no formato errado. Digite novamente a data no formato (DD/MM/AAAA).\nData final: ");
                        tentativas++;
                    }
                    else if (DateTime.ParseExact(dataFim, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date < dataInicio)
                    {
                        Console.Write("Data final tem que ser maior que data inicial.\nData final: ");
                        tentativas++;
                    }
                    else
                    {
                        flag2 = false;
                    }
                }
                
                DateTime dataFinal = DateTime.ParseExact(dataFim, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                var dataEspecifica = agendaDAO.RecuperarAgendamentos().Where(x => x.DataConsulta.Date >= dataInicio.Date && x.DataConsulta.Date <= dataFinal.Date);
                Console.Clear();
                if (dataEspecifica.ToList().Count == 0)
                {
                    Console.WriteLine("Não foram encontrados agendamentos nesse período.");
                    Thread.Sleep(2000);
                }
                else
                {
                    listAgdView.ListarAgenda(dataEspecifica, pctDao.RecuperarPacientes());
                }
                
                flag = false;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Opção digitada inválida, digite novamente T ou P.");
                Thread.Sleep(2000);
                tentativas++;
                Console.Clear();
                Console.Write("Apresentar a agenda T-Toda ou P-Periodo: ");
                aux = Console.ReadLine()[0];
            }
        }
    }
}
