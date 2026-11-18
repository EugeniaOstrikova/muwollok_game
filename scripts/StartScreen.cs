using System.Linq;
using Godot;

public partial class StartScreen : Control
{
    private static readonly string[] Locales = { "en", "ru", "ko" };

    [Export] public Theme MainTheme { get; set; }
    [Export] public LocaleLook[] Looks { get; set; } // в порядке en, ru, ko

    private TextureRect _background;
    public override void _Ready()
    {
        _background = GetNode<TextureRect>("Background");

        var languageOption = GetNode<OptionButton>("VBoxContainer/LanguageOption");
        string current = TranslationServer.GetLocale();
        int selected = 0;
        for (int i = 0; i < Locales.Length; i++)
        {
            if (current.StartsWith(Locales[i]))
            {
                selected = i;
                break;
            }
        }
        languageOption.Selected = selected;
        languageOption.ItemSelected += index => ApplyLanguage((int)index);
        ApplyLanguage(selected);

        GetNode<Button>("VBoxContainer/StartBtn").Pressed +=
            () => GetTree().ChangeSceneToFile("res://scenes/Main.tscn");
    }

    private void ApplyLanguage(int index)
    {
        TranslationServer.SetLocale(Locales[index]);
        LocaleLook look = Looks[index];

        _background.Texture = look.Cover;
        MainTheme.DefaultFont = look.UiFont; // настройки, списки, всё без своей роли
        SetRoleFont("BookText", "normal_font", look.BookFont);
        SetRoleFont("DialogueText", "normal_font", look.DialogueFont);
        SetRoleFont("HintText", "normal_font", look.HintFont);
        SetRoleFont("StartMenuButton", "font", look.MenuFont);
    }

    // Если шрифт роли не задан, текст берёт шрифт UI
    // Если шрифт роли не задан, берётся шрифт UI
    private void SetRoleFont(string role, string slot, Font font)
    {
        if (font != null)
            MainTheme.SetFont(slot, role, font);
        else if (MainTheme.GetFontList(role).Contains(slot))
            MainTheme.ClearFont(slot, role);
    }
}