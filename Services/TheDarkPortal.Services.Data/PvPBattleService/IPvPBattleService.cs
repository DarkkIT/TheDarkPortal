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

         IEnumerable<PvPBattleCardViewModel> GetUserBattleCards<T>(string userId);

         BattleRoomDataViewModel GetBattleRoomData(int roomId);

         Task Attack(int attackingCardId, int defendingCardId, string currentPlayerId, int battleRoomId);

         Task SelectCard(int cardId);

         Task<BattleCard> GetById();

         bool AllCardsHaveTakenTurn();
    }
}
