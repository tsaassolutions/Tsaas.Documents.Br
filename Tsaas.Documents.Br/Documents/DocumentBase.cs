using System.Text.RegularExpressions;
using Tsaas.Documents.Br.Abstractions;

namespace Tsaas.Documents.Br.Documents
{
    public abstract class DocumentBase : IDocument
    {
        private readonly string _value;
        private string? _unformattedValue;
        private bool? _isValid;

        protected DocumentBase(string value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));
            _value = value;
        }

        public string Value => _value;

        public string UnformattedValue => _unformattedValue ??= Regex.Replace(_value, @"[^\dA-Z]", string.Empty, RegexOptions.IgnoreCase).ToUpperInvariant();

        public abstract string FormattedValue { get; }

        public bool IsValid => _isValid ??= Validate();

        protected abstract bool Validate();
    }
}
