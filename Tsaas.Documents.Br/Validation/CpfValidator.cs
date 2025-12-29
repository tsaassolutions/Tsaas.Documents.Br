namespace Tsaas.Documents.Br.Validation
{
    internal static class CpfValidator
    {
        private const int CpfLength = 11;

        public static bool Validate(string unformattedValue)
        {
            if (string.IsNullOrWhiteSpace(unformattedValue))
                return false;

            if (unformattedValue.Length != CpfLength)
                return false;

            if (!unformattedValue.All(char.IsDigit))
                return false;

            // Rejeita CPFs com todos os dígitos iguais
            if (unformattedValue.Distinct().Count() == 1)
                return false;

            // Validação do primeiro dígito verificador
            var sum = 0;
            for (int i = 0; i < 9; i++)
                sum += (unformattedValue[i] - '0') * (10 - i);

            var firstDigit = sum % 11 < 2 ? 0 : 11 - (sum % 11);
            if (firstDigit != (unformattedValue[9] - '0'))
                return false;

            // Validação do segundo dígito verificador
            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += (unformattedValue[i] - '0') * (11 - i);

            var secondDigit = sum % 11 < 2 ? 0 : 11 - (sum % 11);
            return secondDigit == (unformattedValue[10] - '0');
        }
    }
}
