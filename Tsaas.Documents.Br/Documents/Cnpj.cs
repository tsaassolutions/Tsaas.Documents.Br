using Tsaas.Documents.Br.Validation;

namespace Tsaas.Documents.Br.Documents
{
    public class Cnpj : DocumentBase
    {
        private const int CnpjLength = 14;

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
            return CnpjValidator.Validate(UnformattedValue);
        }
    }
}
