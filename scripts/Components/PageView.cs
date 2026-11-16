using System.Collections.Generic;
using Godot;
using Muwollok.Data;

public partial class PageView : PanelContainer
{
    // Шаблон графы назначим в инспекторе
    [Export] public PackedScene FieldScene { get; set; }

    public void Setup(DocumentData document, PageData page)
    {
        GetNode<Label>("VBoxContainer/DocTitle").Text = document.Title;
        GetNode<Label>("VBoxContainer/PageTitle").Text = page.Title;

        var fields = GetNode<VBoxContainer>("VBoxContainer/Fields");

        // Убираем графы предыдущей страницы
        foreach (Node child in fields.GetChildren())
            child.QueueFree();

        // Бланк задаёт порядок граф, документ — их содержание
        foreach (var field in page.Fields)
        {
            var view = FieldScene.Instantiate<FieldView>();
            string valueKey = document.Values.GetValueOrDefault(field.Id, "");
            view.Setup(field.Id, field.Label, valueKey);
            fields.AddChild(view);
        }
    }
}