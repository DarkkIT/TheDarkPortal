namespace TheDarkPortal.Services.Data.PvPBattleService
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IPvPBattleService
    {
         Task<int> SetUpBattleRoom(string firstUserId, string secondUserId);

         Task RemoveFinishedBattleTempData(int roomId);
    }
}
