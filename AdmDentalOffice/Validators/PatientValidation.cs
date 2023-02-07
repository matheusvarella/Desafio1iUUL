using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;

namespace AdmDentalOffice.Validators
{
    public static class PatientValidation
    {
        public static void NameValidation(string name)
        {
            if (name.Length < 5 || !new Regex(@"[a-zA-Z]").IsMatch(name))
            {
                throw new Exception("Nome inválido, digite corretamente e com 5 caracteres no minimo");
            }
            
        }

        public static void CpfValidation(string cpf)
        {
            if (!cpf.CPFIsValid())
                throw new Exception("CPF inválido, digite o CPF corretamente");

        }

        public static void BirthDateValidation(string birthDate)
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
                    throw new Exception("Data de nascimento inválida. "+e.Message);
                }

                if (birthDateDateTime.AddYears(13) > DateTime.Now)
                {
                    throw new Exception("Não pode ter menos que 13 anos");
                }
            }
            else
            {
                throw new Exception("Data de Nascimento inválida");
            }
        }

    }

}
