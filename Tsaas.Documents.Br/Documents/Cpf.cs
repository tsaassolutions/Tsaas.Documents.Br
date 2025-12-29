using Tsaas.Documents.Br.Exceptions;
using Tsaas.Documents.Br.Formatting;
using Tsaas.Documents.Br.Validation;

namespace Tsaas.Documents.Br.Documents
{
    public class Cpf : DocumentBase
    {
        private const int CpfLength = 11;

        public Cpf(string value) : base(value)
        {
            if (UnformattedValue.Length != CpfLength)
            {
                throw new InvalidDocumentException("CPF", value);
            }

            if (!CpfValidator.Validate(UnformattedValue))
            {
                throw new InvalidDocumentException("CPF", value);
            }
        }

        public override string FormattedValue => DocumentFormatter.FormatCpf(UnformattedValue);

        protected override bool Validate()
        {
            return CpfValidator.Validate(UnformattedValue);
        }
    }
}
