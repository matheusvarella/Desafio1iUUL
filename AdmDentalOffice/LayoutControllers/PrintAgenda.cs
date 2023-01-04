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
            int option;
            do
            {
                Console.WriteLine(
                    "Agenda\r\n" +
                    "1-Agendar consulta\r\n" +
                    "2-Cancelar agendamento\r\n" +
                    "3-Listar agenda\r\n" +
                    "4-Voltar p/ menu principal"
                    );
                option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        Console.Clear();
                        RegisterConsult();
                        Console.WriteLine();
                        break;
                    case 2:
                        Console.Clear();
                        DeleteConsult();
                        Console.WriteLine();
                        break;
                    case 3:
                        Console.Clear();
                        ListAgenda();
                        Console.WriteLine();
                        break;
                    case 4:
                        Console.Clear();
                        Print.PrintStart();
                        Console.WriteLine();
                        break;
                    default:
                        Console.WriteLine("Opção informada inválida! Informe novamente!");
                        break;
                }

            } while (option != 4);
        }

        public static void RegisterConsult()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR", false);
            var erros = new List<string>();
            try
            {
                do
                {
                    Console.Write("CPF: ");
                    var cpf = long.Parse(Console.ReadLine());
                    
                    var erro = PatientValidation.CpfValidation(cpf.ToString());
                    if (erro != null)
                        erros.Add(erro);

                    if (erros.Count == 0)
                    {
                        Console.Write("Data da consulta: ");
                        var consultDate = Console.ReadLine();

                        Console.Write("Hora inicial: ");
                        var intialHour = Console.ReadLine();

                        Console.Write("Hora final: ");
                        var finalHour = Console.ReadLine();

                        var appointment = new Appointment(cpf, consultDate, intialHour, finalHour);
                        
                        erro = ListAppointment.AddAppointment(appointment);
                        if (erro != null)
                            erros.Add(erro);
                        
                        if (erros.Count > 0)
                        {
                            PrintErro.PrintErros(erros);
                        }
                        else
                        {
                            Console.WriteLine("\r\nAgendamento realizado com sucesso!");
                        }
                    }
                    else
                    {
                        PrintErro.PrintErros(erros);
                    }
                } while (erros.Count > 0);
            }
            catch (Exception ex)
            {
                erros.Add(ex.ToString());
                PrintErro.PrintErros(erros);
            }
        }

        public static void DeleteConsult()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR", false);
            var erros = new List<string>();
            try
            {
                do
                {
                    Console.Write("CPF: ");
                    var cpf = long.Parse(Console.ReadLine());

                    var erro = PatientValidation.CpfValidation(cpf.ToString());
                    if (erro != null)
                        erros.Add(erro);

                    if (erros.Count == 0)
                    {
                        Console.Write("Data da consulta: ");
                        var consultDate = Console.ReadLine();

                        Console.Write("Hora inicial: ");
                        var intialHour = Console.ReadLine();

                        var appointment = ListAppointment.GetAppointment(cpf);

                        erro = ListAppointment.RemoveAppointment(appointment);
                        if (erro != null)
                            erros.Add(erro);

                        if (erros.Count > 0)
                        {
                            PrintErro.PrintErros(erros);
                        }
                        else
                        {
                            Console.WriteLine("\r\nAgendamento realizado com sucesso!");
                        }
                    }
                    else
                    {
                        PrintErro.PrintErros(erros);
                    }
                } while (erros.Count > 0);
            }
            catch (Exception ex)
            {
                erros.Add(ex.ToString());
                PrintErro.PrintErros(erros);
            }
        }

        public static void ListAgenda()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR", false);
            var erros = new List<string>();
            try
            {
                do
                {
                    Console.Write("Apresentar a agenda T-Toda ou P-Periodo: ");
                    var option = Console.ReadLine().ToUpper();

                    if (option == "P")
                    {
                        var validator = new AppointmentValidation();

                        Console.Write("Data inicial: ");
                        var initialDate = Console.ReadLine();

                        Console.Write("Data final: ");
                        var finalDate = Console.ReadLine();

                        var erro = validator.StarTimeValidation(initialDate);
                        if (erro != null)
                            erros.Add(erro);
                        
                        erro = validator.EndTimeValidation(finalDate);
                        if (erro != null)
                            erros.Add(erro);
                        
                        if (erros.Count > 0)
                        {
                            var list = ListAppointment.ListAppointmentsByPeriod(initialDate, finalDate);

                            ListAgendaFormatted.PrintListAgenda(list);
                        }
                    } 
                    else if (option == "T") 
                    {
                        var list = ListAppointment.ListAllAppointments();

                        ListAgendaFormatted.PrintListAgenda(list);
                    } 
                    else
                    {
                        erros.Add("Informe a opção corretamente");
                        PrintErro.PrintErros(erros);
                    }

                    if (erros.Count == 0)
                    {
    
                    }
                    else
                    {
                        PrintErro.PrintErros(erros);
                    }
                } while (erros.Count > 0);
            }
            catch (Exception ex)
            {
                erros.Add(ex.ToString());
                PrintErro.PrintErros(erros);
            }
        }
    }
}
