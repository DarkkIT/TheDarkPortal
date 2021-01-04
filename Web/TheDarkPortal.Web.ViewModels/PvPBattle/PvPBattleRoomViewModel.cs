namespace TheDarkPortal.Web.ViewModels.PvPBattle
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using TheDarkPortal.Web.ViewModels.Card;

    public class PvPBattleRoomViewModel : BaseUserStatsViewModel
    {

        public BattleRoomDataViewModel BattleRoom { get; set; }

        public IEnumerable<CardViewModel> FirstPlayerBattleCards { get; set; }

        public IEnumerable<CardViewModel> SecondPlayerBattleCards { get; set; }
    }
}
