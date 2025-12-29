using Tsaas.Documents.Br.Exceptions;
using Tsaas.Documents.Br.Formatting;
using Tsaas.Documents.Br.Validation;

namespace Tsaas.Documents.Br.Documents
{
    /// <summary>
    /// Representa um CNPJ (Cadastro Nacional de Pessoa Jurídica) como Value Object.
    /// </summary>
    public class Cnpj : DocumentBase, IEquatable<Cnpj>
    {
        private const int CnpjLength = 14;

        /// <summary>
        /// Inicializa uma nova instância de CNPJ com validação automática.
        /// </summary>
        /// <param name="value">Valor do CNPJ formatado ou não formatado</param>
        /// <exception cref="InvalidDocumentException">Lançada quando o CNPJ é inválido</exception>
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

        /// <summary>
        /// Tenta criar um CNPJ a partir de uma string. Retorna null se o valor for inválido.
        /// </summary>
        /// <param name="value">Valor do CNPJ formatado ou não formatado</param>
        /// <returns>Instância de Cnpj ou null se inválido</returns>
        public static Cnpj? TryCreate(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            try
            {
                return new Cnpj(value);
            }
            catch (InvalidDocumentException)
            {
                return null;
            }
        }

        /// <summary>
        /// Tenta criar um CNPJ a partir de uma string.
        /// </summary>
        /// <param name="value">Valor do CNPJ formatado ou não formatado</param>
        /// <param name="cnpj">CNPJ criado se válido</param>
        /// <returns>True se o CNPJ foi criado com sucesso, false caso contrário</returns>
        public static bool TryParse(string? value, out Cnpj? cnpj)
        {
            cnpj = TryCreate(value);
            return cnpj != null;
        }

        #region Value Object Pattern

        public bool Equals(Cnpj? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return UnformattedValue == other.UnformattedValue;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Cnpj);
        }

        public override int GetHashCode()
        {
            return UnformattedValue.GetHashCode();
        }

        public static bool operator ==(Cnpj? left, Cnpj? right)
        {
            if (left is null) return right is null;
            return left.Equals(right);
        }

        public static bool operator !=(Cnpj? left, Cnpj? right)
        {
            return !(left == right);
        }

        public override string ToString() => FormattedValue;

        #endregion

        #region Conversões Implícitas

        /// <summary>
        /// Conversão implícita de Cnpj para string (retorna o valor formatado).
        /// </summary>
        public static implicit operator string(Cnpj? cnpj) => cnpj?.FormattedValue ?? string.Empty;

        #endregion
    }
}
