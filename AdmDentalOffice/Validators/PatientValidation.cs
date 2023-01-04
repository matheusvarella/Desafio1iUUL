using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;

namespace AdmDentalOffice.Validators
{
    public static class PatientValidation
    {
        public static string NameValidation(string name)
        {
            if (name.Length < 5 || !new Regex(@"[a-zA-Z]").IsMatch(name))
            {
                return "Nome inválido, digite corretamente e com 5 caracteres no minimo";
            }
            return null;
        }

        public static string CpfValidation(string cpf)
        {
            if (!cpf.CPFIsValid())
                return "CPF inválido, digite o CPF corretamente";

            return null;
        }

        public static string BirthDateValidation(string birthDate)
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
