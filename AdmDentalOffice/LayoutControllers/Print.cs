using System;

namespace AdmDentalOffice.LayoutControllers
{
    public static class Print
    {

        public static void PrintStart()
        {
            int option;
            do
            {
                
                Console.WriteLine(
                        "Menu Principal\r\n" +
                        "1-Pacientes\r\n" +
                        "2-Agenda\r\n" +
                        "3-Fim");

                option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        PrintRegisterPatient.Register();
                        break;
                    case 2:
                        PrintAgenda.Agenda();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Finalizando o Sistema!");
                        break;
                    default:
                        Console.WriteLine("Opção informada inválida! Informe novamente!");
                        break;
                }

            } while (option != 3);
        
            Console.WriteLine("Sistema Finalizado!");
            Environment.Exit(0);
        }
    }
}
