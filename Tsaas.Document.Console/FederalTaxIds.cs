using Tsaas.Documents.Br.Documents;

namespace Tsaas.Document.Console
{
    public class FederalTaxIds
    {
        public FederalTaxIds(string? cnpj, string? cpf)
        {
            Cnpj = new Cnpj(cnpj!);
            Cpf = new Cpf(cpf!);
        }

        public Cnpj Cnpj { get; set; }
        public Cpf Cpf { get; set; }
    }
}
