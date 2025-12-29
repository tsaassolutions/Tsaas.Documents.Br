# Tsaas.Documents.Br

Uma biblioteca .NET para validação e formatação de documentos brasileiros (CPF e CNPJ).

## ?? Características

- ? Validação de CPF e CNPJ com verificação de dígitos verificadores
- ?? Formatação automática de documentos
- ??? Validação em tempo de construção do objeto
- ?? Suporte a .NET 8
- ?? Interface comum `IDocument` para extensibilidade
- ?? Exceções customizadas para tratamento de erros

## ?? Instalação

```bash
dotnet add package Tsaas.Documents.Br
```

## ?? Uso

### CPF

```csharp
using Tsaas.Documents.Br.Documents;
using Tsaas.Documents.Br.Exceptions;

// Criar um CPF válido
var cpf = new Cpf("123.456.789-09");

// Propriedades disponíveis
Console.WriteLine(cpf.Value);             // "123.456.789-09"
Console.WriteLine(cpf.UnformattedValue);  // "12345678909"
Console.WriteLine(cpf.FormattedValue);    // "123.456.789-09"
Console.WriteLine(cpf.IsValid);           // true

// CPF sem formatação também funciona
var cpf2 = new Cpf("12345678909");
Console.WriteLine(cpf2.FormattedValue);   // "123.456.789-09"

// CPF inválido lança exceção
try
{
    var cpfInvalido = new Cpf("111.111.111-11");
}
catch (InvalidDocumentException ex)
{
    Console.WriteLine(ex.Message);        // "O documento CPF '111.111.111-11' é inválido."
    Console.WriteLine(ex.DocumentType);   // "CPF"
    Console.WriteLine(ex.DocumentValue);  // "111.111.111-11"
}
```

### CNPJ

```csharp
using Tsaas.Documents.Br.Documents;
using Tsaas.Documents.Br.Exceptions;

// Criar um CNPJ válido
var cnpj = new Cnpj("11.222.333/0001-81");

// Propriedades disponíveis
Console.WriteLine(cnpj.Value);             // "11.222.333/0001-81"
Console.WriteLine(cnpj.UnformattedValue);  // "11222333000181"
Console.WriteLine(cnpj.FormattedValue);    // "11.222.333/0001-81"
Console.WriteLine(cnpj.IsValid);           // true

// CNPJ sem formatação também funciona
var cnpj2 = new Cnpj("11222333000181");
Console.WriteLine(cnpj2.FormattedValue);   // "11.222.333/0001-81"

// CNPJ inválido lança exceção
try
{
    var cnpjInvalido = new Cnpj("11.111.111/1111-11");
}
catch (InvalidDocumentException ex)
{
    Console.WriteLine(ex.Message);         // "O documento CNPJ '11.111.111/1111-11' é inválido."
    Console.WriteLine(ex.DocumentType);    // "CNPJ"
    Console.WriteLine(ex.DocumentValue);   // "11.111.111/1111-11"
}
```

### Interface IDocument

Todos os documentos implementam a interface `IDocument`, permitindo trabalhar de forma genérica:

```csharp
using Tsaas.Documents.Br.Abstractions;
using Tsaas.Documents.Br.Documents;

IDocument documento = new Cpf("123.456.789-09");
Console.WriteLine($"Documento: {documento.FormattedValue}");
Console.WriteLine($"Válido: {documento.IsValid}");

documento = new Cnpj("11.222.333/0001-81");
Console.WriteLine($"Documento: {documento.FormattedValue}");
Console.WriteLine($"Válido: {documento.IsValid}");
```

## ??? Arquitetura

### Estrutura do Projeto

```
Tsaas.Documents.Br/
??? Abstractions/
?   ??? IDocument.cs           # Interface base para documentos
??? Documents/
?   ??? DocumentBase.cs        # Classe base abstrata
?   ??? Cpf.cs                 # Implementação de CPF
?   ??? Cnpj.cs                # Implementação de CNPJ
??? Exceptions/
?   ??? InvalidDocumentException.cs  # Exceção customizada
??? Formatting/
?   ??? DocumentFormatter.cs   # Formatação de documentos
??? Validation/
    ??? CpfValidator.cs        # Validação de CPF
    ??? CnpjValidator.cs       # Validação de CNPJ
```

### Componentes Principais

#### IDocument
Interface que define o contrato para todos os documentos:
- `Value`: Valor original fornecido
- `UnformattedValue`: Valor sem formatação (apenas números/letras)
- `FormattedValue`: Valor formatado
- `IsValid`: Indica se o documento é válido

#### DocumentBase
Classe abstrata que implementa funcionalidades comuns:
- Armazenamento do valor
- Remoção de caracteres especiais
- Cache de validação

#### InvalidDocumentException
Exceção especializada que contém:
- `DocumentType`: Tipo do documento (CPF, CNPJ)
- `DocumentValue`: Valor que causou a exceção

## ?? Testes

O projeto inclui testes unitários completos em `Tsaas.Documents.Br.Tests`.

Para executar os testes:

```bash
dotnet test
```

## ??? Requisitos

- .NET 8.0 ou superior
- C# 12.0

## ?? Licença

Este projeto está sob a licença MIT.

## ?? Contribuindo

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues ou pull requests.

## ?? Suporte

Para suporte, abra uma issue no [repositório do GitHub](https://github.com/tsaassolutions/Tsaas.Documents.Br).