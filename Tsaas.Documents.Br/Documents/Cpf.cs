using Tsaas.Documents.Br.Formatting;
using Tsaas.Documents.Br.Validation;

namespace Tsaas.Documents.Br.Documents
{
    public class Cpf : DocumentBase
    {
        private const int CpfLength = 11;

        public Cpf(string value) : base(value)
        {
        }

        public override string FormattedValue => DocumentFormatter.FormatCpf(UnformattedValue);

        protected override bool Validate()
        {
            return CpfValidator.Validate(UnformattedValue);
        }
    }
}
