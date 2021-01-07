namespace TheDarkPortal.Services.Data.OfflineBattle
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TheDarkPortal.Data.Common.Repositories;
    using TheDarkPortal.Data.Models;
    using TheDarkPortal.Services.Mapping;
    using TheDarkPortal.Web.ViewModels.OfflineBattle;

    public class OfflineBattleService : IOfflineBattleService
    {
        private readonly IRepository<Card> cardRepositiry;
        private readonly IRepository<UserCard> userCardRepositiry;
        private readonly IRepository<TempBattleCards> tempBattleCardsRepository;

        public OfflineBattleService(
            IRepository<Card> cardRepositiry,
            IRepository<UserCard> userCardRepositiry,
            IRepository<TempBattleCards> tempBattleCardsRepository)
        {
            this.cardRepositiry = cardRepositiry;
            this.userCardRepositiry = userCardRepositiry;
            this.tempBattleCardsRepository = tempBattleCardsRepository;
        }

        public void DeleteTempCards()
        {
            var cards = this.tempBattleCardsRepository.All();

            foreach (var item in cards)
            {
                this.tempBattleCardsRepository.Delete(item);
            }
        }

        public IEnumerable<BattleCardViewModel> GetAttackerCards<T>()
        {
            var attakerCards = this.tempBattleCardsRepository.All().Where(x => x.IsAttacker == true).To<BattleCardViewModel>().ToList();

            return attakerCards;
        }

        public IEnumerable<BattleCardViewModel> GetDefenderCards<T>()
        {
            var defenderCards = this.tempBattleCardsRepository.All().Where(x => x.IsAttacker == false).To<BattleCardViewModel>().ToList();

            return defenderCards;
        }

        public async Task SaveAttackerCards(string userId)
        {
            var userCards = this.userCardRepositiry.All().Where(x => x.UserId == userId).ToList();

            foreach (var userCard in userCards)
            {
                var cardId = userCard.Id;

                if (this.cardRepositiry.All().FirstOrDefault(x => x.Id == cardId).IsBattleSetCard == true)
                {
                    var card = this.cardRepositiry.All().FirstOrDefault(x => x.Id == cardId);

                    var battleCard = new TempBattleCards
                    {
                        CardId = card.Id,
                        Name = card.Name,
                        Attack = card.Attack,
                        Defense = card.Defense,
                        Health = card.Health,
                        Tire = card.Tire,
                        Level = card.Level,
                        Element = card.Element,
                        IsAttacker = true,
                        IsSelected = false,
                        HaveTakenTurn = true,
                    };

                    await this.tempBattleCardsRepository.AddAsync(battleCard);
                    await this.tempBattleCardsRepository.SaveChangesAsync();
                }
            }
        }

        public async Task SaveDefenderCards(string userId)
        {
            var userCards = this.userCardRepositiry.All().Where(x => x.UserId == userId).ToList();

            foreach (var userCard in userCards)
            {
                var cardId = userCard.Id;

                if (this.cardRepositiry.All().FirstOrDefault(x => x.Id == cardId).IsBattleSetCard == true)
                {
                    var card = this.cardRepositiry.All().FirstOrDefault(x => x.Id == cardId);

                    var battleCard = new TempBattleCards
                    {
                        CardId = card.Id,
                        Name = card.Name,
                        Attack = card.Attack,
                        Defense = card.Defense,
                        Health = card.Health,
                        Tire = card.Tire,
                        Level = card.Level,
                        Element = card.Element,
                        IsAttacker = false,
                        IsSelected = false,
                        HaveTakenTurn = true,
                    };

                    await this.tempBattleCardsRepository.AddAsync(battleCard);
                    await this.tempBattleCardsRepository.SaveChangesAsync();
                }
            }
        }

        public async Task AttackerSelectCard(int id)
        {
            foreach (var item in this.tempBattleCardsRepository.All().Where(x => x.IsAttacker))
            {
                if (item.CardId == id)
                {
                    item.IsSelected = true;
                }
                else
                {
                    item.IsSelected = false;
                }
            }

            await this.tempBattleCardsRepository.SaveChangesAsync();
        }
    }
}
