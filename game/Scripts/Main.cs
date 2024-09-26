using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public partial class Main : Control

{
	Texture2D ambatukamNormal = GD.Load<Texture2D>("res://Assets/Images/artworks-5aDBMLz7U2zE8hoL-D0dFww-t500x500-removebg-preview.png");
	Texture2D ambatukamCostume = GD.Load<Texture2D>("res://Assets/Images/dreamy-bull-in-his-formal-suit-v0-afe2dcker0qa1-transformed.webp");
	Texture2D SuperSaiyan = GD.Load<Texture2D>("res://Assets/Images/Screenshot_2024-04-27_144612-removebg-preview.png");
	Texture2D WarVeteran = GD.Load<Texture2D>("res://Assets/Images/WarVeteran.png");
	Texture2D God = GD.Load<Texture2D>("res://Assets/Images/god.png");
	[Export] Label ambatukamCoins;
	[Export] ColorRect bg;
	[Export] TextureRect ambatukam;
	[Export] AudioStreamPlayer2D LevelUpSound;
	static AudioStreamPlayer2D  audio1;
	static AudioStreamPlayer2D audio2;
	static AudioStreamPlayer2D audio3;
	static AudioStreamPlayer2D audio4;
	static AudioStreamPlayer2D audio5;
	[Export] Panel upgradePanel;
	[Export] Panel pausePanel;
	[Export] Timer PassiveIncomeTimer;
	[Export] Label label;
	[Export] Label Status;
	[Export] AnimationPlayer animatie;
	[Export] Button[] buttons;
	[Export] Button NewGameButton;
	[Export] Control settingsMenu;
	[Export] Control buttonsMenu;
	List<string> Insults = new List<string>() 
	{
		"mars la munca",
		"la munca nu la intins mana",
		"what color is your bugatti?",
		"nu mai fura ca nu e frumos"
	};
	List<string> InsultsFull = new List<string>
	{
		"mars la munca",
		"la munca nu la intins mana",
		"what color is your bugatti?",
		"nu mai fura ca nu e frumos"
	};
	List<AudioStreamPlayer2D> audios = new List<AudioStreamPlayer2D>();
	List<AudioStreamPlayer2D> audiosFull = new List<AudioStreamPlayer2D>();
	Label coinAddInstance;
	Label upgradePanelText;
	PackedScene insultAdd = GD.Load<PackedScene>("res://Scenes/Insults.tscn");
	PackedScene coinAdd = GD.Load<PackedScene>("res://Scenes/coinAddPrefab.tscn");
	bool canClick = true;
	bool insultOngoing = false;
	bool NormalItem = true;
	int ThisSound;
	int upgradeValue;
	int upgradeValue2;
	string upgradeName;
	public override void _Ready()
	{
		NewGameButton.Disabled = true;
		if (GameManager.passiveIncomeUnlocked) PassiveIncomeTimer.Start();
		audio1 = GetNode<AudioStreamPlayer2D>("BGSound1");
		audio2 = GetNode<AudioStreamPlayer2D>("BGSound2");
		audio3 = GetNode<AudioStreamPlayer2D>("BGSound3");
		audio4 = GetNode<AudioStreamPlayer2D>("BGSound4");
		audio5 = GetNode<AudioStreamPlayer2D>("BGSound5");

		audios = new List<AudioStreamPlayer2D>(){
			audio1,
			audio2,
			audio3,
			audio4,
			audio5
		};
		audiosFull = new List<AudioStreamPlayer2D>(){
			audio1,
			audio2,
			audio3,
			audio4,
			audio5
		};
		settingsMenu.Hide();
		upgradePanel.Hide();
		pausePanel.Hide();
		upgradePanelText = upgradePanel.GetNode<Label>("Label");
		BGSound();
	}
	void BGSound(){
		var random = new RandomNumberGenerator();
		if (audios.Count == 0) audios = new List<AudioStreamPlayer2D>(audiosFull);
		ThisSound = random.RandiRange(0,audios.Count - 1);
		audios[ThisSound].Play();
	}

	public override void _Process(double _delta)
	{
		GameManager.Save();
		if (GameManager.defaultAmbatukam) {
			ambatukam.Texture = ambatukamNormal;
			ambatukam.Scale = new Vector2(1.45f,1.45f);
			NewGameButton.Disabled = true;
			Status.Text = "Status: level 1 poor nigga";
		}
		else if (GameManager.edgeUpgrade){
			ambatukam.Texture = ambatukamNormal;
			ambatukam.Scale = new Vector2(1.45f,1.45f);
			NewGameButton.Disabled = true;
		}
		else if (GameManager.mewUpgrade){
			ambatukam.Texture = ambatukamNormal;
			ambatukam.Scale = new Vector2(1.45f,1.45f);
			NewGameButton.Disabled = true;
		}
		else if (GameManager.goonUpgrade){
			ambatukam.Texture = ambatukamCostume;
			ambatukam.Scale = new Vector2(1.6f,1.6f);
			Status.Text = "Status: lvl 5 alpha male";
			NewGameButton.Disabled = true;
		}
		else if (GameManager.jelqingUpgrade){
			ambatukam.Texture = ambatukamCostume;
			ambatukam.Scale = new Vector2(1.6f,1.6f);
			Status.Text = "Status: lvl 5 alpha male";
			NewGameButton.Disabled = true;
		}
		else if (GameManager.warVeteranUpgrade) {
			ambatukam.Texture = WarVeteran;
			ambatukam.Scale = new Vector2(1.2f,1.4f);
			Status.Text = "Status: lvl 50 War Veteran"; 
			NewGameButton.Disabled = true;
		}
		else if (GameManager.cumUpgrade) {
			ambatukam.Texture = WarVeteran;
			ambatukam.Scale = new Vector2(1.2f,1.4f);
			Status.Text = "Status: lvl 200 War Veteran";
			NewGameButton.Disabled = true;
		}
		else if (GameManager.superSayian) {
			NewGameButton.Disabled = false;
			ambatukam.Texture = SuperSaiyan;
			ambatukam.Scale = new Vector2(1.5f,1.5f);
			Status.Text = "Status: lvl 1000 Super Sayian God";
			ambatukam.Texture = SuperSaiyan;
			if (GameManager.ambatukamPoints == 0) NewGameButton.Disabled = false;
		} 
		else if (GameManager.ambatukamGod){
			ambatukam.Texture = God;
			Status.Text = "Status: lvl 10000 God";
			ambatukam.Scale = new Vector2(1.3f,2.3f);
			Status.Position = new Vector2(Status.Position.X,50);
		}
		for (int i = 0; i < GameManager.loopIndex && i != -1 ; i++){
			buttons[i].Disabled = true;
		}
		if (Insults.Count == 0) Insults = new List<string>(InsultsFull);
		Vector2 mousePos = GetGlobalMousePosition();
		if (mousePos.X >= bg.Size.X|| upgradePanel.Visible || pausePanel.Visible){
			canClick = false;
			if(pausePanel.Visible)GetTree().Paused = true;
			else GetTree().Paused = false;
		}
		else{
			canClick = true;
		}
		label.Text = GameManager.cumCoins.ToString();
		ambatukamCoins.Text = GameManager.ambatukamPoints.ToString();
		if (Input.IsActionJustPressed("click") && canClick){
			animatie.Play("ambatukamClickAnimation");
			GameManager.cumCoins += GameManager.cumCoinsAddValue;
			coinAdded(GetGlobalMousePosition(),GameManager.cumCoinsAddValue);
			if (GameManager.defaultAmbatukam) animatie.Play("ambatukamClickAnimation");
			else if (GameManager.lvl5Achieved) animatie.Play("ambatukamCostumeClickAnimation");
			else if (GameManager.warVeteran) animatie.Play("ambatukamWarClickAnimation");
			else if (GameManager.superSayian) animatie.Play("ambatukamSSJClickAnimation");
			}
		if (GameManager.cumSkinBought){
			ambatukam.Modulate = new Color(3f,3f,3f,0.7f);
		}
		else if (GameManager.pissSkinBought){
			ambatukam.Modulate = new Color(2f, 1.8f, 0.0f,0.7f);
		}
		else{
			ambatukam.Modulate = new Color(1f,1f,1f,1f);
		}
		
	}
	void coinAdded(Vector2 pos,int add){
		coinAddInstance = (Label)coinAdd.Instantiate();
		AddChild(coinAddInstance);
		coinAddInstance.Position = pos;
		if (add == -100) coinAddInstance.Text = add.ToString();
		else coinAddInstance.Text = "+ " + add.ToString();
	}
	void ResetTextures(){
		GameManager.defaultAmbatukam = false;
		GameManager.lvl5Achieved = false;
		GameManager.warVeteran = false;
		GameManager.superSayian = false;
	}
	public void OnEdgeUpgradePressed(){
		NormalItem = true;
		showUpgradePanel(100,"edge",0);
	}
	public void OnMewUpgradePressed(){
		NormalItem = true;
		showUpgradePanel(200,"mew",0);
	}
	public void OnGoonUpgradePressed(){
		NormalItem = true;
		showUpgradePanel(300,"goon",0);
	}
	public void OnJelqingUpgradePressed(){
		NormalItem = true;
		showUpgradePanel(500,"jelqing",0);
	}
	public void OnWarVeteranUpgradePressed(){
		NormalItem = true;
		showUpgradePanel(1000,"war",0);
	}
	public void OnSSJUpgradePressed(){
		NormalItem = true;
		showUpgradePanel(4000,"ssj",0);
	}
	public void OnCumSkinPressed(){
		NormalItem = true;
		showUpgradePanel(150,"cumSkin",0);
	}
	public void OnPissSkinPressed(){
		NormalItem = true;
		showUpgradePanel(150,"piss",0);
	}
	public void OnWhatPressed(){
		NormalItem = false;
		showUpgradePanel(0,"what",0);
	}
	public void OnCumUpgradePressed(){
		NormalItem = true;
		showUpgradePanel(2000,"cumUpgrade",0);
	}
	public void OnQuitToMainMenuPressed(){
		GetTree().Paused = false;
		GetTree().ChangeSceneToFile("res://Scenes/MainMenu.tscn");
	}
	public void OnSettingsPressed(){
		settingsMenu.Show();
		buttonsMenu.Hide();
	}
	public void OnResumePressed(){
		pausePanel.Hide();
		GetTree().Paused = false;
	}
	public void OnBackButtonPressed(){
		settingsMenu.Hide();
		buttonsMenu.Show();
	}
	public void OnNoPressed(){
		upgradePanel.Hide();
	}
	public void OnYesPressed(){
		upgradePanel.Hide();
		if (NormalItem){
			if (GameManager.cumCoins >= upgradeValue){
				switch(upgradeName){
					case "edge":
						GameManager.cumCoins -= upgradeValue;
						GameManager.cumCoinsAddValue ++;
						GameManager.edgeUpgrade = true;
						GameManager.loopIndex +=2;
						break;
					case "mew":
						if (GameManager.edgeUpgrade){
							GameManager.mewUpgrade = true;
							GameManager.cumCoins -= upgradeValue;
							PassiveIncomeTimer.Start();
							GameManager.passiveIncomeUnlocked = true;
							GameManager.cumCoinsPassiveValue ++;
							GameManager.edgeUpgrade = false;
							GameManager.loopIndex ++;
						}
						else ShowInsult("noMoney");
						break;
					case "goon":
						if (GameManager.mewUpgrade){
							GameManager.cumCoins -= upgradeValue;
							GameManager.goonUpgrade = true;
							GameManager.cumCoinsPassiveValue++;
							if(!GameManager.passiveIncomeUnlocked)
								PassiveIncomeTimer.Start();
								GameManager.passiveIncomeUnlocked = true;
							LevelUpSound.Play();
							audios[ThisSound].StreamPaused = true;
							ResetTextures();
							GameManager.lvl5Achieved = true;
							GameManager.goonUpgrade = true;
							GameManager.mewUpgrade = false;
							GameManager.loopIndex ++;
						}
						else {
							ShowInsult("noMoney");
						}
						break;
					case "jelqing":
						if (GameManager.goonUpgrade){
							GameManager.cumCoins -= upgradeValue;
							GameManager.jelqingUpgrade = true;
							GameManager.goonUpgrade = false;
							GameManager.cumCoinsAddValue++;
							GameManager.loopIndex ++;

						}
						else {
							ShowInsult("noMoney");
						}
						break;
					case "war":
						if (GameManager.jelqingUpgrade) {
							GameManager.cumCoins -= upgradeValue;
							GameManager.warVeteranUpgrade = true;
							ResetTextures();
							GameManager.warVeteran = true;
							GameManager.warVeteranUpgrade = true;
							GameManager.cumCoinsAddValue += 3;
							GameManager.cumCoinsPassiveValue += 3;
							LevelUpSound.Play();
							audios[ThisSound].StreamPaused = true;
							GameManager.jelqingUpgrade = false;
							GameManager.loopIndex ++;
						}
						else{
							ShowInsult("noMoney");
						}
						break;
					case "ssj":
						if (GameManager.cumUpgrade){
							GameManager.SuperSayianUpgrade = true;
							ShowInsult("rebirth");
							GameManager.cumCoins -= upgradeValue;
							ResetTextures();
							GameManager.superSayian = true;
							GameManager.cumCoinsAddValue += 4;
							GameManager.cumCoinsPassiveValue += 4;
							LevelUpSound.Play();
							audios[ThisSound].StreamPaused = true;
							GameManager.superSayian = true;
							GameManager.warVeteranUpgrade = false;
							GameManager.warVeteran = false;
							GameManager.cumUpgrade = false;
							ambatukam.Texture = SuperSaiyan;
							GameManager.loopIndex++;
						}
						else{
							ShowInsult("noMoney");
						}
						break;
					case "cumSkin":
						GameManager.cumCoins -= upgradeValue;
						if (!GameManager.cumSkinBought) {
							GameManager.cumSkinBought = true;
							GameManager.pissSkinBought = false;
						}
						else{
							GameManager.cumSkinBought = false;
							GameManager.pissSkinBought = false;
						}
						break;
					case "piss":
						GameManager.cumCoins -= upgradeValue;
						if (!GameManager.pissSkinBought){
							GameManager.pissSkinBought = true;
							GameManager.cumSkinBought = false;
						}
						else{
							GameManager.pissSkinBought = false;
							GameManager.cumSkinBought = false;
						}
						break;
					case "cumUpgrade":
						if (GameManager.warVeteranUpgrade){
							GameManager.cumCoins -= upgradeValue;
							GameManager.cumCoinsAddValue += 5;
							LevelUpSound.Play();
							audios[ThisSound].StreamPaused = true;
							GameManager.cumUpgrade = true;
							GameManager.warVeteranUpgrade = false;
							GameManager.loopIndex ++;
						}
						else {
							ShowInsult("noMoney");
						}
						break;
				}
			}
			else ShowInsult("insult");
		}
		else{
			if (GameManager.cumCoins >= upgradeValue && GameManager.ambatukamPoints >= upgradeValue2){
				switch (upgradeName){
					case "NewGame":
						GameManager.NewGame();
						for (int i = 0; i < buttons.Count() && i != -1 ; i++){
							buttons[i].Disabled = false;
						}
						break;
					case "ambatukamGod":
						if(GameManager.SuperSayianUpgrade){
							GameManager.cumCoins -= upgradeValue;
							GameManager.ambatukamPoints -= upgradeValue2;
							GameManager.ambatukamGod = true;
							GameManager.ambatukamGodUpgrade = true;
							GameManager.loopIndex++;
							GameManager.SuperSayianUpgrade = false;
							LevelUpSound.Play();
							audios[ThisSound].StreamPaused = true;
							GameManager.cumCoinsPassiveValue = 30;
							GameManager.cumCoinsAddValue = 60;
							GameManager.superSayian = false;
						}
						else ShowInsult("upgrade");
						break;
					case "what":
						GameManager.cumCoins -= 100;
						coinAdded(GetGlobalMousePosition(),-100);
						break;
				}
			}
			else{
				ShowInsult("insult");
			}
		}
	}
	async void ShowInsult(string name){
		if (!insultOngoing){
			var random = new RandomNumberGenerator();
			Label InsultText = (Label)insultAdd.Instantiate();
			int randomInsult = random.RandiRange(0,Insults.Count - 1);
			AddChild(InsultText);
			if(insultOngoing) InsultText.QueueFree();
			if (name == "insult"){
				InsultText.Text = Insults[randomInsult];
				Insults.RemoveAt(randomInsult);
			}
			else if (name == "rebirth") InsultText.Text = "NewGame button unlocked!";
			else InsultText.Text = "Buy previous upgrade first!";
			insultOngoing = true;
			await ToSignal(GetTree().CreateTimer(3f),SceneTreeTimer.SignalName.Timeout);
			InsultText.QueueFree();
			insultOngoing = false;
		}
	}
	void showUpgradePanel(int value,string name, int value2){
		upgradeName = name;
		upgradeValue = value;
		upgradeValue2 = value2;
		upgradePanel.Show();
		upgradePanelText.Text = "Are you sure you want to purchase this upgrade for " + value + " cum coins?";
		if (GameManager.cumSkinBought && name == "cumSkin") upgradePanelText.Text = "Are you sure you want to change this skin to default for " + value + " cum coins?";
		if (name == "cumSkin" && !GameManager.cumSkinBought) upgradePanelText.Text = "Are you sure you want to purchase this skin for " + value + " cum coins?";
		if (GameManager.pissSkinBought && name == "piss") upgradePanelText.Text = "Are you sure you want to change this skin to default for " + value + " cum coins?";
		if (name == "piss" && !GameManager.pissSkinBought) upgradePanelText.Text = "Are you sure you want to purchase this skin for " + value + " cum coins?";
		if (name == "NewGame")upgradePanelText.Text = "Are you sure you want to reset your progress and gain 1 ambatukam point?";
	}
	public void OnPassiveIncomeTimerTimeout(){
		var random = new RandomNumberGenerator();
		Vector2 pos = new (random.RandfRange(0f,bg.Size.X - 50),random.RandfRange(0f,bg.Size.Y - 50));
		GameManager.cumCoins += GameManager.cumCoinsPassiveValue;
		coinAdded(pos,GameManager.cumCoinsPassiveValue);
	}
	public void OnAudio1Finished(){
		if (audios[ThisSound] != null) audios.RemoveAt(ThisSound);
		BGSound();
	}
	public void OnAudio2Finished(){
		if (audios[ThisSound] != null) audios.RemoveAt(ThisSound);
		BGSound();
	}
	public void OnAudio3Finished(){
		if (audios[ThisSound] != null) audios.RemoveAt(ThisSound);
		BGSound();
	}
	public void OnAudio4Finished(){
		if (audios[ThisSound] != null) audios.RemoveAt(ThisSound);
		BGSound();
	}
	public void OnAudio5Finished(){
		if (audios[ThisSound] != null) audios.RemoveAt(ThisSound);
		BGSound();
	}
	public void OnLevelUpSoundFinished(){
		audios[ThisSound].StreamPaused = false;
	}
	public override void _Input(InputEvent @event)
	{
		if(Input.IsActionJustPressed("ESC")){
			if(pausePanel.Visible)
				UnPause();
			else Pause();
		}
		void Pause(){
			GetTree().Paused = true;
			pausePanel.Show();
		}
		void UnPause(){
			GetTree().Paused = false;
			pausePanel.Hide();
		}
	}
	void OnNewGamePressed(){
		NormalItem = false;
		showUpgradePanel(0,"NewGame",0);
	}
	void OnAmbatukamGodPressed(){
		NormalItem = false;
		showUpgradePanel(7000,"ambatukamGod",1);
	}
}
