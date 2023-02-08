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

                if (option == 1)
                {
                    PrintPatient.Register();
                }
                else if (option == 2)
                {
                    PrintAgenda.Agenda();
                }
                else if (option == 3)
                {
                    Console.Clear();
                    Console.WriteLine("Finalizando o Sistema!");
                }
                else
                {
                    Console.WriteLine("Opção informada inválida! Informe novamente!");
                }

            } while (option != 3);
        
            Console.WriteLine("Sistema Finalizado!");
            Environment.Exit(0);
        }
    }
}
