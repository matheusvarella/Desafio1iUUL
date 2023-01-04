using System;
using System.Collections.Generic;

namespace AdmDentalOffice.LayoutControllers
{
    public static class PrintErro
    {
        public static void PrintErros(List<string> erros)
        {
            Console.WriteLine();

            foreach (var erro in erros)
            {
                Console.WriteLine("Erro: " + erro);
            }

            Console.WriteLine();
        }
    }
}