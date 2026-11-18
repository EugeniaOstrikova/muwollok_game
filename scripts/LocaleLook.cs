using Godot;

[GlobalClass]
public partial class LocaleLook : Resource
{
    [Export] public Texture2D Cover { get; set; }
    [Export] public Font UiFont { get; set; }
    [Export] public Font BookFont { get; set; }
    [Export] public Font DialogueFont { get; set; }
    [Export] public Font HintFont { get; set; } // пусто = как UI
    [Export] public Font MenuFont { get; set; }
}