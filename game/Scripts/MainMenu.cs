using Godot;
using System;

public partial class MainMenu : Control
{
	[Export] AudioStreamPlayer2D BGM;
	[Export] ParallaxBackground BG;
	[Export] Panel SettingsPanel;
	[Export] Control menuButtons;
	public override void _Ready()
	{
		GameManager.Load();
		BGM.Play();
		SettingsPanel.Hide();
	}
	public override void _Process(double delta)
	{
		BG.ScrollOffset -= new Vector2(150f,0f) * (float)delta;
	}
	public void OnPlayPressed(){
		GetTree().ChangeSceneToFile("res://Scenes/Main.tscn");
	}
	public void OnMenuSettingsPressed(){
		SettingsPanel.Show();
		menuButtons.Hide();
	}
	public void OnMenuBackPressed(){
		SettingsPanel.Hide();
		menuButtons.Show();
	}
	public void OnQuitPressed(){
		GetTree().Quit();
	}
}
