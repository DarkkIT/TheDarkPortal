namespace TheDarkPortal.Services.Data.OfflineBattle
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using TheDarkPortal.Web.ViewModels.OfflineBattle;

    public interface IOfflineBattleService
    {
        Task SaveAttackerCards(string attackerId);

        Task SaveDefenderCards(string defenderId, string attackerId);

        IEnumerable<BattleCardViewModel> GetAttackerCards<T>(string attackerId);

        IEnumerable<BattleCardViewModel> GetDefenderCards<T>(string attackerId);

        void DeleteTempCards(string attackerId);

        Task AttackerSelectCard(int id, string attackerId);

        Task DefenderSelectCard(string attackerId);

        Task AttackDefenderCard(int id, string attackerId);

        Task HaveAttackerTurns(string attackerId);

        Task DefenderAttack(string attackerId);

        string IsBattleEnd(string attackerId);

        Task IncreaseArenaPoints(string attackerId);

        Task DecreaseArenaPoints(string attackerId);
    }
}
