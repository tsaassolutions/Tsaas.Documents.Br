using Tsaas.Documents.Br.Documents;

namespace Tsaas.Documents.Br.Tests
{
    public class CnpjValidationTests
    {
        [Theory]
        [InlineData("12ABC34501DE35", true)]  // 1. CNPJ alfanumérico válido
        [InlineData("1345C3A5000106", true)]  // 2. CNPJ alfanumérico válido
        [InlineData("R55231B3000700", false)] // 3. CNPJ alfanumérico inválido
        [InlineData("1345c3A5000106", false)] // 4. CNPJ com letra minúscula (inválido)
        [InlineData("90021382000122", true)]  // 5. CNPJ numérico válido: 90.021.382/0001-22
        [InlineData("90024778000123", true)]  // 6. CNPJ numérico válido: 90.024.778/0001-23
        [InlineData("90025108000101", false)] // 7. CNPJ numérico inválido: 90.025.108/0001-01
        [InlineData("90025255000100", false)] // 8. CNPJ numérico inválido: 90.025.255/0001-00
        [InlineData("9002442000010A", false)] // 9. DV com letra no final (inválido)
        public void TestCnpjValidation(string cnpj, bool expectedValid)
        {
            try
            {
                var cnpjDoc = new Cnpj(cnpj);
                Assert.True(expectedValid, $"CNPJ {cnpj} deveria ser inválido, mas foi aceito.");
            }
            catch
            {
                Assert.False(expectedValid, $"CNPJ {cnpj} deveria ser válido, mas foi rejeitado.");
            }
        }
    }
}
