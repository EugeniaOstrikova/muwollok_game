using Godot;
using Muwollok.Data;

public partial class CaseScreen : Control
{
    // Игрок нажал «назад» — GameScreen вернёт вид кабинета
    [Signal] public delegate void BackPressedEventHandler();

    private FormData _form;
    private CaseData _case;

    private int _document = 0;    // какую книгу взяли со стола: 0 = 초검, 1 = 복검
    private int _spreadStart = 0; // номер левой страницы разворота: 0, 2, 4…

    private PageView _leftView;
    private PageView _rightView;
    private Button _prevBtn;
    private Button _nextBtn;

    public override void _Ready()
    {
        _form = DataLoader.LoadForm("sijang");
        _case = DataLoader.LoadCase("test_case");

        _leftView = GetNode<PageView>("%LeftPage");
        _rightView = GetNode<PageView>("%RightPage");
        _prevBtn = GetNode<Button>("%PrevBtn");
        _nextBtn = GetNode<Button>("%NextBtn");

        _prevBtn.Pressed += () => Flip(-2);
        _nextBtn.Pressed += () => Flip(2);

        GetNode<Button>("Layout/BtnContainer/BackBtn").Pressed +=
            () => EmitSignal(SignalName.BackPressed);

        Refresh();
    }

    // Вызывает GameScreen: игрок взял книгу со стола — открываем её с начала
    public void OpenDocument(int docIndex)
    {
        _document = docIndex;
        _spreadStart = 0;
        Refresh();
    }

    // Листаем на целый разворот: -2 назад, +2 вперёд
    private void Flip(int delta)
    {
        _spreadStart += delta;
        Refresh();
    }

    // Перерисовать разворот: две страницы подряд из выбранной книги
    private void Refresh()
    {
        var doc = _case.Documents[_document];
        int pageCount = _form.Pages.Count;

        _leftView.Setup(doc, _form.Pages[_spreadStart]);

        // Если страниц нечётное число, на последнем развороте правой страницы нет
        bool hasRight = _spreadStart + 1 < pageCount;
        _rightView.Visible = hasRight;
        if (hasRight)
            _rightView.Setup(doc, _form.Pages[_spreadStart + 1]);

        // На первом развороте назад нельзя, на последнем — вперёд
        _prevBtn.Disabled = _spreadStart == 0;
        _nextBtn.Disabled = _spreadStart + 2 >= pageCount;
    }
}