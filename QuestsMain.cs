using MelonLoader;
using BTD_Mod_Helper;
using Quests;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppAssets.Scripts.Data.Quests;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models;
using Il2CppSystem.Collections.Generic;
using Il2CppAssets.Scripts.Models.Towers.Mods;
using Il2CppAssets.Scripts.Models.Rounds;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppAssets.Scripts.Unity.UI_New;
using Il2CppAssets.Scripts.Models.Towers;
using UI = Il2CppAssets.Scripts.Unity.UI_New.UI;
using Il2CppAssets.Scripts.Simulation.Action;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts;
using Il2CppAssets.Scripts.Simulation.SMath;
using Harmony;
using Il2CppAssets.Scripts.Simulation.Towers;
using System.IO;
using BTD_Mod_Helper.Api;
using Il2CppSystem;
using System;
using Il2CppSystem.Threading.Tasks;
using System.Linq;

[assembly: MelonInfo(typeof(Quests.Main.QuestsMain), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace Quests.Main;

public class QuestsMain : BloonsTD6Mod
{
    public static bool PlayingQuest;
    public static bool StartingQuest;
    public static QuestData? Quest;
    public static SaveData SaveData;
    public override void OnApplicationStart()
    {
        ModHelper.Msg<QuestsMain>("Quests loaded!");
        LoadSave();
    }
    public override void OnNewGameModel(GameModel result, List<ModModel> mods)
    {
        if (StartingQuest)
        {
            result.continueEnabled = false;
            result.doubleCashAllowed = false;

            result.startRound = Quest.StartRound;
            result.endRound = Quest.EndRound;
            MissionSpecific(result);
        }
        else
        {
            PlayingQuest = false;
            Quest = null;
        }
    }
    public override void OnMatchStart()
    {
        base.OnMatchStart();
        if (StartingQuest)
        {
            StartOrRestart();
        }
        
    }
    public override void OnRestart()
    {
        if (Quest != null)
        StartOrRestart();
    }
    public override void OnVictory()
    {
        base.OnVictory();
        if (PlayingQuest)
        {
            PlayingQuest = false;
            if (SaveData.QuestingLevel == Quest.Level)
            {
                SaveData.QuestingLevel += 1;
                MelonLogger.Msg(SaveData.QuestingLevel);
            }
            SaveSave();
        }
    }
    public void LoadSave()
    {
        if (!Directory.Exists(MelonHandler.ModsDirectory + @"\QuestsData"))
        {
            Directory.CreateDirectory(MelonHandler.ModsDirectory + @"\QuestsData");
        }
        if (!File.Exists(MelonHandler.ModsDirectory + @"\QuestsData\Save.json"))
        {
            JsonSerializer.instance.SaveToFile<SaveData>(new SaveData(10), MelonHandler.ModsDirectory + @"\QuestsData\Save.json");
        }
        SaveData = JsonSerializer.instance.LoadFromFile<SaveData>(MelonHandler.ModsDirectory + @"\QuestsData\Save.json");
    }
    public void SaveSave()
    {
        JsonSerializer.instance.SaveToFile<SaveData>(SaveData, MelonHandler.ModsDirectory + @"\QuestsData\Save.json");
    }
    public static void MissionSpecific(GameModel game)
    {
        switch (Quest.QuestName)
        {
            case "Bloonarius's Troops":
                game.roundSets[1].rounds[59].ClearBloonGroups();
                game.roundSets[1].rounds[59].AddBloonGroup("Bloonarius1", 1, 0, 1);
                game.roundSets[1].rounds[59].AddBloonGroup("Ceramic", 15, 0, 10);
                    break;
            case "The Moab Camp":
                game.roundSets[1].rounds = Quests.RoundSets.MoabCamp.GetRounds();
                break;
        }
    }
    public void StartOrRestart()
    {
        InGame.instance.SetCash(Quest.StartCash);
        switch (Quest.QuestName)
        {
            case "Quincy in trouble":

                InGame.instance.GetTowerManager().CreateTower(Game.instance.model.GetTower(TowerType.Quincy), new Vector3(-44, 56, 0), InGame.Bridge.MyPlayerNumber, ObjectId.FromData(1), ObjectId.FromData(4294967295));
                break;

        }
        StartingQuest = false;
        MelonLogger.Msg(InGameData.CurrentGame.selectedMode);
    }
}

public interface QuestData  
{
    public int StartRound { get; set; }
    public int EndRound { get; set; }
    public int StartCash { get; set; }
    public bool KnowledgeAllowed { get; set; }

    public string QuestName { get; set; }

    public string QuestDescription { get; set; }

    public string Map { get; set; }
    public Difficulty Difficulty { get; set; }
    public string Mode { get; set; }

    public int Level { get; set; }

    public bool CustomRounds { get; set; }

}
public enum Difficulty
{
    Easy,
    Medium,
    Hard
}

public class SaveData
{
    public int QuestingLevel;

    public SaveData(int level)
    {
        QuestingLevel = level;
    }
}
