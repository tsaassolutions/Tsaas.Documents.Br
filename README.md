# Tsaas.Documents.Br

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4)](https://dotnet.microsoft.com/download/dotnet/8.0)

Uma biblioteca .NET moderna e robusta para validacao e formatacao de documentos brasileiros (CPF e CNPJ) com suporte a CNPJs alfanumericos.

## Caracteristicas

- [x] **Validacao completa** de CPF e CNPJ com verificacao de digitos verificadores
- [x] **Suporte a CNPJ alfanumerico** (A-Z e 0-9 nos primeiros 12 caracteres)
- [x] **Formatacao automatica** de documentos
- [x] **Validacao em tempo de construcao** do objeto (fail-fast)
- [x] **Performance otimizada** com cache de validacao e valores nao formatados
- [x] **Value Object Pattern** implementado corretamente
- [x] **Conversao implicita** para string
- [x] **TryCreate e TryParse** para criacao segura sem excecoes
- [x] **Interface comum** `IDocument` para extensibilidade
- [x] **Comparacao por valor** (Equals, GetHashCode, operadores ==, !=)
- [x] **Excecoes customizadas** com informacoes detalhadas
- [x] **Totalmente documentado** com XML documentation
- [x] **100% testado** com xUnit

## Instalacao

```bash
dotnet add package Tsaas.Documents.Br
```

Ou via Package Manager Console:

```powershell
Install-Package Tsaas.Documents.Br
```

## Uso

### CPF

#### Criacao e Validacao

```csharp
using Tsaas.Documents.Br.Documents;
using Tsaas.Documents.Br.Exceptions;

// Criar um CPF valido
var cpf = new Cpf("123.456.789-09");

// Propriedades disponiveis
Console.WriteLine(cpf.Value);             // "123.456.789-09" (valor original)
Console.WriteLine(cpf.UnformattedValue);  // "12345678909" (sem formatacao)
Console.WriteLine(cpf.FormattedValue);    // "123.456.789-09" (formatado)
Console.WriteLine(cpf.IsValid);           // true (com cache)

// CPF sem formatacao tambem funciona
var cpf2 = new Cpf("12345678909");
Console.WriteLine(cpf2.FormattedValue);   // "123.456.789-09"
```

#### Criacao Segura (Sem Excecoes)

```csharp
// TryCreate - retorna null se invalido
var cpfValido = Cpf.TryCreate("987.654.321-00");
if (cpfValido != null)
{
    Console.WriteLine($"CPF valido: {cpfValido.FormattedValue}");
}

var cpfInvalido = Cpf.TryCreate("111.111.111-11");
Console.WriteLine(cpfInvalido == null); // true

// TryParse - padrao .NET
if (Cpf.TryParse("111.444.777-35", out var cpf))
{
    Console.WriteLine($"CPF parseado: {cpf.FormattedValue}");
}
```

#### Conversao Implicita

```csharp
var cpf = new Cpf("123.456.789-09");

// Conversao implicita para string
string cpfString = cpf;
Console.WriteLine(cpfString); // "123.456.789-09"

// ToString() tambem funciona
Console.WriteLine(cpf.ToString()); // "123.456.789-09"
```

#### Comparacao (Value Object)

```csharp
var cpf1 = new Cpf("123.456.789-09");
var cpf2 = new Cpf("12345678909");
var cpf3 = new Cpf("111.444.777-35");

Console.WriteLine(cpf1 == cpf2);  // true (mesmo valor, formatacao diferente)
Console.WriteLine(cpf1 != cpf3);  // true (valores diferentes)
Console.WriteLine(cpf1.Equals(cpf2)); // true
Console.WriteLine(cpf1.GetHashCode() == cpf2.GetHashCode()); // true

// Funciona em colecoes
var cpfSet = new HashSet<Cpf> { cpf1, cpf2, cpf3 };
Console.WriteLine(cpfSet.Count); // 2 (cpf1 e cpf2 sao considerados iguais)
```

#### Tratamento de Erros

```csharp
try
{
    var cpfInvalido = new Cpf("123.456.789-00"); // DV incorreto
}
catch (InvalidDocumentException ex)
{
    Console.WriteLine(ex.Message);        // "O documento CPF '123.456.789-00' e invalido."
    Console.WriteLine(ex.DocumentType);   // "CPF"
    Console.WriteLine(ex.DocumentValue);  // "123.456.789-00"
}
```

**Validacoes aplicadas:**
- [x] Comprimento exato de 11 digitos
- [x] Apenas numeros (0-9)
- [x] Rejeita sequencias de digitos iguais (111.111.111-11, 000.000.000-00, etc.)
- [x] Valida ambos os digitos verificadores usando o algoritmo oficial

### CNPJ

#### Criacao e Validacao

```csharp
using Tsaas.Documents.Br.Documents;

// CNPJ numerico
var cnpj = new Cnpj("11.222.333/0001-81");

Console.WriteLine(cnpj.Value);             // "11.222.333/0001-81"
Console.WriteLine(cnpj.UnformattedValue);  // "11222333000181"
Console.WriteLine(cnpj.FormattedValue);    // "11.222.333/0001-81"
Console.WriteLine(cnpj.IsValid);           // true

// CNPJ sem formatacao
var cnpj2 = new Cnpj("90021382000122");
Console.WriteLine(cnpj2.FormattedValue);   // "90.021.382/0001-22"

// CNPJ alfanumerico (A-Z e 0-9)
var cnpjAlfa = new Cnpj("12ABC34501DE35");
Console.WriteLine(cnpjAlfa.FormattedValue); // "12.ABC.345/01DE-35"
Console.WriteLine(cnpjAlfa.IsValid);        // true
```

#### Criacao Segura (Sem Excecoes)

```csharp
// TryCreate
var cnpjValido = Cnpj.TryCreate("90.024.778/0001-23");
var cnpjInvalido = Cnpj.TryCreate("11.111.111/1111-11");

Console.WriteLine(cnpjValido.FormattedValue);  // "90.024.778/0001-23"
Console.WriteLine(cnpjInvalido == null);        // true

// TryParse
if (Cnpj.TryParse("90021382000122", out var cnpj))
{
    Console.WriteLine($"CNPJ parseado: {cnpj.FormattedValue}");
}
```

#### Conversao Implicita e Comparacao

```csharp
var cnpj1 = new Cnpj("11.222.333/0001-81");
var cnpj2 = new Cnpj("11222333000181");

// Conversao implicita
string cnpjString = cnpj1;
Console.WriteLine(cnpjString); // "11.222.333/0001-81"

// Comparacao
Console.WriteLine(cnpj1 == cnpj2); // true (mesmo valor)
Console.WriteLine(cnpj1.GetHashCode() == cnpj2.GetHashCode()); // true

// Em colecoes
var cnpjSet = new HashSet<Cnpj> { cnpj1, cnpj2 };
Console.WriteLine(cnpjSet.Count); // 1 (sao considerados iguais)
```

#### Tratamento de Erros

```csharp
try
{
    var cnpj = new Cnpj("00.000.000/0000-00"); // CNPJ zerado
}
catch (InvalidDocumentException ex)
{
    Console.WriteLine(ex.Message); // "O documento CNPJ '00.000.000/0000-00' e invalido."
}

try
{
    var cnpj = new Cnpj("1345c3A5000106"); // Letra minuscula (invalido)
}
catch (InvalidDocumentException ex)
{
    Console.WriteLine(ex.Message); // "O documento CNPJ '1345c3A5000106' e invalido."
}
```

**Validacoes aplicadas:**
- [x] Comprimento exato de 14 caracteres
- [x] Primeiros 12 caracteres: A-Z (maiusculas) ou 0-9
- [x] Ultimos 2 caracteres (DV): apenas 0-9
- [x] Rejeita CNPJ zerado (00000000000000)
- [x] Valida ambos os digitos verificadores usando algoritmo especial para alfanumericos
- [ ] Rejeita letras minusculas

### Interface IDocument

Trabalhe de forma generica com diferentes tipos de documentos:

```csharp
using Tsaas.Documents.Br.Abstractions;
using Tsaas.Documents.Br.Documents;

void ProcessarDocumento(IDocument documento)
{
    Console.WriteLine($"Tipo: {documento.GetType().Name}");
    Console.WriteLine($"Original: {documento.Value}");
    Console.WriteLine($"Formatado: {documento.FormattedValue}");
    Console.WriteLine($"Sem formatacao: {documento.UnformattedValue}");
    Console.WriteLine($"Valido: {documento.IsValid}");
}

IDocument cpf = new Cpf("123.456.789-09");
IDocument cnpj = new Cnpj("11.222.333/0001-81");

ProcessarDocumento(cpf);
ProcessarDocumento(cnpj);
```

### Valores Nulos

```csharp
Cpf cpfNulo = null;
Cnpj cnpjNulo = null;

// Conversao implicita retorna string vazia
string cpfString = cpfNulo;   // ""
string cnpjString = cnpjNulo; // ""

Console.WriteLine(cpfNulo == null);  // true
Console.WriteLine(cnpjNulo == null); // true
```

## Arquitetura


```

### Componentes Principais

#### IDocument
Interface que define o contrato para todos os documentos:

```csharp
public interface IDocument
{
    string Value { get; }           // Valor original fornecido
    string UnformattedValue { get; } // Valor sem formatacao (cached)
    string FormattedValue { get; }   // Valor formatado (padrao brasileiro)
    bool IsValid { get; }            // Validacao (cached)
}
```

#### DocumentBase
Classe abstrata que implementa funcionalidades comuns:
- Armazenamento imutavel do valor original
- Remocao automatica de caracteres especiais (mantem A-Z e 0-9)
- Cache de `UnformattedValue` e `IsValid`
- Conversao para maiusculas automatica

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
- [x] Imutabilidade
- [x] Igualdade por valor (Equals, GetHashCode)
- [x] Operadores == e !=
- [x] Conversao implicita para string
- [x] TryCreate e TryParse para criacao segura
- [x] Validacao em tempo de construcao

#### InvalidDocumentException
Excecao especializada que contem informacoes detalhadas:

```csharp
public class InvalidDocumentException : Exception
{
    public string DocumentType { get; }   // "CPF" ou "CNPJ"
    public string DocumentValue { get; }  // Valor que causou a excecao
    public override string Message { get; } // Mensagem formatada
}
```

### Algoritmos de Validacao

#### CPF
- Valida apenas digitos numericos (0-9)
- Rejeita sequencias de digitos iguais
- Calcula dois digitos verificadores usando pesos especificos

#### CNPJ
- **Base (12 primeiros)**: aceita A-Z (maiusculas) e 0-9
- **Digitos verificadores (2 ultimos)**: apenas 0-9
- Rejeita CNPJ zerado
- Calcula digitos verificadores considerando letras como valores ASCII

**Exemplo de calculo para CNPJ alfanumerico:**
```
Base: 12ABC3450001
Pesos: [5,4,3,2,9,8,7,6,5,4,3,2]
'1' = ASCII 49 -> (49-48) * 5 = 5
'2' = ASCII 50 -> (50-48) * 4 = 8
'A' = ASCII 65 -> (65-48) * 3 = 51
...
```

## Testes

O projeto inclui **100% de cobertura de testes** com xUnit:

```bash
# Executar todos os testes
dotnet test

# Com cobertura
dotnet test /p:CollectCoverage=true
```

### Casos de Teste Cobertos

**CPF:**
- [x] Formatados e nao formatados validos
- [x] Digitos verificadores corretos e incorretos
- [x] Sequencias de digitos iguais (111.111.111-11, etc.)
- [x] Comprimentos invalidos
- [x] Caracteres invalidos (letras, especiais)
- [x] Valores nulos e vazios

**CNPJ:**
- [x] CNPJs numericos e alfanumericos validos
- [x] Formatados e nao formatados
- [x] Digitos verificadores corretos e incorretos
- [x] CNPJ zerado
- [x] Letras minusculas (invalidas)
- [x] Letras nos digitos verificadores (invalidas)
- [x] Comprimentos invalidos
- [x] Valores nulos e vazios

## Requisitos

- **.NET 8.0** ou superior
- **C# 12.0**

## Versionamento

Seguimos [Semantic Versioning](https://semver.org/):
- **MAJOR**: Mudancas incompativeis na API
- **MINOR**: Novas funcionalidades (compativel)
- **PATCH**: Correcoes de bugs

## Licenca

Este projeto esta sob a licenca **MIT**. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

## Contribuindo

Contribuicoes sao bem-vindas! Por favor:

1. Faca um fork do projeto
2. Crie uma branch para sua feature (`git checkout -b feature/MinhaFeature`)
3. Commit suas mudancas (`git commit -m 'Adiciona MinhaFeature'`)
4. Push para a branch (`git push origin feature/MinhaFeature`)
5. Abra um Pull Request

### Diretrizes:
- [x] Mantenha 100% de cobertura de testes
- [x] Siga os padroes de codigo existentes
- [x] Adicione XML documentation para APIs publicas
- [x] Atualize o README se necessario

## Reportar Problemas

Encontrou um bug? Por favor, abra uma [issue](https://github.com/tsaassolutions/Tsaas.Documents.Br/issues) com:
- Descricao clara do problema
- Passos para reproduzir
- Codigo de exemplo (se possivel)
- Versao do .NET e do pacote

## Roadmap

Funcionalidades planejadas:
- [ ] Validacao de RG
- [ ] Validacao de CNH
- [ ] Validacao de Titulo de Eleitor
- [ ] Validacao de PIS/PASEP


## Suporte

- **Email**: suporte@tsaas.com.br
- **Issues**: [GitHub Issues](https://github.com/tsaassolutions/Tsaas.Documents.Br/issues)
- **Documentacao**: [GitHub Wiki](https://github.com/tsaassolutions/Tsaas.Documents.Br/wiki)

## Autores

**TSaaS Solutions** - [GitHub](https://github.com/tsaassolutions)

## Agradecimentos

Obrigado a todos que contribuiram para este projeto!

---

Se este projeto foi util para voce, considere dar uma estrela no GitHub!
