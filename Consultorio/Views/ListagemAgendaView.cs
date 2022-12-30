using System;
using System.Linq;

/// <summary>
/// Classe que irá mostrar para o usuário todos os agendamentos realizados/a fazer ou em um período específico.
/// </summary>
public class ListagemAgendaView
{

	public void ListarAgenda(IEnumerable<Agendamentos> agds, List<Paciente> pcts)
	{
        var aux = agds.ToList();

        //Verica se a lista está vazia
        if (aux.Count == 0)
        {
            Console.WriteLine("Consultório ainda não possui agendamentos marcados.");
            Thread.Sleep(2000);
        }
        else
        {
            var ordenado = agds.OrderBy(x => x.DataConsulta);
            DateTime auxIgual = new DateTime(1900,1,1);//Variável que irá fazer as comparações se as horas são iguais para apenas mostrar uma vez.Comecei com uma data antiga 
            //pois se colocasse a data atual poderia ser que o primeiro agendamento fosse da data atual ai não mostraria ela.
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine("{0,-11}{1,-6}{2,-6}{3,-6}{4,-30}{5}", "Data", "H.Ini", "H.Fim", "Tempo", "Nome", "Dt.Nasc.");
            Console.WriteLine("--------------------------------------------------------------------");

            foreach (var agenda in ordenado)
            {
                int indice = pcts.FindIndex(x => x.Cpf == agenda.Cpf);

                if (auxIgual.Date == agenda.DataConsulta)//Verifica se a data atual é igual a data que passou.Se for não irá mostrar a data novamente
                {
                    if (indice != -1)//Verifica se o paciente ainda está cadastrado. Caso não esteja, irá imprimir "---" no lugar do nome e nascimento
                    {
                        Console.WriteLine("{0,16}{1,6}{2,6}{3,17}{4,23}", agenda.HoraInicial.ToString("hh\\:mm"), agenda.HoraFinal.ToString("hh\\:mm"),
                        (agenda.HoraFinal - agenda.HoraInicial).ToString("hh\\:mm"), pcts[indice].Nome, pcts[indice].DataNasc.ToString("dd/MM/yyyy"));
                    }
                    else
                    {
                        Console.WriteLine("{0,16}{1,6}{2,6}{3,6}{4,32}", agenda.HoraInicial.ToString("hh\\:mm"), agenda.HoraFinal.ToString("hh\\:mm"),
                        (agenda.HoraFinal - agenda.HoraInicial).ToString("hh\\:mm"), "-----", "-----");
                    }
                    
                }
                else
                {
                    if (indice != -1)
                    {
                        Console.WriteLine("{0,-11}{1,-6}{2,-6}{3,-6}{4,-29}{5}", agenda.DataConsulta.ToString("dd/MM/yyyy"), agenda.HoraInicial.ToString("hh\\:mm"),
                            agenda.HoraFinal.ToString("hh\\:mm"), (agenda.HoraFinal - agenda.HoraInicial).ToString("hh\\:mm"), pcts[indice].Nome, pcts[indice].DataNasc.ToString("dd/MM/yyyy"));
                    }
                    else
                    {
                        Console.WriteLine("{0,-11}{1,-6}{2,-6}{3,-6}{4,-31}{5}", agenda.DataConsulta.ToString("dd/MM/yyyy"), agenda.HoraInicial.ToString("hh\\:mm"),
                            agenda.HoraFinal.ToString("hh\\:mm"), (agenda.HoraFinal - agenda.HoraInicial).ToString("hh\\:mm"), "-----", "-----");
                    }
                   
                }
                
                auxIgual = agenda.DataConsulta;

            }
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Pressione qualquer tecla para continuar.");
            Console.ReadKey();
        }
    }
}
