using BTD_Mod_Helper.Api.Bloons;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Rounds;
using Il2CppAssets.Scripts.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quests.RoundSets
{
    public static class MoabCamp
    {
        static RoundModel[] rounds = Game.instance.model.roundSets[1].rounds;
        public static RoundModel[] GetRounds()
        {
            rounds[19].ClearBloonGroups();
            rounds[19].AddBloonGroup("Lead", 10, 0, 100);
            rounds[19].AddBloonGroup("Ceramic", 5, 1, 100);
            return rounds;
        }
    }
}
