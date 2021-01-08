namespace TheDarkPortal.Web.ViewModels.PvPBattle
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using TheDarkPortal.Web.ViewModels.Card;

    public class BattleRoomDataViewModel
    {
        public int RoomId { get; set; }

        public string AttackerId { get; set; }

        public string DefenderId { get; set; }

        public bool isAttackerTurn { get; set; }

    }
}
