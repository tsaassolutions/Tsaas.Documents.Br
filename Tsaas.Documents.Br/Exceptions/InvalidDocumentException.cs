namespace Tsaas.Documents.Br.Exceptions
{
    public class InvalidDocumentException : Exception
    {
        public InvalidDocumentException()
            : base("O documento fornecido é inválido.")
        {
        }

        public InvalidDocumentException(string message)
            : base(message)
        {
        }

        public InvalidDocumentException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public InvalidDocumentException(string documentType, string value)
            : base($"O documento {documentType} '{value}' é inválido.")
        {
            DocumentType = documentType;
            DocumentValue = value;
        }

        public string? DocumentType { get; }
        public string? DocumentValue { get; }
    }
}
