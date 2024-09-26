using Godot;
using System;
using System.Threading;

public partial class coinAddPrefab : Label
{
	[Export] float speed = 100f;
	public override async void _Ready()
	{
		await ToSignal(GetTree().CreateTimer(0.6f),SceneTreeTimer.SignalName.Timeout);
		SetProcess(false);
		await ToSignal(GetTree().CreateTimer(0.5f),SceneTreeTimer.SignalName.Timeout);
		QueueFree();
	}
	public override void _Process(double delta)
	{
		Position -= new Vector2(0f,1f) * speed * (float)delta;
	}
}
