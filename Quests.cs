using Il2CppAssets.Scripts.Unity.Difficulty;
using Quests.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Difficulty = Quests.Main.Difficulty;

namespace Quests
{
    public class Quest1 : QuestData
    {
        public int StartRound { get; set; } = 10;
        public int EndRound { get; set; } = 30;
        public bool KnowledgeAllowed { get; set; } = true;
        public int StartCash { get; set; } = 1000;

        public string QuestName { get; set; } = "Quincy in trouble";

        public string QuestDescription { get; set; } = "Whilst walking through a forest trying to reach the frontline, Quincy got jumped by the bloon army. Since he was alone he called in backup.";

        public Difficulty Difficulty { get; set; } = Difficulty.Easy;

        public string Map { get; set; } = "TheCabin";

        public string Mode { get; set; } = "Standard";

        public int Level { get; set; } = 1;

        public bool CustomRounds { get; set; } = false;
    }
    public class Quest2 : QuestData
    {
        public int StartRound { get; set; } = 5;
        public int EndRound { get; set; } = 60;
        public bool KnowledgeAllowed { get; set; } = true;
        public int StartCash { get; set; } = 500;

        public string QuestName { get; set; } = "Bloonarius's Troops";

        public string QuestDescription { get; set; } = "On the way to the frontline, Quincy and the backup ran into Bloonarius and his troops. Its the perfect time to take Bloonarius down!";

        public Difficulty Difficulty { get; set; } = Difficulty.Hard;

        public string Map { get; set; } = "BloonariusPrime";

        public string Mode { get; set; } = "Standard";

        public int Level { get; set; } = 2;

        public bool CustomRounds { get; set; } = true;
    }
    public class Quest3 : QuestData
    {
        public int StartRound { get; set; } = 20;
        public int EndRound { get; set; } = 40;
        public bool KnowledgeAllowed { get; set; } = true;
        public int StartCash { get; set; } = 5000;

        public string QuestName { get; set; } = "The Moab Camp";

        public string QuestDescription { get; set; } = "Finishing the attack on Bloonarius's troops, Quincy noticed the stragglers running towards the main camp. This was the moab camp, where all the moabs lived. Quincy followed";

        public Difficulty Difficulty { get; set; } = Difficulty.Hard;

        public string Map { get; set; } = "Logs";

        public string Mode { get; set; } = "Standard";

        public int Level { get; set; } = 2;

        public bool CustomRounds { get; set; } = true;
    }
}
