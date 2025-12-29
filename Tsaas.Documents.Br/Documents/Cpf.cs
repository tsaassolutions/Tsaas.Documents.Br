using Tsaas.Documents.Br.Exceptions;
using Tsaas.Documents.Br.Formatting;
using Tsaas.Documents.Br.Validation;

namespace Tsaas.Documents.Br.Documents
{
    /// <summary>
    /// Representa um CPF (Cadastro de Pessoa Física) como Value Object.
    /// </summary>
    public class Cpf : DocumentBase, IEquatable<Cpf>
    {
        private const int CpfLength = 11;

        /// <summary>
        /// Inicializa uma nova instância de CPF com validação automática.
        /// </summary>
        /// <param name="value">Valor do CPF formatado ou não formatado</param>
        /// <exception cref="InvalidDocumentException">Lançada quando o CPF é inválido</exception>
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

        /// <summary>
        /// Tenta criar um CPF a partir de uma string. Retorna null se o valor for inválido.
        /// </summary>
        /// <param name="value">Valor do CPF formatado ou não formatado</param>
        /// <returns>Instância de Cpf ou null se inválido</returns>
        public static Cpf? TryCreate(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            try
            {
                return new Cpf(value);
            }
            catch (InvalidDocumentException)
            {
                return null;
            }
        }

        /// <summary>
        /// Tenta criar um CPF a partir de uma string.
        /// </summary>
        /// <param name="value">Valor do CPF formatado ou não formatado</param>
        /// <param name="cpf">CPF criado se válido</param>
        /// <returns>True se o CPF foi criado com sucesso, false caso contrário</returns>
        public static bool TryParse(string? value, out Cpf? cpf)
        {
            cpf = TryCreate(value);
            return cpf != null;
        }

        #region Value Object Pattern

        public bool Equals(Cpf? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return UnformattedValue == other.UnformattedValue;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Cpf);
        }

        public override int GetHashCode()
        {
            return UnformattedValue.GetHashCode();
        }

        public static bool operator ==(Cpf? left, Cpf? right)
        {
            if (left is null) return right is null;
            return left.Equals(right);
        }

        public static bool operator !=(Cpf? left, Cpf? right)
        {
            return !(left == right);
        }

        public override string ToString() => FormattedValue;

        #endregion

        #region Conversões Implícitas

        /// <summary>
        /// Conversão implícita de Cpf para string (retorna o valor formatado).
        /// </summary>
        public static implicit operator string(Cpf? cpf) => cpf?.FormattedValue ?? string.Empty;

        #endregion
    }
}
