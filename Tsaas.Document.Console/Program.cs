using Tsaas.Document.Console;
using Tsaas.Documents.Br.Documents;
using Tsaas.Documents.Br.Exceptions;

Console.WriteLine("=== Demonstração Tsaas.Documents.Br com FederalTaxIds ===\n");

// === 1. Criação básica ===
Console.WriteLine("--- 1. Criação Básica ---");
var federalTaxIds1 = new FederalTaxIds("11.222.333/0001-81", "123.456.789-09");
Console.WriteLine($"CNPJ: {federalTaxIds1.Cnpj.FormattedValue}");
Console.WriteLine($"CPF: {federalTaxIds1.Cpf.FormattedValue}");

// === 2. Propriedades do Value Object (IDocument) ===
Console.WriteLine("\n--- 2. Propriedades dos Value Objects ---");
Console.WriteLine($"CNPJ Original: {federalTaxIds1.Cnpj.Value}");
Console.WriteLine($"CNPJ Sem Formatação: {federalTaxIds1.Cnpj.UnformattedValue}");
Console.WriteLine($"CNPJ Formatado: {federalTaxIds1.Cnpj.FormattedValue}");
Console.WriteLine($"CNPJ é válido: {federalTaxIds1.Cnpj.IsValid}");

Console.WriteLine($"CPF Original: {federalTaxIds1.Cpf.Value}");
Console.WriteLine($"CPF Sem Formatação: {federalTaxIds1.Cpf.UnformattedValue}");
Console.WriteLine($"CPF Formatado: {federalTaxIds1.Cpf.FormattedValue}");
Console.WriteLine($"CPF é válido: {federalTaxIds1.Cpf.IsValid}");

// === 3. Aceita entrada com ou sem formatação ===
Console.WriteLine("\n--- 3. Flexibilidade de Entrada ---");
var federalTaxIds2 = new FederalTaxIds("11222333000181", "12345678909"); // Sem formatação
Console.WriteLine($"CNPJ (entrada sem formatação): {federalTaxIds2.Cnpj.FormattedValue}");
Console.WriteLine($"CPF (entrada sem formatação): {federalTaxIds2.Cpf.FormattedValue}");

// === 4. Comparação de Value Objects (Equals) ===
Console.WriteLine("\n--- 4. Comparação de Value Objects ---");
var federalTaxIds3 = new FederalTaxIds("90.021.382/0001-22", "111.444.777-35");
Console.WriteLine($"FederalTaxIds1.Cnpj == FederalTaxIds3.Cnpj: {federalTaxIds1.Cnpj == federalTaxIds3.Cnpj}");
Console.WriteLine($"FederalTaxIds1.Cpf == FederalTaxIds3.Cpf: {federalTaxIds1.Cpf == federalTaxIds3.Cpf}");
Console.WriteLine($"FederalTaxIds1.Cnpj == FederalTaxIds2.Cnpj: {federalTaxIds1.Cnpj == federalTaxIds2.Cnpj}");

// === 5. Conversão implícita para string ===
Console.WriteLine("\n--- 5. Conversão Implícita para String ---");
string cnpjString = federalTaxIds1.Cnpj;
string cpfString = federalTaxIds1.Cpf;
Console.WriteLine($"CNPJ como string: {cnpjString}");
Console.WriteLine($"CPF como string: {cpfString}");

// === 6. ToString() ===
Console.WriteLine("\n--- 6. Método ToString() ---");
Console.WriteLine($"CNPJ.ToString(): {federalTaxIds1.Cnpj.ToString()}");
Console.WriteLine($"CPF.ToString(): {federalTaxIds1.Cpf.ToString()}");

// === 7. Modificando valores das propriedades ===
Console.WriteLine("\n--- 7. Modificação de Valores ---");
federalTaxIds1.Cnpj = new Cnpj("90.024.778/0001-23");
federalTaxIds1.Cpf = new Cpf("000.000.001-91");
Console.WriteLine($"CNPJ atualizado: {federalTaxIds1.Cnpj.FormattedValue}");
Console.WriteLine($"CPF atualizado: {federalTaxIds1.Cpf.FormattedValue}");

