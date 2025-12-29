namespace Tsaas.Documents.Br.Documents
{
    public class Cnpj : DocumentBase
    {
        private const int CnpjLength = 14;
        private static readonly int[] FirstDigitWeights = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        private static readonly int[] SecondDigitWeights = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        public Cnpj(string value) : base(value)
        {
        }

        public override string FormattedValue
        {
            get
            {
                if (UnformattedValue.Length != CnpjLength)
                    return UnformattedValue;

                return $"{UnformattedValue[..2]}.{UnformattedValue[2..5]}.{UnformattedValue[5..8]}/{UnformattedValue[8..12]}-{UnformattedValue[12..14]}";
            }
        }

        protected override bool Validate()
        {
            if (UnformattedValue.Length != CnpjLength)
                return false;

            if (!UnformattedValue.All(char.IsDigit))
                return false;

            // Rejeita CNPJs com todos os dígitos iguais
            if (UnformattedValue.Distinct().Count() == 1)
                return false;

            // Validação do primeiro dígito verificador
            var sum = 0;
            for (int i = 0; i < 12; i++)
                sum += (UnformattedValue[i] - '0') * FirstDigitWeights[i];

            var firstDigit = sum % 11 < 2 ? 0 : 11 - (sum % 11);
            if (firstDigit != (UnformattedValue[12] - '0'))
                return false;

            // Validação do segundo dígito verificador
            sum = 0;
            for (int i = 0; i < 13; i++)
                sum += (UnformattedValue[i] - '0') * SecondDigitWeights[i];

            var secondDigit = sum % 11 < 2 ? 0 : 11 - (sum % 11);
            return secondDigit == (UnformattedValue[13] - '0');
        }
    }
}
