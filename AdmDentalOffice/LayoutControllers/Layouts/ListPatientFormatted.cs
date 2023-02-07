using System;
using System.Collections.Generic;
using AdmDentalOffice.Models;

namespace AdmDentalOffice.LayoutControllers.Layouts
{
    internal class ListPatientFormatted
    {
        private static string baseList = "-------------------------------------------------------------\r\n";

        public static void PrintListPatient(Dictionary<Patient, Appointment> items)
        {
            if (items.Equals(null))
            {
                Console.WriteLine("Não ha pacientes para listar");
            }
            else
            {
                Console.WriteLine(baseList);
                Console.WriteLine($"CPF         Nome                              Dt.Nasc. Idade");
                Console.WriteLine(baseList);
                foreach (var item in items)
                {
                    Console.Write(item.Key.Cpf + " ");
                    FormateName(item.Key.Name + " ");
                    Console.Write(item.Key.BirthDate.ToString().Substring(0, 10) + " ");
                    Console.WriteLine(FormateYears(item.Key.BirthDate.ToString()));

                    if (item.Value != null)
                    {
                        Console.WriteLine("            " + item.Value.ConsultationDate);
                        Console.Write("            ");
                        FormateHour(item.Value.StartTime);
                        Console.Write(" às ");
                        FormateHour(item.Value.EndTime);
                        Console.WriteLine();
                    }
                }
            }
        }

                                        

        private static void FormateName(string name)
        {
            if (name.Length > 32)
            {
                Console.Write(name.Substring(0, 31));
            }

            var difference = 32 - name.Length;

            Console.Write(name);

            for (int i = 0; i < difference; i++)
            {
                Console.Write(" ");
            }
        }

        public static void FormateHour(string hour)
        {
            var aux = hour.Substring(0, 2);
            Console.Write(aux);
            Console.Write(":");
            aux = hour.Substring(2, 2);
            Console.Write(aux);
        }

        public static string FormateDuration(string initialTime, string finalTime)
        {
            int result = int.Parse(finalTime) - int.Parse(initialTime);

            if (result > 999)
            {
                return result.ToString();
            }
            if (result > 99)
            {
                return "0" + result;
            }
            if (result > 9)
            {
                return "00" + result;
            }
            return "0000";
        }

        public static string FormateYears(string birthDate)
        {
            var birthDateDateTime = DateTime.Parse(birthDate);

            var idade = DateTime.Now.Year - birthDateDateTime.Year;

            if (DateTime.Now.DayOfYear < birthDateDateTime.DayOfYear)
                idade--;

            if (idade > 999)
            {
                return idade.ToString();
            }

            if (idade > 99) 
            { 
                return " " + idade.ToString();
            }

            if (idade > 9)
            {
                return "  " + idade.ToString();
            }

            return "    ";
        }
    }
}