// === 8. TryCreate - criação segura sem exceções ===
Console.WriteLine("\n--- 8. TryCreate - Criação Segura ---");
var cnpjValido = Cnpj.TryCreate("12ABC34501DE35");
var cnpjInvalido = Cnpj.TryCreate("11.111.111/1111-11");
var cpfValido = Cpf.TryCreate("987.654.321-00");
var cpfInvalido = Cpf.TryCreate("111.111.111-11");

Console.WriteLine($"CNPJ válido alfanumérico (TryCreate): {(cnpjValido == null ? "null" : cnpjValido.FormattedValue)}");
Console.WriteLine($"CNPJ inválido (TryCreate): {(cnpjInvalido == null ? "null" : cnpjInvalido.FormattedValue)}");
Console.WriteLine($"CPF válido (TryCreate): {(cpfValido == null ? "null" : cpfValido.FormattedValue)}");
Console.WriteLine($"CPF inválido (TryCreate): {(cpfInvalido == null ? "null" : cpfInvalido.FormattedValue)}");

// === 9. TryParse - padrão .NET ===
Console.WriteLine("\n--- 9. TryParse - Padrão .NET ---");
if (Cnpj.TryParse("90021382000122", out var cnpjParsed))
{
    Console.WriteLine($"CNPJ parseado com sucesso: {cnpjParsed?.FormattedValue}");
}

if (Cpf.TryParse("11144477735", out var cpfParsed))
{
    Console.WriteLine($"CPF parseado com sucesso: {cpfParsed?.FormattedValue}");
}

if (!Cnpj.TryParse("90.025.108/0001-01", out var cnpjFalhou))
{
    Console.WriteLine($"CNPJ inválido não foi parseado: {cnpjFalhou == null}");
}

if (!Cpf.TryParse("123.456.789-10", out var cpfFalhou))
{
    Console.WriteLine($"CPF inválido não foi parseado: {cpfFalhou == null}");
}

// === 10. Trabalhando com valores nulos ===
Console.WriteLine("\n--- 10. Valores Nulos ---");
Cnpj? cnpjNulo = null;
Cpf? cpfNulo = null;
Console.WriteLine($"CNPJ nulo: {cnpjNulo == null}");
Console.WriteLine($"CPF nulo: {cpfNulo == null}");
string cnpjNuloString = cnpjNulo;
string cpfNuloString = cpfNulo;
Console.WriteLine($"CNPJ nulo como string: '{cnpjNuloString}'");
Console.WriteLine($"CPF nulo como string: '{cpfNuloString}'");

// === 11. HashCode e Equals ===
Console.WriteLine("\n--- 11. HashCode e Equals ---");
var cnpj1 = new Cnpj("11.222.333/0001-81");
var cnpj2 = new Cnpj("11222333000181");

Console.WriteLine($"CNPJ1 HashCode: {cnpj1.GetHashCode()}");
Console.WriteLine($"CNPJ2 HashCode: {cnpj2.GetHashCode()}");
Console.WriteLine($"CNPJ1.Equals(CNPJ2): {cnpj1.Equals(cnpj2)}");
Console.WriteLine($"CNPJ1 == CNPJ2: {cnpj1 == cnpj2}");

try
{
    var cnpj3 = new Cnpj("11.222.333/0001-82");
    Console.WriteLine($"CNPJ3 HashCode: {cnpj3.GetHashCode()}");
    Console.WriteLine($"CNPJ1 != CNPJ3: {cnpj1 != cnpj3}");
}
catch (InvalidDocumentException ex)
{
    Console.WriteLine($"Exceção capturada ao criar CNPJ3 com dígito verificador incorreto:");
    Console.WriteLine($"  Mensagem: {ex.Message}");
    Console.WriteLine($"  Tipo: {ex.DocumentType}");
    Console.WriteLine($"  Valor: {ex.DocumentValue}");
}

var cpf1 = new Cpf("123.456.789-09");
var cpf2 = new Cpf("12345678909");
Console.WriteLine($"\nCPF1 HashCode: {cpf1.GetHashCode()}");
Console.WriteLine($"CPF2 HashCode: {cpf2.GetHashCode()}");
Console.WriteLine($"CPF1.Equals(CPF2): {cpf1.Equals(cpf2)}");
Console.WriteLine($"CPF1 == CPF2: {cpf1 == cpf2}");

