namespace TheDarkPortal.Services.Data.OfflineBattle
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using TheDarkPortal.Web.ViewModels.OfflineBattle;

    public interface IOfflineBattleService
    {
        Task SaveAttackerCards(string userId);

        Task SaveDefenderCards(string userId);

        IEnumerable<BattleCardViewModel> GetAttackerCards<T>();

        IEnumerable<BattleCardViewModel> GetDefenderCards<T>();

        void DeleteTempCards();
    }
}
