namespace AdmDentalOffice.Validators
{
    public static class CPFValidator
    {
        public static bool CPFIsValid(this string cpf)
        {
            cpf = cpf.Trim().Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            var multiplicador1 = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new int[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            var temporaria = cpf.Substring(0, 9);
            var digito = "";
            var soma = 0;
            var resto = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(temporaria[i].ToString()) * multiplicador1[i];

            resto = soma % 11;

            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito = resto.ToString();

            temporaria = temporaria + digito;

            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(temporaria[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }
    }
}
