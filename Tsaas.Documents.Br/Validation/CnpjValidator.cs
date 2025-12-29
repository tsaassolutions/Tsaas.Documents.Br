namespace Tsaas.Documents.Br.Validation
{
    internal static class CnpjValidator
    {
        private const int CnpjLength = 14;
        private static readonly int[] FirstDigitWeights = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        private static readonly int[] SecondDigitWeights = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        public static bool Validate(string unformattedValue)
        {
            if (string.IsNullOrWhiteSpace(unformattedValue))
                return false;

            if (unformattedValue.Length != CnpjLength)
                return false;

            if (!unformattedValue.All(char.IsDigit))
                return false;

            // Rejeita CNPJs com todos os dígitos iguais
            if (unformattedValue.Distinct().Count() == 1)
                return false;

            // Validação do primeiro dígito verificador
            var sum = 0;
            for (int i = 0; i < 12; i++)
                sum += (unformattedValue[i] - '0') * FirstDigitWeights[i];

            var firstDigit = sum % 11 < 2 ? 0 : 11 - (sum % 11);
            if (firstDigit != (unformattedValue[12] - '0'))
                return false;

            // Validação do segundo dígito verificador
            sum = 0;
            for (int i = 0; i < 13; i++)
                sum += (unformattedValue[i] - '0') * SecondDigitWeights[i];

            var secondDigit = sum % 11 < 2 ? 0 : 11 - (sum % 11);
            return secondDigit == (unformattedValue[13] - '0');
        }
    }
}
