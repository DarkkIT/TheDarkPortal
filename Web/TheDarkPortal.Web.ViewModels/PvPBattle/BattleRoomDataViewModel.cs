namespace TheDarkPortal.Web.ViewModels.PvPBattle
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using TheDarkPortal.Web.ViewModels.Card;

    public class BattleRoomDataViewModel
    {
        public int RoomId { get; set; }

        public string FirstUserId { get; set; }

        public string SecondUserId { get; set; }

        public bool IsFirstPlayerTurn { get; set; }


    }
}
