using Godot;

public partial class GameScreen : Control
{
	private OfficeView _office;
	private CaseScreen _desk;

	public override void _Ready()
	{
		_office = GetNode<OfficeView>("OfficeView");
		_desk = GetNode<CaseScreen>("DeskView");
		_office.DocumentOpened += ShowDesk;
		_desk.BackPressed += ShowOffice;
		ShowOffice();
	}

	private void ShowDesk(int docIndex)
	{
		_desk.OpenDocument(docIndex);
		_office.Visible = false;
		_desk.Visible = true;
	}

	private void ShowOffice()
	{
		_desk.Visible = false;
		_office.Visible = true;
	}
}