using System;
using System.Text.RegularExpressions;

/// <summary>
/// Classe responsável pela interação com usuário.Irá receber os input e interagir com a classe controler
/// </summary>
public class View
{
    private Controlador controller;

    public View()
	{
        controller = new Controlador();
    }

	public void MenuPrincipal()
	{
        char flag;

        while (true)
		{
            Console.Title = "Consultório";
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            Console.WriteLine("Menu Principal\n1-Cadastro de pacientes\n2-Agenda\n3-Fim");
            flag = Console.ReadLine()[0];

            if (flag == '1')
            {
                Console.Clear();
                MenuCadPaciente();  
            }
            else if (flag == '2')
            {
                Console.Clear();
                MenuAgenda();
            }
            else if (flag == '3')
            {
                Console.Clear();
                Console.Write("Fechando aplicação");
                Thread.Sleep(1000);
                Console.Write(" .");
                Thread.Sleep(1000);
                Console.Write(" .");
                Thread.Sleep(1000);
                Console.Write(" .");
                Thread.Sleep(1000);
                Environment.Exit(0);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Valor digitado não é uma opção válida");
                Thread.Sleep(2000);
                Console.Clear();
            }
        }
	}

    public void MenuCadPaciente()
    {
        char flag;
        
        while (true)
        {
            Console.WriteLine("Menu do Cadastro de Pacientes\n1-Cadastrar novo paciente\n2-Excluir paciente\n3-Listar pacientes (ordenado por CPF)" +
                "\n4-Listar pacientes (ordenado por nome)\n5-Voltar p/ menu principal");
            flag = Console.ReadLine()[0];

            if (flag == '1')
            {
                Console.Clear();
                controller.CadNovoPaciente();
                Console.Clear();
            }
            else if (flag == '2')
            {
                Console.Clear();
                controller.ExcluirPaciente();
                Console.Clear();
            }
            else if (flag == '3')
            {
                Console.Clear();
                controller.ListPacOrdCpf();
                Console.Clear();
            }
            else if (flag == '4')
            {
                Console.Clear();
                controller.ListPacOrdNome();
                Console.Clear();
            }
            else if (flag == '5')
            {
                Console.Clear();
                MenuPrincipal();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Valor digitado não é uma opção válida");
                Thread.Sleep(2000);
                Console.Clear();
            }
        }
    }
    public void MenuAgenda()
    {
        char flag;

        while (true)
        {
            Console.WriteLine("Agenda\n1-Agendar consulta\n2-Cancelar agendamento\n3-Listar agenda" +
                "\n4-Voltar p/ menu principal");
            flag = Console.ReadLine()[0];

            if (flag == '1')
            {
                Console.Clear();
                controller.CadAgendamento();
                Console.Clear();
            }
            else if (flag == '2')
            {
                Console.Clear();
                controller.ExcluirAgendamento();
                Console.Clear();
            }
            else if (flag == '3')
            {
                Console.Clear();
                controller.ListAgedView();
                Console.Clear();
            }
            else if (flag == '4')
            {
                Console.Clear();
                MenuPrincipal();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Valor digitado não é uma opção válida");
                Thread.Sleep(2000);
                Console.Clear();
            }
        }
    }
}
