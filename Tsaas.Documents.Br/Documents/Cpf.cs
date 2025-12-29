using Tsaas.Documents.Br.Validation;

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
            return CpfValidator.Validate(UnformattedValue);
        }
    }
}
