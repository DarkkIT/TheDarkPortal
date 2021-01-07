namespace TheDarkPortal.Services.Data.PvPBattleService
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using TheDarkPortal.Data.Models;
    using TheDarkPortal.Web.ViewModels.Card;
    using TheDarkPortal.Web.ViewModels.PvPBattle;

    public interface IPvPBattleService
    {
         Task<int> SetUpBattleRoom(string firstUserId, string secondUserId);

         Task RemoveFinishedBattleTempData(int roomId);

         bool IsInBattle(string userId);

         IEnumerable<CardViewModel> GetUserCardsCollection<T>(string userId);

         BattleRoomDataViewModel GetBattleRoomData(int roomId);

         Task Attack(int attackingCardId, int defendingCardId);

         Task SelectCard(int cardId);

         Task<BattleCard> GetById();

    }
}
