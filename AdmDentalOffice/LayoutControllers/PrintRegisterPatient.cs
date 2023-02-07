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
    public static class PrintRegisterPatient
    {
        public static void Register()
        {
            int option;
            do
            {
                Console.WriteLine(
                    "Menu do Cadastro de Pacientes\r\n" +
                    "1-Cadastrar novo paciente\r\n" +
                    "2-Excluir paciente\r\n" +
                    "3-Listar pacientes (ordenado por CPF)\r\n" +
                    "4-Listar pacientes (ordenado por nome)\r\n" +
                    "5-Voltar p/ menu principal"
                    );
                option = int.Parse(Console.ReadLine());

                if (option == 1)
                {
                    Console.Clear();
                    RegisterPatient();
                    Console.WriteLine();
                }
                else if(option == 2)
                {
                    Console.Clear();
                    DeletePatient();
                    Console.WriteLine();
                }
                else if(option == 3)
                {
                    Console.Clear();
                    PatientByCPF();
                    Console.WriteLine();
                }
                else if(option == 4)
                {
                    Console.Clear();
                    PatientByName();
                    Console.WriteLine();
                }
                else if (option == 5)
                {
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Opção informada inválida! Informe novamente!");
                }
               /* switch (option)
                {
                    case 1:
                        Console.Clear();
                        RegisterPatient();
                        Console.WriteLine();
                        break;
                    case 2:
                        Console.Clear();
                        DeletePatient();
                        Console.WriteLine();
                        break;
                    case 3:
                        Console.Clear();
                        PatientByCPF();
                        Console.WriteLine();
                        break;
                    case 4:
                        Console.Clear();
                        PatientByName();
                        Console.WriteLine();
                        break;
                    case 5:
                        Console.Clear();
                        Print.PrintStart();
                        break;
                    default:
                        Console.WriteLine("Opção informada inválida! Informe novamente!");
                        break;
                }*/
            } while (option == 5);
        }

        public static void RegisterPatient()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR", false);
            bool containsError = true;
            
            do
            {
                try
                {
                    Console.Write("CPF: ");
                    var cpf = long.Parse(Console.ReadLine());

                    Console.Write("Nome: ");
                    var name = Console.ReadLine();

                    Console.Write("Data de nascimento: ");
                    var birthDate = Console.ReadLine();
                    
                    PatientValidation.CpfValidation(cpf.ToString());
                    
                    PatientValidation.NameValidation(name);
                    
                    PatientValidation.BirthDateValidation(birthDate);
                    
                    Patient patient = new Patient(name, cpf, DateTime.Parse(birthDate));

                    ListPatient.AddPatient(patient);
                    
                    Console.WriteLine("\r\nCadastrado com sucesso!");
                    
                    containsError = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (containsError);
            
        }

        public static void DeletePatient()
        {
            bool containsError = true;
            string error = null;
            do
            {
                Console.Write("CPF: ");
                try
                {
                    var cpf = Console.ReadLine();
                    
                    PatientValidation.CpfValidation(cpf);
                    
                    ListPatient.RemovePatient(long.Parse(cpf));
                    
                    Console.WriteLine("\r\nPaciente excluido com sucesso!");
                    
                    containsError = false;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message); 
                }
                

            } while (containsError);
        }

        public static void PatientByCPF()
        {
            var patients = ListPatient.ListPatientsByCpf();

            ListPatientFormatted.PrintListPatient(patients);
        }

        public static void PatientByName()
        {
            var patients = ListPatient.ListPatientsByName();

            ListPatientFormatted.PrintListPatient(patients);
        }
    }
}