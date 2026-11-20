using Godot;

public partial class OfficeView : Control
{
	[Signal] public delegate void DocumentOpenedEventHandler(int docIndex);

	public override void _Ready()
	{
		GetNode<TextureButton>("%Book1").Pressed +=
			() => EmitSignal(SignalName.DocumentOpened, 0);
		GetNode<TextureButton>("%Book2").Pressed +=
			() => EmitSignal(SignalName.DocumentOpened, 1);
	}
}