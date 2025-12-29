using Tsaas.Documents.Br.Formatting;
using Tsaas.Documents.Br.Validation;

namespace Tsaas.Documents.Br.Documents
{
    public class Cnpj : DocumentBase
    {
        private const int CnpjLength = 14;

        public Cnpj(string value) : base(value)
        {
        }

        public override string FormattedValue => DocumentFormatter.FormatCnpj(UnformattedValue);

        protected override bool Validate()
        {
            return CnpjValidator.Validate(UnformattedValue);
        }
    }
}
