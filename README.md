# Tsaas.Documents.Br

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4)](https://dotnet.microsoft.com/download/dotnet/8.0)

Uma biblioteca .NET moderna e robusta para validação e formatação de documentos brasileiros (CPF e CNPJ) com suporte a CNPJs alfanuméricos.

## ? Características

- ? **Validação completa** de CPF e CNPJ com verificação de dígitos verificadores
- ?? **Suporte a CNPJ alfanumérico** (A-Z e 0-9 nos primeiros 12 caracteres)
- ?? **Formatação automática** de documentos
- ??? **Validação em tempo de construção** do objeto (fail-fast)
- ?? **Performance otimizada** com cache de validação e valores não formatados
- ?? **Value Object Pattern** implementado corretamente
- ?? **Conversão implícita** para string
- ?? **TryCreate e TryParse** para criação segura sem exceções
- ?? **Interface comum** `IDocument` para extensibilidade
- ?? **Comparação por valor** (Equals, GetHashCode, operadores ==, !=)
- ?? **Exceções customizadas** com informações detalhadas
- ?? **Totalmente documentado** com XML documentation
- ? **100% testado** com xUnit

## ?? Instalação

```bash
dotnet add package Tsaas.Documents.Br
```

Ou via Package Manager Console:

```powershell
Install-Package Tsaas.Documents.Br
```

## ?? Uso

### CPF

#### Criação e Validação

```csharp
using Tsaas.Documents.Br.Documents;
using Tsaas.Documents.Br.Exceptions;

// Criar um CPF válido
var cpf = new Cpf("123.456.789-09");

// Propriedades disponíveis
Console.WriteLine(cpf.Value);             // "123.456.789-09" (valor original)
Console.WriteLine(cpf.UnformattedValue);  // "12345678909" (sem formatação)
Console.WriteLine(cpf.FormattedValue);    // "123.456.789-09" (formatado)
Console.WriteLine(cpf.IsValid);           // true (com cache)

// CPF sem formatação também funciona
var cpf2 = new Cpf("12345678909");
Console.WriteLine(cpf2.FormattedValue);   // "123.456.789-09"
```

#### Criação Segura (Sem Exceções)

```csharp
// TryCreate - retorna null se inválido
var cpfValido = Cpf.TryCreate("987.654.321-00");
if (cpfValido != null)
{
    Console.WriteLine($"CPF válido: {cpfValido.FormattedValue}");
}

var cpfInvalido = Cpf.TryCreate("111.111.111-11");
Console.WriteLine(cpfInvalido == null); // true

// TryParse - padrão .NET
if (Cpf.TryParse("111.444.777-35", out var cpf))
{
    Console.WriteLine($"CPF parseado: {cpf.FormattedValue}");
}
```

#### Conversão Implícita

```csharp
var cpf = new Cpf("123.456.789-09");

// Conversão implícita para string
string cpfString = cpf;
Console.WriteLine(cpfString); // "123.456.789-09"

// ToString() também funciona
Console.WriteLine(cpf.ToString()); // "123.456.789-09"
```

#### Comparação (Value Object)

```csharp
var cpf1 = new Cpf("123.456.789-09");
var cpf2 = new Cpf("12345678909");
var cpf3 = new Cpf("111.444.777-35");

Console.WriteLine(cpf1 == cpf2);  // true (mesmo valor, formatação diferente)
Console.WriteLine(cpf1 != cpf3);  // true (valores diferentes)
Console.WriteLine(cpf1.Equals(cpf2)); // true
Console.WriteLine(cpf1.GetHashCode() == cpf2.GetHashCode()); // true

// Funciona em coleções
var cpfSet = new HashSet<Cpf> { cpf1, cpf2, cpf3 };
Console.WriteLine(cpfSet.Count); // 2 (cpf1 e cpf2 são considerados iguais)
```

#### Tratamento de Erros

```csharp
try
{
    var cpfInvalido = new Cpf("123.456.789-00"); // DV incorreto
}
catch (InvalidDocumentException ex)
{
    Console.WriteLine(ex.Message);        // "O documento CPF '123.456.789-00' é inválido."
    Console.WriteLine(ex.DocumentType);   // "CPF"
    Console.WriteLine(ex.DocumentValue);  // "123.456.789-00"
}
```

**Validações aplicadas:**
- ? Comprimento exato de 11 dígitos
- ? Apenas números (0-9)
- ? Rejeita sequências de dígitos iguais (111.111.111-11, 000.000.000-00, etc.)
- ? Valida ambos os dígitos verificadores usando o algoritmo oficial

### CNPJ

#### Criação e Validação

```csharp
using Tsaas.Documents.Br.Documents;

// CNPJ numérico
var cnpj = new Cnpj("11.222.333/0001-81");

Console.WriteLine(cnpj.Value);             // "11.222.333/0001-81"
Console.WriteLine(cnpj.UnformattedValue);  // "11222333000181"
Console.WriteLine(cnpj.FormattedValue);    // "11.222.333/0001-81"
Console.WriteLine(cnpj.IsValid);           // true

// CNPJ sem formatação
var cnpj2 = new Cnpj("90021382000122");
Console.WriteLine(cnpj2.FormattedValue);   // "90.021.382/0001-22"

// CNPJ alfanumérico (A-Z e 0-9)
var cnpjAlfa = new Cnpj("12ABC34501DE35");
Console.WriteLine(cnpjAlfa.FormattedValue); // "12.ABC.345/01DE-35"
Console.WriteLine(cnpjAlfa.IsValid);        // true
```

#### Criação Segura (Sem Exceções)

```csharp
// TryCreate
var cnpjValido = Cnpj.TryCreate("90.024.778/0001-23");
var cnpjInvalido = Cnpj.TryCreate("11.111.111/1111-11");

Console.WriteLine(cnpjValido?.FormattedValue);  // "90.024.778/0001-23"
Console.WriteLine(cnpjInvalido == null);        // true

// TryParse
if (Cnpj.TryParse("90021382000122", out var cnpj))
{
    Console.WriteLine($"CNPJ parseado: {cnpj.FormattedValue}");
}
```

#### Conversão Implícita e Comparação

```csharp
var cnpj1 = new Cnpj("11.222.333/0001-81");
var cnpj2 = new Cnpj("11222333000181");

// Conversão implícita
string cnpjString = cnpj1;
Console.WriteLine(cnpjString); // "11.222.333/0001-81"

// Comparação
Console.WriteLine(cnpj1 == cnpj2); // true (mesmo valor)
Console.WriteLine(cnpj1.GetHashCode() == cnpj2.GetHashCode()); // true

// Em coleções
var cnpjSet = new HashSet<Cnpj> { cnpj1, cnpj2 };
Console.WriteLine(cnpjSet.Count); // 1 (são considerados iguais)
```

#### Tratamento de Erros

```csharp
try
{
    var cnpj = new Cnpj("00.000.000/0000-00"); // CNPJ zerado
}
catch (InvalidDocumentException ex)
{
    Console.WriteLine(ex.Message); // "O documento CNPJ '00.000.000/0000-00' é inválido."
}

try
{
    var cnpj = new Cnpj("1345c3A5000106"); // Letra minúscula (inválido)
}
catch (InvalidDocumentException ex)
{
    Console.WriteLine(ex.Message); // "O documento CNPJ '1345c3A5000106' é inválido."
}
```

**Validações aplicadas:**
- ? Comprimento exato de 14 caracteres
- ? Primeiros 12 caracteres: A-Z (maiúsculas) ou 0-9
- ? Últimos 2 caracteres (DV): apenas 0-9
- ? Rejeita CNPJ zerado (00000000000000)
- ? Valida ambos os dígitos verificadores usando algoritmo especial para alfanuméricos
- ? Rejeita letras minúsculas

### Interface IDocument

Trabalhe de forma genérica com diferentes tipos de documentos:

```csharp
using Tsaas.Documents.Br.Abstractions;
using Tsaas.Documents.Br.Documents;

void ProcessarDocumento(IDocument documento)
{
    Console.WriteLine($"Tipo: {documento.GetType().Name}");
    Console.WriteLine($"Original: {documento.Value}");
    Console.WriteLine($"Formatado: {documento.FormattedValue}");
    Console.WriteLine($"Sem formatação: {documento.UnformattedValue}");
    Console.WriteLine($"Válido: {documento.IsValid}");
}

IDocument cpf = new Cpf("123.456.789-09");
IDocument cnpj = new Cnpj("11.222.333/0001-81");

ProcessarDocumento(cpf);
ProcessarDocumento(cnpj);
```

### Valores Nulos

```csharp
Cpf? cpfNulo = null;
Cnpj? cnpjNulo = null;

// Conversão implícita retorna string vazia
string cpfString = cpfNulo;   // ""
string cnpjString = cnpjNulo; // ""

Console.WriteLine(cpfNulo == null);  // true
Console.WriteLine(cnpjNulo == null); // true
```

## ??? Arquitetura

### Estrutura do Projeto

```
Tsaas.Documents.Br/
??? Abstractions/
?   ??? IDocument.cs              # Interface base para documentos
??? Documents/
?   ??? DocumentBase.cs           # Classe abstrata com lógica comum
?   ??? Cpf.cs                    # Implementação de CPF
?   ??? Cnpj.cs                   # Implementação de CNPJ
??? Exceptions/
?   ??? InvalidDocumentException.cs  # Exceção customizada
??? Formatting/
?   ??? DocumentFormatter.cs      # Formatação de documentos
??? Validation/
    ??? CpfValidator.cs           # Validação de CPF (apenas numérico)
    ??? CnpjValidator.cs          # Validação de CNPJ (numérico e alfanumérico)
```

### Componentes Principais

#### IDocument
Interface que define o contrato para todos os documentos:

```csharp
public interface IDocument
{
    string Value { get; }           // Valor original fornecido
    string UnformattedValue { get; } // Valor sem formatação (cached)
    string FormattedValue { get; }   // Valor formatado (padrão brasileiro)
    bool IsValid { get; }            // Validação (cached)
}
```

#### DocumentBase
Classe abstrata que implementa funcionalidades comuns:
- ?? Armazenamento imutável do valor original
- ?? Remoção automática de caracteres especiais (mantém A-Z e 0-9)
- ? Cache de `UnformattedValue` e `IsValid`
- ?? Conversão para maiúsculas automática

```csharp
public abstract class DocumentBase : IDocument
{
    protected DocumentBase(string value);
    public string Value { get; }
    public string UnformattedValue { get; } // Lazy cached
    public abstract string FormattedValue { get; }
    public bool IsValid { get; }            // Lazy cached
    protected abstract bool Validate();
}
```

#### CPF e CNPJ (Value Objects)
Implementam o **Value Object Pattern** completo:
- ? Imutabilidade
- ? Igualdade por valor (Equals, GetHashCode)
- ? Operadores == e !=
- ? Conversão implícita para string
- ? TryCreate e TryParse para criação segura
- ? Validação em tempo de construção

#### InvalidDocumentException
Exceção especializada que contém informações detalhadas:

```csharp
public class InvalidDocumentException : Exception
{
    public string DocumentType { get; }   // "CPF" ou "CNPJ"
    public string DocumentValue { get; }  // Valor que causou a exceção
    public override string Message { get; } // Mensagem formatada
}
```

### Algoritmos de Validação

#### CPF
- Valida apenas dígitos numéricos (0-9)
- Rejeita sequências de dígitos iguais
- Calcula dois dígitos verificadores usando pesos específicos

#### CNPJ
- **Base (12 primeiros)**: aceita A-Z (maiúsculas) e 0-9
- **Dígitos verificadores (2 últimos)**: apenas 0-9
- Rejeita CNPJ zerado
- Calcula dígitos verificadores considerando letras como valores ASCII

**Exemplo de cálculo para CNPJ alfanumérico:**
```
Base: 12ABC3450001
Pesos: [5,4,3,2,9,8,7,6,5,4,3,2]
'1' = ASCII 49 ? (49-48) * 5 = 5
'2' = ASCII 50 ? (50-48) * 4 = 8
'A' = ASCII 65 ? (65-48) * 3 = 51
...
```

## ?? Testes

O projeto inclui **100% de cobertura de testes** com xUnit:

```bash
# Executar todos os testes
dotnet test

# Com cobertura
dotnet test /p:CollectCoverage=true
```

### Casos de Teste Cobertos

**CPF:**
- ? Formatados e não formatados válidos
- ? Dígitos verificadores corretos e incorretos
- ? Sequências de dígitos iguais (111.111.111-11, etc.)
- ? Comprimentos inválidos
- ? Caracteres inválidos (letras, especiais)
- ? Valores nulos e vazios

**CNPJ:**
- ? CNPJs numéricos e alfanuméricos válidos
- ? Formatados e não formatados
- ? Dígitos verificadores corretos e incorretos
- ? CNPJ zerado
- ? Letras minúsculas (inválidas)
- ? Letras nos dígitos verificadores (inválidas)
- ? Comprimentos inválidos
- ? Valores nulos e vazios

## ?? Requisitos

- **.NET 8.0** ou superior
- **C# 12.0**

## ?? Versionamento

Seguimos [Semantic Versioning](https://semver.org/):
- **MAJOR**: Mudanças incompatíveis na API
- **MINOR**: Novas funcionalidades (compatível)
- **PATCH**: Correções de bugs

## ?? Licença

Este projeto está sob a licença **MIT**. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

## ?? Contribuindo

Contribuições são bem-vindas! Por favor:

1. Faça um fork do projeto
2. Crie uma branch para sua feature (`git checkout -b feature/MinhaFeature`)
3. Commit suas mudanças (`git commit -m 'Adiciona MinhaFeature'`)
4. Push para a branch (`git push origin feature/MinhaFeature`)
5. Abra um Pull Request

### Diretrizes:
- ? Mantenha 100% de cobertura de testes
- ? Siga os padrões de código existentes
- ? Adicione XML documentation para APIs públicas
- ? Atualize o README se necessário

## ?? Reportar Problemas

Encontrou um bug? Por favor, abra uma [issue](https://github.com/tsaassolutions/Tsaas.Documents.Br/issues) com:
- ?? Descrição clara do problema
- ?? Passos para reproduzir
- ?? Código de exemplo (se possível)
- ??? Versão do .NET e do pacote


## ?? Suporte

- ?? **Email**: suporte@tsaas.com.br
- ?? **Issues**: [GitHub Issues](https://github.com/tsaassolutions/Tsaas.Documents.Br/issues)
- ?? **Documentação**: [GitHub Wiki](https://github.com/tsaassolutions/Tsaas.Documents.Br/wiki)

## ?? Autores

**TSaaS Solutions** - [GitHub](https://github.com/tsaassolutions)

## ?? Agradecimentos

Obrigado a todos que contribuíram para este projeto!

---

? Se este projeto foi útil para você, considere dar uma estrela no GitHub!