namespace Tsaas.Documents.Br.Abstractions
{
    public interface IDocument
    {
        string Value { get; }
        string UnformattedValue { get; }
        string FormattedValue { get; }
        bool IsValid { get; }
    }
}
