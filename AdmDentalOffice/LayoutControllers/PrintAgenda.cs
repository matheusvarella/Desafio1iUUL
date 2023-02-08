using AdmDentalOffice.Controllers;
using AdmDentalOffice.LayoutControllers.Layouts;
using AdmDentalOffice.Models;
using AdmDentalOffice.Validators;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace AdmDentalOffice.LayoutControllers
{
    public static class PrintAgenda
    {
        public static void Agenda()
        {
            string option;
            do
            {
                Console.WriteLine(
                    "Agenda\r\n" +
                    "1-Agendar consulta\r\n" +
                    "2-Cancelar agendamento\r\n" +
                    "3-Listar agenda\r\n" +
                    "4-Voltar p/ menu principal"
                    );
                option = Console.ReadLine();


                if (option.Equals('1'))

                {
                    Console.Clear();
                    RegisterConsult();
                    Console.WriteLine();
                }

                else if (option.Equals('2'))

                {
                    Console.Clear();
                    DeleteConsult();
                    Console.WriteLine();
                }

                else if (option.Equals('3'))

                {
                    Console.Clear();
                    ListAgenda();
                    Console.WriteLine();
                }

                else if(option.Equals('4'))

                {
                    Console.Clear();
                    Print.PrintStart();
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Opção informada inválida! Informe novamente!");
                }


            } while (option.Equals('4'));

        }

        public static void RegisterConsult()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR", false);
            bool containsError = true;
            
            do
            {
                try
                {
                    Console.Write("CPF: ");
                    var cpf = long.Parse(Console.ReadLine());
                    
                    PatientValidation.CpfValidation(cpf.ToString());
                    
                    Console.Write("Data da consulta: ");
                    var consultDate = Console.ReadLine();

                    Console.Write("Hora inicial: ");
                    var intialHour = Console.ReadLine();

                    Console.Write("Hora final: ");
                    var finalHour = Console.ReadLine();

                    Appointment appointment = new Appointment(cpf, consultDate, intialHour, finalHour);
                        
                    ListAppointment.InsertAppointment(appointment);
                
                    Console.WriteLine("\r\nAgendamento realizado com sucesso!");

                    containsError = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (containsError);
            
        }

        public static void DeleteConsult()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR", false);
            bool containsError = true;
            
            do
            {
                try
                {
                    Console.Write("CPF: ");
                    var cpf = long.Parse(Console.ReadLine());

                    PatientValidation.CpfValidation(cpf.ToString());
                    
                    Console.Write("Data da consulta: ");
                    var consultDate = Console.ReadLine();

                    Console.Write("Hora inicial: ");

                    var initialHour = Console.ReadLine();

                    Appointment appointment = ListAppointment.GetAppointment(cpf, consultDate, initialHour);


                    ListAppointment.RemoveAppointment(appointment);
                    
                    Console.WriteLine("\r\nAgendamento realizado com sucesso!");

                    containsError = false;
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (containsError);
            
        }

        public static void ListAgenda()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR", false);
            bool containsError = true;

            do
            {
                try
                {
                    Console.Write("Apresentar a agenda T-Toda ou P-Periodo: ");
                    string option = Console.ReadLine().ToUpper();

                    if (option == "P")
                    {
                        AppointmentValidation validator = new AppointmentValidation();

                        Console.Write("Data inicial: ");
                        var initialDate = Console.ReadLine();

                        Console.Write("Data final: ");
                        var finalDate = Console.ReadLine();

                        validator.StarTimeValidation(initialDate);
                        
                        validator.EndTimeValidation(finalDate);
                        
                        var list = ListAppointment.ListAppointmentsByPeriod(initialDate, finalDate);

                        ListAgendaFormatted.PrintListAgenda(list);
                        
                    } 
                    else if (option == "T") 
                    {
                        var list = ListAppointment.ListAllAppointments();

                        ListAgendaFormatted.PrintListAgenda(list);
                    } 
                    else
                    {
                        throw new Exception("Informe a opção corretamente");
                        
                    }

                    containsError = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (containsError);
            
        }
    }
}
