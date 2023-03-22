using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Components;
using BTD_Mod_Helper.Extensions;
using UnityEngine;
using BTD_Mod_Helper.Api.Enums;
using TaskScheduler = BTD_Mod_Helper.Api.TaskScheduler;
using Il2CppAssets.Scripts.Unity.UI_New.ChallengeEditor;
using Il2CppAssets.Scripts.Utils;
using Il2CppAssets.Scripts.Unity.UI_New;
using Il2CppAssets.Scripts.Unity.Menu;
using Il2CppTMPro;
using Il2CppSystem;
using Quests;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using System;
using Il2CppAssets.Scripts.Simulation.Action;
using Action = Il2CppSystem.Action;
using UnityEngine.Events;
using Quests.Main;
using MelonLoader;

namespace ClassesMenuUI;
public class QuestSelect : ModGameMenu<ExtraSettingsScreen>
{
    // private static ClassesMenuClasses modTemplate;
    public ModHelperPanel panel;

    internal const int Padding = 50;

    internal const int MenuWidth = 3600;
    internal const int MenuHeight = 1900;

    public override string Name => "QuestSelect";
    public override bool OnMenuOpened(Il2CppSystem.Object data)
    {
        CommonForegroundScreen.instance.heading.SetActive(true);
        CommonForegroundHeader.SetText("Quests");
        var panelTransform = GameMenu.gameObject.GetComponentInChildrenByName<RectTransform>("Panel");
        var thingy = panelTransform.gameObject;
        thingy.DestroyAllChildren();
        panel = thingy.AddModHelperPanel(new Info("ScrollMenu", 3600, 1900));
        CreateMenu(panel);
        return false;
    }
    private void CreateMenu(ModHelperPanel ClassesMenu)
    {
        var menu = ClassesMenu.AddScrollPanel(new Info("Panel", 0, 0, MenuWidth, MenuHeight), RectTransform.Axis.Vertical, VanillaSprites.MainBGPanelBlue, Padding, Padding);
        menu.AddScrollContent(CreateQuest(new Quest1()));
        menu.AddScrollContent(CreateQuest(new Quest2()));
        menu.AddScrollContent(CreateQuest(new Quest3()));
    }
    private static void StartQuest(QuestData? data)
    {
        Quests.Main.QuestsMain.PlayingQuest = true;
        Quests.Main.QuestsMain.StartingQuest = true;
        Quests.Main.QuestsMain.Quest = data;
        InGameData.Editable.selectedMap = data.Map;
        InGameData.Editable.selectedDifficulty = data.Difficulty.ToString();
        InGameData.Editable.selectedMode = data.Mode;
        Il2CppAssets.Scripts.Unity.UI_New.UI.instance.LoadGame();
        QuestsButton.Hide();
    }
    private ModHelperPanel CreateQuest(QuestData data)
    {
        ModHelperPanel panel = ModHelperPanel.Create(new Info("QuestPanel", 3400, 1000), VanillaSprites.MainBGPanelGrey);
        panel.AddText(new Info("QuestName", -625, 200, 2000, 500), data.QuestName, 120, TextAlignmentOptions.TopLeft);
        panel.AddText(new Info("Description", -625, -200, 2000, 800), data.QuestDescription, 90, TextAlignmentOptions.TopLeft);
        panel.AddText(new Info("RoundsText", 1400, 400, 300, 150), "Rounds", 70);
        panel.AddText(new Info("Rounds", 1400, 300, 500, 150), data.StartRound + "-" + data.EndRound, 120);

        panel.AddText(new Info("CustomRounds", 950, 400, 700, 150), "Custom Rounds", 70);
        string customroundsicon = VanillaSprites.TickGreenIcon;
        if (!data.CustomRounds) { customroundsicon = VanillaSprites.AddRemoveBtn; }
        panel.AddImage(new Info("CustomRoundsImage", 950, 275, 150, 150), customroundsicon);
        
        if (data.Level <= QuestsMain.SaveData.QuestingLevel)
        {
            var button = panel.AddButton(new Info("StartQuestButton", 1500, -300, 350, 350), VanillaSprites.StartRoundIcon, null);
            button.Button.onClick.AddListener(() =>
            {
                StartQuest(data);
            });
        }
        else
        {
            panel.AddImage(new Info("StartQuestBlocked", 1500, -300, 350, 350), VanillaSprites.NoContinuesIcon);
        }
        return panel;
    }
    
}
