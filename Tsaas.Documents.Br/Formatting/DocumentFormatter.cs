namespace Tsaas.Documents.Br.Formatting
{
    internal static class DocumentFormatter
    {
        private const int CpfLength = 11;
        private const int CnpjLength = 14;

        /// <summary>
        /// Formata um CPF no padrão XXX.XXX.XXX-XX
        /// </summary>
        /// <param name="unformattedValue">CPF sem formatação (apenas dígitos)</param>
        /// <returns>CPF formatado ou o valor original se não tiver 11 dígitos</returns>
        public static string FormatCpf(string unformattedValue)
        {
            if (string.IsNullOrWhiteSpace(unformattedValue) || unformattedValue.Length != CpfLength)
                return unformattedValue;

            return $"{unformattedValue[..3]}.{unformattedValue[3..6]}.{unformattedValue[6..9]}-{unformattedValue[9..11]}";
        }

        /// <summary>
        /// Formata um CNPJ no padrão XX.XXX.XXX/XXXX-XX
        /// </summary>
        /// <param name="unformattedValue">CNPJ sem formatação (apenas dígitos)</param>
        /// <returns>CNPJ formatado ou o valor original se não tiver 14 dígitos</returns>
        public static string FormatCnpj(string unformattedValue)
        {
            if (string.IsNullOrWhiteSpace(unformattedValue) || unformattedValue.Length != CnpjLength)
                return unformattedValue;

            return $"{unformattedValue[..2]}.{unformattedValue[2..5]}.{unformattedValue[5..8]}/{unformattedValue[8..12]}-{unformattedValue[12..14]}";
        }
    }
}
