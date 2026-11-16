using Godot;

public partial class StartScreen : Control
{
    // Коды языков в том же порядке, что и пункты в списке
    private static readonly string[] Locales = { "en", "ru", "ko" };

    // _Ready вызывается один раз, когда сцена загрузилась
    public override void _Ready()
    {
        var languageOption = GetNode<OptionButton>("VBoxContainer/LanguageOption");
		string current = TranslationServer.GetLocale();
        int selected = 0; // 0 = English, по умолчанию
        for (int i = 0; i < Locales.Length; i++)
        {
            if (current.StartsWith(Locales[i]))
            {
                selected = i;
                break;
            }
        }
        languageOption.Selected = selected;
        TranslationServer.SetLocale(Locales[selected]);
        languageOption.ItemSelected += OnLanguageSelected;

		var startButton = GetNode<Button>("VBoxContainer/StartBtn");
        startButton.Pressed += OnStartPressed;
    }

    private void OnLanguageSelected(long index)
    {
        TranslationServer.SetLocale(Locales[index]);
    }

	private void OnStartPressed()
    {
        // Заменяет текущую сцену на другую
        GetTree().ChangeSceneToFile("res://scenes/Main.tscn");
    }
}