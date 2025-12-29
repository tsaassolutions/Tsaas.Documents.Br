namespace Tsaas.Documents.Br.Documents
{
    public class Cnpj : DocumentBase
    {
        private const int CnpjLength = 14;
        private const int CnpjLengthWithoutDv = 12;
        private const int ValueBase = (int)'0'; // ASCII value = 48
        
        // Pesos para cálculo dos dígitos verificadores (usado para ambos DV1 e DV2)
        private static readonly int[] DvWeights = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        public Cnpj(string value) : base(value)
        {
        }

        public override string FormattedValue
        {
            get
            {
                if (UnformattedValue.Length != CnpjLength)
                    return UnformattedValue;

                return $"{UnformattedValue[..2]}.{UnformattedValue[2..5]}.{UnformattedValue[5..8]}/{UnformattedValue[8..12]}-{UnformattedValue[12..14]}";
            }
        }

        protected override bool Validate()
        {
            if (UnformattedValue.Length != CnpjLength)
                return false;

            // Formato: 12 caracteres alfanuméricos (A-Z ou 0-9) + 2 dígitos verificadores (0-9)
            if (!IsValidFormat())
                return false;

            // Rejeita CNPJs zerados (todos zeros)
            if (UnformattedValue.All(c => c == '0'))
                return false;

            // Valida os dígitos verificadores
            string dvInformado = UnformattedValue.Substring(CnpjLengthWithoutDv, 2);
            string dvCalculado = CalculateDv(UnformattedValue.Substring(0, CnpjLengthWithoutDv));
            
            return dvCalculado == dvInformado;
        }
        
        private bool IsValidFormat()
        {
            // Primeiros 12 caracteres devem ser A-Z ou 0-9
            for (int i = 0; i < CnpjLengthWithoutDv; i++)
            {
                char c = UnformattedValue[i];
                if (!char.IsLetterOrDigit(c))
                    return false;
            }
            
            // Últimos 2 caracteres devem ser dígitos (0-9)
            for (int i = CnpjLengthWithoutDv; i < CnpjLength; i++)
            {
                if (!char.IsDigit(UnformattedValue[i]))
                    return false;
            }
            
            return true;
        }
        
        /// <summary>
        /// Calcula os dígitos verificadores (DV) para a base do CNPJ (12 caracteres)
        /// </summary>
        /// <param name="baseCnpj">Base do CNPJ com 12 caracteres alfanuméricos</param>
        /// <returns>Os 2 dígitos verificadores calculados</returns>
        /// <exception cref="ArgumentException">Lançada quando a base não tem 12 caracteres ou contém caracteres inválidos</exception>
        public static string CalculateDv(string baseCnpj)
        {
            if (string.IsNullOrWhiteSpace(baseCnpj))
                throw new ArgumentException("A base do CNPJ não pode ser nula ou vazia", nameof(baseCnpj));
            
            // Remove caracteres de formatação
            baseCnpj = System.Text.RegularExpressions.Regex.Replace(baseCnpj.Trim(), @"[.\-/]", string.Empty).ToUpperInvariant();
            
            if (baseCnpj.Length != CnpjLengthWithoutDv)
                throw new ArgumentException($"A base do CNPJ deve ter {CnpjLengthWithoutDv} caracteres", nameof(baseCnpj));
            
            // Valida se contém apenas caracteres alfanuméricos
            if (!baseCnpj.All(char.IsLetterOrDigit))
                throw new ArgumentException("A base do CNPJ contém caracteres inválidos", nameof(baseCnpj));
            
            // Rejeita base zerada
            if (baseCnpj.All(c => c == '0'))
                throw new ArgumentException("A base do CNPJ não pode ser zerada", nameof(baseCnpj));
            
            int dv1 = CalculateDigit(baseCnpj);
            int dv2 = CalculateDigit(baseCnpj + dv1.ToString());
            
            return $"{dv1}{dv2}";
        }
        
        /// <summary>
        /// Calcula um dígito verificador usando o algoritmo oficial
        /// Itera de trás para frente aplicando os pesos
        /// </summary>
        private static int CalculateDigit(string cnpj)
        {
            int soma = 0;
            
            // Itera de trás para frente (do final para o início)
            for (int indice = cnpj.Length - 1; indice >= 0; indice--)
            {
                // Converte caractere para valor: '0'=0, '1'=1, ..., 'A'=17, 'B'=18, etc.
                int valorCaracter = (int)cnpj[indice] - ValueBase;
                
                // Calcula o índice do peso: PESOS_DV.Length - cnpj.Length + indice
                int pesoIndice = DvWeights.Length - cnpj.Length + indice;
                
                soma += valorCaracter * DvWeights[pesoIndice];
            }
            
            return soma % 11 < 2 ? 0 : 11 - (soma % 11);
        }
    }
}
