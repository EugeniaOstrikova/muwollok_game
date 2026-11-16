using System.Collections.Generic;

namespace Muwollok.Data;

// Бланк (например, 시장): страницы и графы
public class FormData
{
    public string Id { get; set; } = "";
    public List<PageData> Pages { get; set; } = new();
}

public class PageData
{
    public string Id { get; set; } = "";
    public string Title { get; set; } = "";
    public List<FieldData> Fields { get; set; } = new();
}

public class FieldData
{
    public string Id { get; set; } = "";
    public string Label { get; set; } = "";
}

// Дело: набор заполненных документов
public class CaseData
{
    public string Id { get; set; } = "";
    public List<DocumentData> Documents { get; set; } = new();
}

public class DocumentData
{
    public string Id { get; set; } = "";
    public string Form { get; set; } = "";
    public string Title { get; set; } = "";
    // id графы → ключ текста в strings.csv
    public Dictionary<string, string> Values { get; set; } = new();
}