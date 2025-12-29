using Tsaas.Documents.Br.Documents;

namespace Tsaas.Documents.Br.Tests
{
    public class CnpjValidationTests
    {
        [Theory]
        [InlineData("11.222.333/0001-81", true)]  // 1. CNPJ formatado válido
        [InlineData("11222333000181", true)]      // 2. CNPJ sem formatação válido
        [InlineData("12ABC34501DE35", true)]      // 3. CNPJ alfanumérico válido
        [InlineData("1345C3A5000106", true)]      // 4. CNPJ alfanumérico válido
        [InlineData("90.021.382/0001-22", true)]  // 5. CNPJ formatado válido
        [InlineData("90021382000122", true)]      // 6. CNPJ numérico válido
        [InlineData("90.024.778/0001-23", true)]  // 7. CNPJ formatado válido
        [InlineData("90024778000123", true)]      // 8. CNPJ numérico válido
        [InlineData("00.000.000/0000-00", false)] // 9. CNPJ com todos zeros (inválido)
        [InlineData("00000000000000", false)]     // 10. CNPJ com todos zeros (inválido)
        [InlineData("11.111.111/1111-11", false)] // 11. CNPJ com todos dígitos iguais (inválido)
        [InlineData("11111111111111", false)]     // 12. CNPJ com todos dígitos iguais (inválido)
        [InlineData("22.222.222/2222-22", false)] // 13. CNPJ com todos dígitos iguais (inválido)
        [InlineData("33.333.333/3333-33", false)] // 14. CNPJ com todos dígitos iguais (inválido)
        [InlineData("44.444.444/4444-44", false)] // 15. CNPJ com todos dígitos iguais (inválido)
        [InlineData("55.555.555/5555-55", false)] // 16. CNPJ com todos dígitos iguais (inválido)
        [InlineData("66.666.666/6666-66", false)] // 17. CNPJ com todos dígitos iguais (inválido)
        [InlineData("77.777.777/7777-77", false)] // 18. CNPJ com todos dígitos iguais (inválido)
        [InlineData("88.888.888/8888-88", false)] // 19. CNPJ com todos dígitos iguais (inválido)
        [InlineData("99.999.999/9999-99", false)] // 20. CNPJ com todos dígitos iguais (inválido)
        [InlineData("90.025.108/0001-01", false)] // 21. CNPJ numérico inválido (DV incorreto)
        [InlineData("90025108000101", false)]     // 22. CNPJ numérico inválido (DV incorreto)
        [InlineData("90.025.255/0001-00", false)] // 23. CNPJ numérico inválido (DV incorreto)
        [InlineData("90025255000100", false)]     // 24. CNPJ numérico inválido (DV incorreto)
        [InlineData("R55231B3000700", false)]     // 25. CNPJ alfanumérico inválido
        [InlineData("1345c3A5000106", false)]     // 26. CNPJ com letra minúscula (inválido)
        [InlineData("9002442000010A", false)]     // 27. DV com letra no final (inválido)
        [InlineData("11.222.333/0001", false)]    // 28. CNPJ incompleto (12 dígitos)
        [InlineData("112223330001", false)]       // 29. CNPJ incompleto (12 dígitos)
        [InlineData("11.222.333", false)]         // 30. CNPJ incompleto (8 dígitos)
        [InlineData("11222333", false)]           // 31. CNPJ incompleto (8 dígitos)
        [InlineData("1122233300018", false)]      // 32. CNPJ com 13 dígitos (inválido)
        [InlineData("112223330001811", false)]    // 33. CNPJ com 15 dígitos (inválido)
        [InlineData("11.222.ABC/0001-81", false)] // 34. CNPJ com letras no meio (inválido)
        [InlineData("AB.CDE.FGH/IJKL-MN", false)] // 35. CNPJ totalmente alfabético (inválido)
        [InlineData("1122233300018A", false)]     // 36. CNPJ com letra no final (inválido)
        [InlineData("", false)]                   // 37. CNPJ vazio (inválido)
        [InlineData("   ", false)]                // 38. CNPJ apenas com espaços (inválido)
        [InlineData(null, false)]                 // 39. CNPJ nulo (inválido)
        public void TestarValidacaoCnpj(string cnpj, bool esperadoValido)
        {
            try
            {
                var cnpjDoc = new Cnpj(cnpj);
                Assert.True(esperadoValido, $"CNPJ {cnpj} deveria ser inválido, mas foi aceito.");
            }
            catch
            {
                Assert.False(esperadoValido, $"CNPJ {cnpj} deveria ser válido, mas foi rejeitado.");
            }
        }

        [Theory]
        [InlineData("11.222.333/0001-81", "11222333000181")]
        [InlineData("90.021.382/0001-22", "90021382000122")]
        [InlineData("90.024.778/0001-23", "90024778000123")]
        public void TestarValorNaoFormatado(string cnpjFormatado, string esperadoNaoFormatado)
        {
            var cnpj = new Cnpj(cnpjFormatado);
            Assert.Equal(esperadoNaoFormatado, cnpj.UnformattedValue);
        }

        [Theory]
        [InlineData("11222333000181", "11.222.333/0001-81")]
        [InlineData("90021382000122", "90.021.382/0001-22")]
        [InlineData("90024778000123", "90.024.778/0001-23")]
        public void TestarValorFormatado(string cnpjNaoFormatado, string esperadoFormatado)
        {
            var cnpj = new Cnpj(cnpjNaoFormatado);
            Assert.Equal(esperadoFormatado, cnpj.FormattedValue);
        }

        [Theory]
        [InlineData("11.222.333/0001-81")]
        [InlineData("11222333000181")]
        public void TestarPreservacaoValorOriginal(string valorOriginal)
        {
            var cnpj = new Cnpj(valorOriginal);
            Assert.Equal(valorOriginal, cnpj.Value);
        }

        [Fact]
        public void TestarCacheIsValid()
        {
            var cnpj = new Cnpj("11.222.333/0001-81");
            var isValid1 = cnpj.IsValid;
            var isValid2 = cnpj.IsValid;
            
            Assert.True(isValid1);
            Assert.True(isValid2);
            Assert.Equal(isValid1, isValid2);
        }

        [Fact]
        public void TestarCacheValorNaoFormatado()
        {
            var cnpj = new Cnpj("11.222.333/0001-81");
            var unformatted1 = cnpj.UnformattedValue;
            var unformatted2 = cnpj.UnformattedValue;
            
            Assert.Equal("11222333000181", unformatted1);
            Assert.Equal(unformatted1, unformatted2);
        }

        [Theory]
        [InlineData("11.222.333/0001-81")]
        [InlineData("11222333000181")]
        [InlineData("90.021.382/0001-22")]
        public void TestarPadraoFormatacao(string cnpjEntrada)
        {
            var cnpj = new Cnpj(cnpjEntrada);
            var formatado = cnpj.FormattedValue;
            
            Assert.Matches(@"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$", formatado);
        }

        [Fact]
        public void TestarIndependenciaInstancias()
        {
            var cnpj1 = new Cnpj("11.222.333/0001-81");
            var cnpj2 = new Cnpj("90.021.382/0001-22");
            
            Assert.NotEqual(cnpj1.Value, cnpj2.Value);
            Assert.NotEqual(cnpj1.UnformattedValue, cnpj2.UnformattedValue);
            Assert.NotEqual(cnpj1.FormattedValue, cnpj2.FormattedValue);
            Assert.True(cnpj1.IsValid);
            Assert.True(cnpj2.IsValid);
        }
    }
}
