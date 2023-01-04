using AdmDentalOffice.Validators;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;

namespace AdmDentalOffice
{
    public static class PatientValidation
    {
        public static string nameValidation(string name)
        {
            if (name.Length < 5 || !new Regex(@"[a-zA-Z]").IsMatch(name))
            {
                return "Nome inválido, digite corretamente e com 5 caracteres no minimo";
            }
            return null;
        }

        public static string cpfValidation(string cpf)
        {
            if (!cpf.CPFIsValid())
                return "CPF inválido, digite o CPF corretamente";

            return null;

            //if (cpf.Length != 11 || !new Regex(@"[0-9]").IsMatch(cpf))
            //{
            //    return "CPF inválido, digite o CPF corretamente";
            //}
            //else
            //{
            //    int multipleCPF = 0;
            //    int multiplier = 10;
            //    for (int i = 0; i < 9; i++)
            //    {
            //        multipleCPF += int.Parse(cpf.Substring(i, 1)) * multiplier;
            //        multiplier--;
            //    }

            //    int rest = multipleCPF % 11;
            //    string j = cpf.Substring(9, 1);
            //    if (rest < 2)
            //    {
            //        if (j != "0")
            //        {
            //            return "CPF inválido, digite um valor valido";
            //        }
            //    }
            //    else if (rest > 1 && rest < 11)
            //    {
            //        if (j != (11 - rest).ToString())
            //        {
            //            return "CPF inválido, digite um valor valido";
            //        }
            //    }

            //    multipleCPF = 0;
            //    multiplier = 11;
            //    for (int i = 0; i < 10; i++)
            //    {
            //        multipleCPF += int.Parse(cpf.Substring(i, 1)) * multiplier;
            //        multiplier--;
            //    }
            //    rest = multipleCPF % 11;
            //    string k = cpf.Substring(10, 1);
            //    if (rest < 2)
            //    {
            //        if (k != "0")
            //        {
            //            return "CPF inválido, digite um valor valido";
            //        }
            //    }
            //    else if (rest > 1 && rest < 11)
            //    {
            //        if (k != (11 - rest).ToString())
            //        {
            //            return "CPF inválido, digite um valor valido";
            //        }
            //    }

            //}

            //bool equals = true;
            //for (int i = 0; i < cpf.Length; i++)
            //{
            //    if (cpf.Substring(0, 1) != cpf.Substring(i, 1))
            //    {
            //        equals = false;
            //    }
            //}
            //if (equals)
            //{
            //    return "CPf inválido, um CPF não repete os mesmo numeros o tempo todo";
            //}

            //return null;
        }

        public static string birthDateValidation(string birthDate)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR", false);
            if (birthDate.Length == 10)
            {
                DateTime birthDateDateTime;

                try
                {
                    birthDateDateTime = DateTime.Parse(birthDate);
                }
                catch (FormatException e)
                {
                    return "Data de nascimento inválida";
                }

                if (birthDateDateTime.AddYears(13) > DateTime.Now)
                {
                    return "Não pode ter menos que 13 anos";
                }
            }
            else
            {
                return "Data de Nascimento inválida";
            }
            return null;
        }

    }

}
