using System;
using System.Collections.Generic;
using AdmDentalOffice.Models;

namespace AdmDentalOffice.LayoutControllers.Layouts
{
    public static class ListAgendaFormatted
    {
        private static string baseList = "-------------------------------------------------------------\r\n";
        
        public static void PrintListAgenda(Dictionary<Appointment, Patient> items)
        {
            string date = "";
            Console.WriteLine(baseList);
            Console.WriteLine($"   Data    H.Ini H.Fim Tempo Nome                   Dt.Nasc.");
            Console.WriteLine(baseList);
            foreach (var item in items)
            {
                if (date == item.Key.AppointmentDate)
                {
                    Console.WriteLine("          .");
                }
                else
                {
                    date = item.Key.AppointmentDate;
                    Console.Write($"{date} ");
                }

                FormateHour(item.Key.StartTime);
                Console.Write(" ");
                FormateHour(item.Key.EndTime);
                Console.Write(" ");
                FormateHour(FormateDuration(item.Key.StartTime, item.Key.EndTime));
                Console.Write(" ");
                FormateName(item.Value.Name);
                Console.WriteLine($"{item.Value.BirthDate.ToString().Substring(0,10)}");
            }
                                  
        }

        private static void FormateName(string name)
        {
            if (name.Length > 22)
            {
                Console.Write(name.Substring(0, 21));
            }

            var difference = 22 - name.Length;

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
            aux = hour.Substring(2,2);
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
    }
}