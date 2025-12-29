using Tsaas.Documents.Br.Exceptions;
using Tsaas.Documents.Br.Formatting;
using Tsaas.Documents.Br.Validation;

namespace Tsaas.Documents.Br.Documents
{
    public class Cnpj : DocumentBase
    {
        private const int CnpjLength = 14;

        public Cnpj(string value) : base(value)
        {
            if (UnformattedValue.Length != CnpjLength)
            {
                throw new InvalidDocumentException("CNPJ", value);
            }

            if (!CnpjValidator.Validate(UnformattedValue))
            {
                throw new InvalidDocumentException("CNPJ", value);
            }
        }

        public override string FormattedValue => DocumentFormatter.FormatCnpj(UnformattedValue);

        protected override bool Validate()
        {
            return CnpjValidator.Validate(UnformattedValue);
        }
    }
}