// === 12. Usando em coleções (Set) ===
Console.WriteLine("\n--- 12. Usando em Coleções (HashSet) ---");
var cnpjSet = new HashSet<Cnpj>
{
    new Cnpj("11.222.333/0001-81"),
    new Cnpj("11222333000181"), // Mesmo CNPJ, formatação diferente
    new Cnpj("90.021.382/0001-22")
};

try
{
    cnpjSet.Add(new Cnpj("90.025.255/0001-00")); // CNPJ com DV incorreto
}
catch (InvalidDocumentException ex)
{
    Console.WriteLine($"Não foi possível adicionar CNPJ inválido ao Set:");
    Console.WriteLine($"  Mensagem: {ex.Message}");
}

Console.WriteLine($"Quantidade de CNPJs únicos no Set: {cnpjSet.Count}"); // Deve ser 2 (dois CNPJs únicos)

var cpfSet = new HashSet<Cpf>
{
    new Cpf("123.456.789-09"),
    new Cpf("12345678909"), // Mesmo CPF, formatação diferente
    new Cpf("111.444.777-35")
};

try
{
    cpfSet.Add(new Cpf("123.456.789-99")); // CPF com DV incorreto
}
catch (InvalidDocumentException ex)
{
    Console.WriteLine($"Não foi possível adicionar CPF inválido ao Set:");
    Console.WriteLine($"  Mensagem: {ex.Message}");
}

Console.WriteLine($"Quantidade de CPFs únicos no Set: {cpfSet.Count}"); // Deve ser 2 (dois CPFs únicos)

// === 13. Tratamento de exceções ===
Console.WriteLine("\n--- 13. Tratamento de Exceções ---");
try
{
    var federalTaxIdsInvalido = new FederalTaxIds("00.000.000/0000-00", "000.000.000-00");
}
catch (InvalidDocumentException ex)
{
    Console.WriteLine($"Exceção capturada ao criar FederalTaxIds com CNPJ zerado:");
    Console.WriteLine($"  Mensagem: {ex.Message}");
    Console.WriteLine($"  Tipo: {ex.DocumentType}");
    Console.WriteLine($"  Valor: {ex.DocumentValue}");
}

try
{
    var cpfInvalidoException = new Cpf("123.456.789-00");
}
catch (InvalidDocumentException ex)
{
    Console.WriteLine($"\nExceção capturada ao criar CPF com DV incorreto:");
    Console.WriteLine($"  Mensagem: {ex.Message}");
    Console.WriteLine($"  Tipo: {ex.DocumentType}");
    Console.WriteLine($"  Valor: {ex.DocumentValue}");
}

try
{
    var cnpjInvalidoException = new Cnpj("R55231B3000700");
}
catch (InvalidDocumentException ex)
{
    Console.WriteLine($"\nExceção capturada ao criar CNPJ alfanumérico inválido:");
    Console.WriteLine($"  Mensagem: {ex.Message}");
    Console.WriteLine($"  Tipo: {ex.DocumentType}");
    Console.WriteLine($"  Valor: {ex.DocumentValue}");
}

try
{
    var cpfComLetrasException = new Cpf("123.456.ABC-09");
}
catch (InvalidDocumentException ex)
{
    Console.WriteLine($"\nExceção capturada ao criar CPF com letras:");
    Console.WriteLine($"  Mensagem: {ex.Message}");
    Console.WriteLine($"  Tipo: {ex.DocumentType}");
    Console.WriteLine($"  Valor: {ex.DocumentValue}");
}

try
{
    var cnpjMinusculaException = new Cnpj("1345c3A5000106");
}
catch (InvalidDocumentException ex)
{
    Console.WriteLine($"\nExceção capturada ao criar CNPJ com letra minúscula:");
    Console.WriteLine($"  Mensagem: {ex.Message}");
    Console.WriteLine($"  Tipo: {ex.DocumentType}");
    Console.WriteLine($"  Valor: {ex.DocumentValue}");
}

Console.WriteLine("\n=== Fim da demonstração ===");
Console.ReadKey();

