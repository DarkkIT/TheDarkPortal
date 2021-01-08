namespace TheDarkPortal.Web.ViewModels.PvPBattle
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using TheDarkPortal.Web.ViewModels.Card;

    public class PvPBattleRoomViewModel : BaseUserStatsViewModel
    {
        public BattleRoomDataViewModel BattleRoom { get; set; }

        public string CurrentUserId { get; set; }

        public IEnumerable<PvPBattleCardViewModel> FirstPlayerBattleCards { get; set; }

        public IEnumerable<PvPBattleCardViewModel> SecondPlayerBattleCards { get; set; }

        public PvPBattleCardViewModel SelectedCard =>
            this.BattleRoom.isAttackerTurn == true ?
            this.FirstPlayerBattleCards.FirstOrDefault(x => x.IsSelected == true) :
            this.SecondPlayerBattleCards.FirstOrDefault(x => x.IsSelected == true);

    }
}
