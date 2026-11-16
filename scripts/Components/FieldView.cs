using Godot;

public partial class FieldView : PanelContainer
{
    public string FieldId { get; private set; } = "";

    public void Setup(string fieldId, string labelKey, string valueKey)
    {
        FieldId = fieldId;
        // В Label кладём ключи: Godot сам переведёт их
        // и обновит при смене языка
        GetNode<Label>("VBoxContainer/FieldLabel").Text = labelKey;
        GetNode<Label>("VBoxContainer/FieldValue").Text = valueKey;
    }
}