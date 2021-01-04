namespace TheDarkPortal.Services.Data.PvPBattleService
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using TheDarkPortal.Web.ViewModels.Card;
    using TheDarkPortal.Web.ViewModels.PvPBattle;

    public interface IPvPBattleService
    {
         Task<int> SetUpBattleRoom(string firstUserId, string secondUserId);

         Task RemoveFinishedBattleTempData(int roomId);

         IEnumerable<CardViewModel> GetUserCardsCollection<T>(string userId);

         BattleRoomDataViewModel GetBattleRoomData(int roomId);

    }
}
