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

        public void DeleteTempCards(string attackerId)
        {
            var cards = this.tempBattleCardsRepository.All();

            foreach (var item in cards)
            {
                if (item.UniqueTag == attackerId)
                {
                    this.tempBattleCardsRepository.Delete(item);
                }
            }
        }

        public IEnumerable<BattleCardViewModel> GetAttackerCards<T>(string attackerId)
        {
            var attakerCards = this.tempBattleCardsRepository.All().Where(x => x.IsAttacker == true && x.UniqueTag == attackerId).To<BattleCardViewModel>().ToList();

            return attakerCards;
        }

        public IEnumerable<BattleCardViewModel> GetDefenderCards<T>(string attackerId)
        {
            var defenderCards = this.tempBattleCardsRepository.All().Where(x => x.IsAttacker == false && x.UniqueTag == attackerId).To<BattleCardViewModel>().ToList();

            return defenderCards;
        }

        public async Task SaveAttackerCards(string attackerId)
        {
            var userCards = this.userCardRepositiry.All().Where(x => x.UserId == attackerId).ToList();

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
                        HaveTakenTurn = false,
                        UniqueTag = attackerId,
                    };

                    await this.tempBattleCardsRepository.AddAsync(battleCard);
                    await this.tempBattleCardsRepository.SaveChangesAsync();
                }
            }
        }

        public async Task SaveDefenderCards(string defenderId, string attackerId)
        {
            var userCards = this.userCardRepositiry.All().Where(x => x.UserId == defenderId).ToList();

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
                        HaveTakenTurn = false,
                        UniqueTag = attackerId,
                    };

                    await this.tempBattleCardsRepository.AddAsync(battleCard);
                    await this.tempBattleCardsRepository.SaveChangesAsync();
                }
            }
        }

        public async Task AttackerSelectCard(int id, string attackerId)
        {
            foreach (var item in this.tempBattleCardsRepository.All().Where(x => x.IsAttacker && x.UniqueTag == attackerId))
            {
                if (item.CardId == id && item.HaveTakenTurn == false)
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

        public async Task AttackDefenderCard(int id, string attackerId)
        {
            var attackerCard = this.tempBattleCardsRepository.All().FirstOrDefault(x => x.UniqueTag == attackerId && x.IsSelected == true);

            var defenderCard = this.tempBattleCardsRepository.All().FirstOrDefault(x => x.UniqueTag == attackerId && x.CardId == id);

            defenderCard.Health -= attackerCard.Attack;
            attackerCard.HaveTakenTurn = true;
            attackerCard.IsSelected = false;

            if (defenderCard.Health <= 0)
            {
                defenderCard.Health = 0;
                defenderCard.IsDestroyed = true;
            }

            await this.tempBattleCardsRepository.SaveChangesAsync();
        }

        public async Task HaveAttackerTutns(string attackerId)
        {
            if (!this.tempBattleCardsRepository.All().Any(x => x.UniqueTag == attackerId && x.IsAttacker && x.HaveTakenTurn == false))
            {
                foreach (var card in this.tempBattleCardsRepository.All().Where(x => x.UniqueTag == attackerId && x.IsAttacker))
                {
                    card.HaveTakenTurn = false;
                }
            }

            await this.tempBattleCardsRepository.SaveChangesAsync();
        }
    }
}
