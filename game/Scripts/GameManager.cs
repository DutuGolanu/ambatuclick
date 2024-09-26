using Godot;
using System;
using System.IO;

public partial class GameManager : Node
{
	public static bool defaultAmbatukam = false;
	public static int cumCoins = 0;
	public static bool edgeUpgrade = false;
	public static bool mewUpgrade = false;
	public static bool goonUpgrade = false;
	public static bool jelqingUpgrade = false;
	public static bool warVeteranUpgrade = false;
	public static bool SuperSayianUpgrade = false;
	public static bool pissSkinBought = false;
	public static bool cumUpgrade = false;
	public static bool lvl5Achieved = false;
	public static bool warVeteran = false;
	public static bool superSayian = false;
	public static bool ambatukamGod = false;
	public static bool passiveIncomeUnlocked = false;
	public static int cumCoinsPassiveValue = 0;
	public static int cumCoinsAddValue = 1;
	public static bool cumSkinBought = false;
	public static int loopIndex = -1;
	public static int ambatukamPoints = 0;
	public static bool ambatukamGodUpgrade = false;
	public static string SavePath = "user://ambatukamSaveData.dat";
	public static void Save(){
		using var file = Godot.FileAccess.Open(SavePath,Godot.FileAccess.ModeFlags.Write);
		file.StoreVar(cumCoins);//1
		file.StoreVar(defaultAmbatukam);//2
		file.StoreVar(edgeUpgrade);//3
		file.StoreVar(mewUpgrade);//4
		file.StoreVar(goonUpgrade);//5
		file.StoreVar(jelqingUpgrade);//6
		file.StoreVar(warVeteranUpgrade);//7
		file.StoreVar(warVeteran);//8
		file.StoreVar(cumUpgrade);//9
		file.StoreVar(superSayian);//10
		file.StoreVar(loopIndex);//11
		file.StoreVar(cumSkinBought);//12
		file.StoreVar(pissSkinBought);//13
		file.StoreVar(cumCoinsAddValue);//14
		file.StoreVar(cumCoinsPassiveValue);//15
		file.StoreVar(passiveIncomeUnlocked);//16
		file.StoreVar(ambatukamPoints);//17
		file.StoreVar(ambatukamGod);//18
		file.StoreVar(ambatukamGodUpgrade);//19
	}
	public static void Load(){
		using var file = Godot.FileAccess.Open(SavePath,Godot.FileAccess.ModeFlags.Read);
		if (Godot.FileAccess.FileExists(SavePath)){
			cumCoins = (int)file.GetVar();//1
			defaultAmbatukam = (bool)file.GetVar();//2
			edgeUpgrade = (bool)file.GetVar();//3
			mewUpgrade = (bool)file.GetVar();//4
			goonUpgrade = (bool)file.GetVar();//5
			jelqingUpgrade = (bool)file.GetVar();//6
			warVeteranUpgrade = (bool)file.GetVar();//7
			warVeteran = (bool)file.GetVar();//8
			cumUpgrade = (bool)file.GetVar();//9
			superSayian = (bool)file.GetVar();//10
			loopIndex = (int)file.GetVar();//11
			cumSkinBought = (bool)file.GetVar();//12
			pissSkinBought = (bool)file.GetVar();//13
			cumCoinsAddValue = (int)file.GetVar();//14
			cumCoinsPassiveValue = (int)file.GetVar();//15
			passiveIncomeUnlocked = (bool)file.GetVar();//16
			ambatukamPoints = (int)file.GetVar();//17
			ambatukamGod = (bool)file.GetVar();//18
			ambatukamGodUpgrade = (bool)file.GetVar();//19
		}
	}
	public static void NewGame(){
		defaultAmbatukam = true;
		cumCoins = 0;
		edgeUpgrade = false;
		mewUpgrade = false;
		goonUpgrade = false;
		jelqingUpgrade = false;
		warVeteranUpgrade = false;
		SuperSayianUpgrade = false;
		pissSkinBought = false;
		cumUpgrade = false;
		lvl5Achieved = false;
		warVeteran = false;
		superSayian = false;
		passiveIncomeUnlocked = false;
		cumCoinsPassiveValue = 0;
		cumCoinsAddValue = 1;
		cumSkinBought = false;
		loopIndex = -1;
		ambatukamGod = false;
		ambatukamGodUpgrade = false;
		//important shit under here
		ambatukamPoints++;
		//important shit above here
	}
}
