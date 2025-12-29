using Tsaas.Documents.Br.Documents;

namespace Tsaas.Documents.Br.Tests
{
    public class CpfValidationTests
    {
        [Theory]
        [InlineData("123.456.789-09", true)]  // 1. CPF formatado válido
        [InlineData("12345678909", true)]     // 2. CPF sem formatação válido
        [InlineData("111.444.777-35", true)]  // 3. CPF formatado válido
        [InlineData("11144477735", true)]     // 4. CPF sem formatação válido
        [InlineData("000.000.001-91", true)]  // 5. CPF formatado válido
        [InlineData("00000000191", true)]     // 6. CPF sem formatação válido
        [InlineData("987.654.321-00", true)]  // 7. CPF formatado válido
        [InlineData("111.111.111-11", false)] // 8. CPF com todos dígitos iguais (inválido)
        [InlineData("000.000.000-00", false)] // 9. CPF com todos zeros (inválido)
        [InlineData("222.222.222-22", false)] // 10. CPF com todos dígitos iguais (inválido)
        [InlineData("333.333.333-33", false)] // 11. CPF com todos dígitos iguais (inválido)
        [InlineData("444.444.444-44", false)] // 12. CPF com todos dígitos iguais (inválido)
        [InlineData("555.555.555-55", false)] // 13. CPF com todos dígitos iguais (inválido)
        [InlineData("666.666.666-66", false)] // 14. CPF com todos dígitos iguais (inválido)
        [InlineData("777.777.777-77", false)] // 15. CPF com todos dígitos iguais (inválido)
        [InlineData("888.888.888-88", false)] // 16. CPF com todos dígitos iguais (inválido)
        [InlineData("999.999.999-99", false)] // 17. CPF com todos dígitos iguais (inválido)
        [InlineData("123.456.789-00", false)] // 18. CPF com dígito verificador inválido
        [InlineData("123.456.789-99", false)] // 19. CPF com dígito verificador inválido
        [InlineData("111.444.777-00", false)] // 20. CPF com dígito verificador inválido
        [InlineData("000.000.001-00", false)] // 21. CPF com dígito verificador inválido
        [InlineData("123.456.789-10", false)] // 22. CPF com dígito verificador inválido
        [InlineData("123.456.789", false)]    // 23. CPF incompleto (9 dígitos)
        [InlineData("123456789", false)]      // 24. CPF incompleto (9 dígitos)
        [InlineData("12345678", false)]       // 25. CPF incompleto (8 dígitos)
        [InlineData("1234567890", false)]     // 26. CPF com 10 dígitos (inválido)
        [InlineData("123456789012", false)]   // 27. CPF com 12 dígitos (inválido)
        [InlineData("123.456.ABC-09", false)] // 28. CPF com letras no meio (inválido)
        [InlineData("ABC.DEF.GHI-JK", false)] // 29. CPF totalmente alfabético (inválido)
        [InlineData("12345678ABC", false)]    // 30. CPF com letras no final (inválido)
        [InlineData("", false)]               // 31. CPF vazio (inválido)
        [InlineData("   ", false)]            // 32. CPF apenas com espaços (inválido)
        [InlineData(null, false)]             // 33. CPF nulo (inválido)
        public void TestarValidacaoCpf(string cpf, bool esperadoValido)
        {
            try
            {
                var cpfDoc = new Cpf(cpf);
                Assert.True(esperadoValido, $"CPF {cpf} deveria ser inválido, mas foi aceito.");
            }
            catch
            {
                Assert.False(esperadoValido, $"CPF {cpf} deveria ser válido, mas foi rejeitado.");
            }
        }

        [Theory]
        [InlineData("123.456.789-09", "12345678909")]
        [InlineData("111.444.777-35", "11144477735")]
        [InlineData("000.000.001-91", "00000000191")]
        public void TestarValorNaoFormatado(string cpfFormatado, string esperadoNaoFormatado)
        {
            var cpf = new Cpf(cpfFormatado);
            Assert.Equal(esperadoNaoFormatado, cpf.UnformattedValue);
        }

        [Theory]
        [InlineData("12345678909", "123.456.789-09")]
        [InlineData("11144477735", "111.444.777-35")]
        [InlineData("00000000191", "000.000.001-91")]
        public void TestarValorFormatado(string cpfNaoFormatado, string esperadoFormatado)
        {
            var cpf = new Cpf(cpfNaoFormatado);
            Assert.Equal(esperadoFormatado, cpf.FormattedValue);
        }

        [Theory]
        [InlineData("123.456.789-09")]
        [InlineData("12345678909")]
        public void TestarPreservacaoValorOriginal(string valorOriginal)
        {
            var cpf = new Cpf(valorOriginal);
            Assert.Equal(valorOriginal, cpf.Value);
        }

        [Fact]
        public void TestarCacheIsValid()
        {
            var cpf = new Cpf("123.456.789-09");
            var isValid1 = cpf.IsValid;
            var isValid2 = cpf.IsValid;
            
            Assert.True(isValid1);
            Assert.True(isValid2);
            Assert.Equal(isValid1, isValid2);
        }

        [Fact]
        public void TestarCacheValorNaoFormatado()
        {
            var cpf = new Cpf("123.456.789-09");
            var unformatted1 = cpf.UnformattedValue;
            var unformatted2 = cpf.UnformattedValue;
            
            Assert.Equal("12345678909", unformatted1);
            Assert.Equal(unformatted1, unformatted2);
        }

        [Theory]
        [InlineData("123.456.789-09")]
        [InlineData("12345678909")]
        [InlineData("111.444.777-35")]
        public void TestarPadraoFormatacao(string cpfEntrada)
        {
            var cpf = new Cpf(cpfEntrada);
            var formatado = cpf.FormattedValue;
            
            Assert.Matches(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", formatado);
        }

        [Fact]
        public void TestarIndependenciaInstancias()
        {
            var cpf1 = new Cpf("123.456.789-09");
            var cpf2 = new Cpf("111.444.777-35");
            
            Assert.NotEqual(cpf1.Value, cpf2.Value);
            Assert.NotEqual(cpf1.UnformattedValue, cpf2.UnformattedValue);
            Assert.NotEqual(cpf1.FormattedValue, cpf2.FormattedValue);
            Assert.True(cpf1.IsValid);
            Assert.True(cpf2.IsValid);
        }
    }
}
