namespace Tsaas.Documents.Br.Documents
{
    public class Cpf : DocumentBase
    {
        private const int CpfLength = 11;

        public Cpf(string value) : base(value)
        {
        }

        public override string FormattedValue
        {
            get
            {
                if (UnformattedValue.Length != CpfLength)
                    return UnformattedValue;

                return $"{UnformattedValue[..3]}.{UnformattedValue[3..6]}.{UnformattedValue[6..9]}-{UnformattedValue[9..11]}";
            }
        }

        protected override bool Validate()
        {
            if (UnformattedValue.Length != CpfLength)
                return false;

            if (!UnformattedValue.All(char.IsDigit))
                return false;

            // Rejeita CPFs com todos os dígitos iguais
            if (UnformattedValue.Distinct().Count() == 1)
                return false;

            // Validação do primeiro dígito verificador
            var sum = 0;
            for (int i = 0; i < 9; i++)
                sum += (UnformattedValue[i] - '0') * (10 - i);

            var firstDigit = sum % 11 < 2 ? 0 : 11 - (sum % 11);
            if (firstDigit != (UnformattedValue[9] - '0'))
                return false;

            // Validação do segundo dígito verificador
            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += (UnformattedValue[i] - '0') * (11 - i);

            var secondDigit = sum % 11 < 2 ? 0 : 11 - (sum % 11);
            return secondDigit == (UnformattedValue[10] - '0');
        }
    }
}
