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

                switch (option)
                {
                    case 1:
                        RegisterPatient();
                        break;
                    case 2:
                        DeletePatient();
                        break;
                    case 3:
                        PatientByCPF();
                        break;
                    case 4:
                        
                        break;
                    case 5:
                        
                        break;
                    default:
                        Console.WriteLine("Opção informada inválida! Informe novamente!");
                        break;
                }
            } while (option != 5);
        }

        public static void RegisterPatient()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR", false);
            var erros = new List<string>();
            try
            {
                do
                {
                    Console.Write("CPF: ");
                    var cpf = long.Parse(Console.ReadLine());

                    Console.Write("Nome: ");
                    var name = Console.ReadLine();

                    Console.Write("Data de nascimento: ");
                    var birthDate = Console.ReadLine();

                    var erro = PatientValidation.cpfValidation(cpf.ToString());
                    if (erro != null)
                        erros.Add(erro);

                    erro = PatientValidation.nameValidation(name);
                    if (erro != null)
                        erros.Add(erro);

                    erro = PatientValidation.birthDateValidation(birthDate);
                    if (erro != null)
                        erros.Add(erro);

                    if (erros.Count > 0)
                    {
                        PrintErro.PrintErros(erros);
                    }
                    else
                    {
                        var patient = new Patient(name, cpf, DateTime.Parse(birthDate));

                        erro = ListPatient.addPatient(patient);
                        if (erro != null)
                            erros.Add(erro);

                        if (erros.Count > 0)
                        {
                            PrintErro.PrintErros(erros);
                        }
                        else
                        {
                            Console.WriteLine("\r\nCadastrado com sucesso!");
                        }
                        Console.Clear();
                    }
                } while (erros.Count > 0);
            }
            catch (Exception ex)
            {
                erros.Add(ex.ToString());
                PrintErro.PrintErros(erros);
            }
        }

        public static void DeletePatient()
        {
            var erros = new List<string>();

            do
            {
                Console.Write("CPF: ");
                var cpf = Console.ReadLine();

                var erro = PatientValidation.cpfValidation(cpf);
                if (erro != null)
                    erros.Add(erro);

                if (erros.Count > 0)
                {
                    PrintErro.PrintErros(erros);
                }
                else
                {
                    erro = ListPatient.removePatient(long.Parse(cpf));
                    if (erro != null)
                        erros.Add(erro);

                    if (erros.Count > 0)
                    {
                        PrintErro.PrintErros(erros);
                    }
                    else
                    {
                        Console.WriteLine("\r\nPaciente excluido com sucesso!");
                    }
                }

            } while (erros.Count > 0);
        }

        public static void PatientByCPF()
        {
            var patients = ListPatient.listPatientsByCpf();
            
            foreach (var patient in patients)
            {
                Console.WriteLine(
                                patient.Key.Cpf + " " +
                                patient.Key.Name + " " +
                                patient.Key.BirthDate + " " +
                                patient.Value.ConsultationDate + " " +
                                patient.Value.StartTime);
            }
        }

        public static void PatientByName()
        {
            var patients = ListPatient.listPatientsByName();

            foreach (var patient in patients)
            {
                Console.WriteLine(
                                patient.Key.Cpf + " " +
                                patient.Key.Name + " " +
                                patient.Key.BirthDate + " " +
                                patient.Value.ConsultationDate + " " +
                                patient.Value.StartTime);
            }
        }
    }
}