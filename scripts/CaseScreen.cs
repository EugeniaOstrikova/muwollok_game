using Godot;
using Muwollok.Data;

public partial class CaseScreen : Control
{
    private enum Mode { Study, Compare }

    private FormData _form;
    private CaseData _case;

    // Состояние экрана
    private Mode _mode = Mode.Study;
    private int _studyDocument = 0; // 0 = 초검, 1 = 복검
    private int _leftPage = 0;
    private int _rightPage = 1;

    private PageView _leftView;
    private PageView _rightView;
    private HBoxContainer _leftTabs;
    private HBoxContainer _rightTabs;
	private HBoxContainer _modeBar;

    public override void _Ready()
    {
        _form = DataLoader.LoadForm("sijang");
        _case = DataLoader.LoadCase("test_case");

        _leftView = GetNode<PageView>("%LeftPage");
        _rightView = GetNode<PageView>("%RightPage");
        _leftTabs = GetNode<HBoxContainer>("%LeftTabs");
        _rightTabs = GetNode<HBoxContainer>("%RightTabs");
        _modeBar = GetNode<HBoxContainer>("%ModeBar");

        CreateTabs(_leftTabs, isLeft: true);
        CreateTabs(_rightTabs, isLeft: false);
        CreateModeButtons();

        Refresh();
    }

    // По кнопке на каждую страницу бланка
    private void CreateTabs(HBoxContainer container, bool isLeft)
    {
        for (int i = 0; i < _form.Pages.Count; i++)
        {
            int pageIndex = i; // своя копия для каждой кнопки
            var button = new Button { Text = _form.Pages[i].Title };
            button.Pressed += () => OnTabPressed(isLeft, pageIndex);
            container.AddChild(button);
        }
    }

    private void OnTabPressed(bool isLeft, int pageIndex)
    {
        // В сравнении обе стороны листаются вместе
        if (_mode == Mode.Compare || isLeft)
            _leftPage = pageIndex;
        else
            _rightPage = pageIndex;

        Refresh();
    }

	    // Кнопка «изучить» на каждый документ + кнопка «сравнить»
    private void CreateModeButtons()
    {
        for (int i = 0; i < _case.Documents.Count; i++)
        {
            int docIndex = i;
            var button = new Button { Text = _case.Documents[i].Title };
            button.Pressed += () => SetStudyMode(docIndex);
            _modeBar.AddChild(button);
        }

        var compare = new Button { Text = "UI_MODE_COMPARE" };
        compare.Pressed += SetCompareMode;
        _modeBar.AddChild(compare);
    }

    private void SetStudyMode(int docIndex)
    {
        _mode = Mode.Study;
        _studyDocument = docIndex;
        Refresh();
    }

    private void SetCompareMode()
    {
        _mode = Mode.Compare;
        Refresh();
    }

    // Перерисовать разворот по текущему состоянию
    private void Refresh()
    {
        if (_mode == Mode.Study)
        {
            var doc = _case.Documents[_studyDocument];
            _leftView.Setup(doc, _form.Pages[_leftPage]);
            _rightView.Setup(doc, _form.Pages[_rightPage]);
        }
        else
        {
            _leftView.Setup(_case.Documents[0], _form.Pages[_leftPage]);
            _rightView.Setup(_case.Documents[1], _form.Pages[_leftPage]);
        }

        HighlightTab(_leftTabs, _leftPage);
        HighlightTab(_rightTabs, _mode == Mode.Compare ? _leftPage : _rightPage);
		        // Кнопки документов идут первыми, «Сравнение» — последней
        int activeMode = _mode == Mode.Compare ? _case.Documents.Count : _studyDocument;
        HighlightTab(_modeBar, activeMode);
    }

    // Текущая закладка неактивна — так видно, какая страница открыта
    private static void HighlightTab(HBoxContainer tabs, int current)
    {
        for (int i = 0; i < tabs.GetChildCount(); i++)
            tabs.GetChild<Button>(i).Disabled = i == current;
    }
}