using System.Text.RegularExpressions;

namespace Tsaas.Documents.Br.Validation
{
    internal static class CnpjValidator
    {
        private const int CnpjLength = 14;
        private const int CnpjBaseLength = 12;
        private const int ValueBase = '0';
        private static readonly int[] Weights = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        
        // Os primeiros 12 caracteres podem ser dígitos ou letras maiúsculas
        private static readonly Regex BaseRegex = new Regex(@"^[A-Z\d]{12}$", RegexOptions.Compiled);
        // Os últimos 2 caracteres devem ser dígitos
        private static readonly Regex VerificationDigitsRegex = new Regex(@"^\d{2}$", RegexOptions.Compiled);
        // Rejeita valores zerados
        private static readonly Regex AllZerosRegex = new Regex(@"^0+$", RegexOptions.Compiled);

        public static bool Validate(string unformattedValue)
        {
            if (string.IsNullOrWhiteSpace(unformattedValue))
                return false;

            if (unformattedValue.Length != CnpjLength)
                return false;

            var baseCnpj = unformattedValue.Substring(0, CnpjBaseLength);
            var verificationDigits = unformattedValue.Substring(CnpjBaseLength);

            // Valida formato: primeiros 12 devem ser A-Z ou 0-9, últimos 2 devem ser 0-9
            if (!BaseRegex.IsMatch(baseCnpj) || !VerificationDigitsRegex.IsMatch(verificationDigits))
                return false;

            // Rejeita CNPJs com todos os caracteres zerados
            if (AllZerosRegex.IsMatch(unformattedValue))
                return false;

            // Calcula os dígitos verificadores
            var calculatedDv = CalculateVerificationDigits(baseCnpj);
            
            return calculatedDv == verificationDigits;
        }

        private static string CalculateVerificationDigits(string baseCnpj)
        {
            var firstDigit = CalculateDigit(baseCnpj);
            var secondDigit = CalculateDigit(baseCnpj + firstDigit);
            
            return $"{firstDigit}{secondDigit}";
        }

        private static int CalculateDigit(string cnpj)
        {
            var sum = 0;
            for (int index = cnpj.Length - 1; index >= 0; index--)
            {
                var charValue = cnpj[index] - ValueBase;
                sum += charValue * Weights[Weights.Length - cnpj.Length + index];
            }
            
            return sum % 11 < 2 ? 0 : 11 - (sum % 11);
        }
    }
}
