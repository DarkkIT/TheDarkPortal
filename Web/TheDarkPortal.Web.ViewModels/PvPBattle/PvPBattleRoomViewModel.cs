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

        public IEnumerable<CardViewModel> FirstPlayerBattleCards { get; set; }

        public IEnumerable<CardViewModel> SecondPlayerBattleCards { get; set; }

        public CardViewModel SelectedCard =>
            this.BattleRoom.IsFirstPlayerTurn == true ?
            this.FirstPlayerBattleCards.FirstOrDefault(x => x.IsSelected == true) :
            this.SecondPlayerBattleCards.FirstOrDefault(x => x.IsSelected == true);

    }
}
