using System;

/// <summary>
/// Classe que irá mostrar para o usuário todos os clientes cadastrados.
/// </summary>
public class ListagemPacienteView
{

    public void ListarOrdenadoCpf(List<Paciente> pcts)
	{
        if (pcts.Count == 0)
        {
            Console.WriteLine("Consultório ainda não possui pacientes cadastrados."); 
            Thread.Sleep(1000);
        }
        else
        {
            var ordenado = pcts.OrderBy(x => x.Cpf);

            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("{0,-12}{1,-30}{2,-11}{3}", "CPF", "Nome", "Dt.Nasc", "Idade");
            Console.WriteLine("----------------------------------------------------------");
            foreach (var paciente in ordenado)
            {
                if (paciente.Agendamento!= null)
                {
                    if (paciente.Agendamento.DataConsulta.Date >= DateTime.Now.Date)
                    {
                        Console.WriteLine("{0,-12}{1,-30}{2,-14}{3}", paciente.Cpf, paciente.Nome, paciente.DataNasc.ToString("dd/MM/yyyy"), (DateTime.Now.Year - paciente.DataNasc.Year));
                        Console.WriteLine("{0,27}{1}","Agendado para: " ,paciente.Agendamento.DataConsulta.ToString("dd/MM/yyyy"));
                        Console.WriteLine("{0,17} às {1}",paciente.Agendamento.HoraInicial.ToString("hh\\:mm"), paciente.Agendamento.HoraFinal.ToString("hh\\:mm"));

                    }
                    else
                    {
                        Console.WriteLine("{0,-12}{1,-30}{2,-14}{3}", paciente.Cpf, paciente.Nome, paciente.DataNasc.ToString("dd/MM/yyyy"), (DateTime.Now.Year - paciente.DataNasc.Year));
                    }
                }
                else
                {
                    Console.WriteLine("{0,-12}{1,-30}{2,-14}{3}", paciente.Cpf, paciente.Nome, paciente.DataNasc.ToString("dd/MM/yyyy"), (DateTime.Now.Year - paciente.DataNasc.Year));
                }
                
            }
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Pressione qualquer tecla para continuar.");
            Console.ReadKey();
        }
    }

    public void ListarOrdenadoNome(List<Paciente> pcts)
    {

        if (pcts.Count == 0)
        {
            Console.WriteLine("Consultório ainda não possui pacientes cadastrados.");
            Thread.Sleep(1000);
        }
        else
        {
            var ordenado = pcts.OrderBy(x => x.Nome);
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("{0,-12}{1,-30}{2,-11}{3}", "CPF", "Nome", "Dt.Nasc", "Idade");
            Console.WriteLine("----------------------------------------------------------");

            foreach (var paciente in ordenado)
            {
                if (paciente.Agendamento != null)
                {
                    if (paciente.Agendamento.DataConsulta.Date >= DateTime.Now.Date)
                    {
                        Console.WriteLine("{0,-12}{1,-30}{2,-14}{3}", paciente.Cpf, paciente.Nome, paciente.DataNasc.ToString("dd/MM/yyyy"), (DateTime.Now.Year - paciente.DataNasc.Year));
                        Console.WriteLine("{0,27}{1}", "Agendado para: ", paciente.Agendamento.DataConsulta.ToString("dd/MM/yyyy"));
                        Console.WriteLine("{0,17} às {1}", paciente.Agendamento.HoraInicial.ToString("hh\\:mm"), paciente.Agendamento.HoraFinal.ToString("hh\\:mm"));

                    }
                    else
                    {
                        Console.WriteLine("{0,-12}{1,-30}{2,-14}{3}", paciente.Cpf, paciente.Nome, paciente.DataNasc.ToString("dd/MM/yyyy"), (DateTime.Now.Year - paciente.DataNasc.Year));
                    }
                }
                else
                {
                    Console.WriteLine("{0,-12}{1,-30}{2,-14}{3}", paciente.Cpf, paciente.Nome, paciente.DataNasc.ToString("dd/MM/yyyy"), (DateTime.Now.Year - paciente.DataNasc.Year));
                }

            }
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Pressione qualquer tecla para continuar.");
            Console.ReadKey();
        }
    }
}
