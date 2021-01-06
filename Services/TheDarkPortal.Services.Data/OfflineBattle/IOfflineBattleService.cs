namespace TheDarkPortal.Services.Data.OfflineBattle
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using TheDarkPortal.Web.ViewModels.OfflineBattle;

    public interface IOfflineBattleService
    {
        IEnumerable<BattleCardViewModel> GetUserCards<T>(string userId);
    }
}
