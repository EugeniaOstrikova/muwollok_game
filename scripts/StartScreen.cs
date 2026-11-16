using Godot;

public partial class StartScreen : Control
{
    // Коды языков в том же порядке, что и пункты в списке
    private static readonly string[] Locales = { "en", "ru", "ko" };

    // _Ready вызывается один раз, когда сцена загрузилась
    public override void _Ready()
    {
        var languageOption = GetNode<OptionButton>("VBoxContainer/LanguageOption");

        // Подписываемся на сигнал: «когда выбрали пункт — вызови мой метод»
        languageOption.ItemSelected += OnLanguageSelected;
    }

    private void OnLanguageSelected(long index)
    {
        TranslationServer.SetLocale(Locales[index]);
    }
}