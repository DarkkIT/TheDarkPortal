namespace TheDarkPortal.Services.Data.OfflineBattle
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using TheDarkPortal.Data.Common.Repositories;
    using TheDarkPortal.Data.Models;
    using TheDarkPortal.Services.Mapping;
    using TheDarkPortal.Web.ViewModels.OfflineBattle;

    public class OfflineBattleService : IOfflineBattleService
    {
        private readonly IRepository<Card> cardRepositiry;
        private readonly IRepository<UserCard> userCardRepositiry;

        public OfflineBattleService(
            IRepository<Card> cardRepositiry,
            IRepository<UserCard> userCardRepositiry)
        {
            this.cardRepositiry = cardRepositiry;
            this.userCardRepositiry = userCardRepositiry;
        }

        public IEnumerable<BattleCardViewModel> GetUserCards<T>(string userId)
        {
            var userCards = this.userCardRepositiry.All().Where(x => x.UserId == userId).ToList();

            var attackerBattleCards = new List<BattleCardViewModel>();

            foreach (var userCard in userCards)
            {
                var cardId = userCard.Id;

                if (this.cardRepositiry.All().FirstOrDefault(x => x.Id == cardId).IsBattleSetCard == true)
                {
                    var card = this.cardRepositiry.All().FirstOrDefault(x => x.Id == cardId);

                    var battleCard = new BattleCardViewModel
                    {
                        Name = card.Name,
                        Attack = card.Attack,
                        Defense = card.Defense,
                        Health = card.Health,
                        Tire = card.Tire,
                        Level = card.Level,
                        Element = card.Element,
                    };

                    attackerBattleCards.Add(battleCard);
                }
            }

            return attackerBattleCards;
        }
    }
}
