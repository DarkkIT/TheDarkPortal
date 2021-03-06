﻿namespace TheDarkPortal.Services.Data.OfflineBattle
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using TheDarkPortal.Common;
    using TheDarkPortal.Data.Common.Repositories;
    using TheDarkPortal.Data.Models;
    using TheDarkPortal.Services.Mapping;
    using TheDarkPortal.Web.ViewModels.OfflineBattle;

    public class OfflineBattleService : IOfflineBattleService
    {
        private readonly IRepository<Card> cardRepositiry;
        private readonly IRepository<UserCard> userCardRepositiry;
        private readonly IRepository<TempBattleCards> tempBattleCardsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public OfflineBattleService(
            IRepository<Card> cardRepositiry,
            IRepository<UserCard> userCardRepositiry,
            IRepository<TempBattleCards> tempBattleCardsRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.cardRepositiry = cardRepositiry;
            this.userCardRepositiry = userCardRepositiry;
            this.tempBattleCardsRepository = tempBattleCardsRepository;
            this.userRepository = userRepository;
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
            foreach (var card in this.tempBattleCardsRepository.All().Where(x => x.IsAttacker && x.UniqueTag == attackerId && x.IsDestroyed == false))
            {
                if (card.CardId == id && card.HaveTakenTurn == false)
                {
                    card.IsSelected = true;
                }
                else
                {
                    card.IsSelected = false;
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
                defenderCard.HaveTakenTurn = true;
            }

            await this.tempBattleCardsRepository.SaveChangesAsync();
        }

        public async Task HaveAttackerTurns(string attackerId)
        {
            if (!this.tempBattleCardsRepository.All().Any(x => x.UniqueTag == attackerId && x.IsAttacker && x.HaveTakenTurn == false))
            {
                foreach (var card in this.tempBattleCardsRepository.All().Where(x => x.UniqueTag == attackerId && x.IsAttacker))
                {
                    if (card.IsDestroyed)
                    {
                        card.HaveTakenTurn = true;
                    }
                    else
                    {
                        card.HaveTakenTurn = false;
                    }
                }
            }

            await this.tempBattleCardsRepository.SaveChangesAsync();
        }

        public async Task DefenderSelectCard(string attackerId)
        {
            var defenderCardsCheker = this.tempBattleCardsRepository.All().Where(x => x.UniqueTag == attackerId && x.IsAttacker == false && x.IsDestroyed == false);

            foreach (var card in defenderCardsCheker)
            {
                card.IsSelected = false;
            }

            if (!defenderCardsCheker.Any(x => x.HaveTakenTurn == false))
            {
                foreach (var card in defenderCardsCheker)
                {
                    if (!card.IsDestroyed)
                    {
                        card.HaveTakenTurn = false;
                    }
                }
            }

            await this.tempBattleCardsRepository.SaveChangesAsync();

            var defenderCards = this.tempBattleCardsRepository.All().Where(x => x.UniqueTag == attackerId && x.IsAttacker == false && x.HaveTakenTurn == false);

            if (defenderCards.Any(x => x.HaveTakenTurn == false))
            {
                var selectedCard = defenderCards.FirstOrDefault();
                selectedCard.HaveTakenTurn = true;
                selectedCard.IsSelected = true;
            }
            else
            {
                foreach (var card in defenderCards)
                {
                    card.HaveTakenTurn = false;
                }
            }

            await this.tempBattleCardsRepository.SaveChangesAsync();
        }

        public async Task DefenderAttack(string attackerId)
        {
            int milliseconds = 500;
            Thread.Sleep(milliseconds);

            await this.DefenderSelectCard(attackerId);

            var defenderCard = this.tempBattleCardsRepository.All().FirstOrDefault(x => x.UniqueTag == attackerId && x.IsAttacker == false && x.IsSelected == true && x.IsDestroyed == false);

            var attackerCards = this.tempBattleCardsRepository.All().Where(x => x.UniqueTag == attackerId && x.IsAttacker == true && x.IsDestroyed == false);

            var attackerCard = attackerCards.OrderBy(_ => Guid.NewGuid()).First();

            attackerCard.Health -= defenderCard.Attack;

            if (attackerCard.Health <= 0)
            {
                attackerCard.Health = 0;
                attackerCard.IsDestroyed = true;
                attackerCard.HaveTakenTurn = true;
            }

            await this.tempBattleCardsRepository.SaveChangesAsync();
        }

        public string IsBattleEnd(string attackerId)
        {
            var defenderCardsCheker = this.tempBattleCardsRepository.All().Where(x => x.UniqueTag == attackerId && x.IsAttacker == false);
            var attackerCardsCheker = this.tempBattleCardsRepository.All().Where(x => x.UniqueTag == attackerId && x.IsAttacker == true);

            double defenderHealth = defenderCardsCheker.Sum(x => x.Health);
            double attackerHealth = attackerCardsCheker.Sum(x => x.Health);

            if (defenderHealth <= 0)
            {
                this.IncreaseArenaPoints(attackerId);

                int milliseconds = 500;
                Thread.Sleep(milliseconds);

                return "attackerWin";
            }
            else if (attackerHealth <= 0)
            {
                this.DecreaseArenaPoints(attackerId);

                int milliseconds = 500;
                Thread.Sleep(milliseconds);

                return "defenderWin";
            }
            else
            {
                return "noOneWin";
            }
        }

        public async Task IncreaseArenaPoints(string attackerId)
        {
            var user = this.userRepository.All().FirstOrDefault(x => x.Id == attackerId);

            int points = GlobalConstants.OfflineArenaPointReward;

            user.ArenaPoints += points;
            user.Gold += points * 100;

            await this.userRepository.SaveChangesAsync();
        }

        public async Task DecreaseArenaPoints(string attackerId)
        {
            var user = this.userRepository.All().FirstOrDefault(x => x.Id == attackerId);

            user.ArenaPoints -= GlobalConstants.OfflineArenaPointDecrease;

            await this.userRepository.SaveChangesAsync();
        }

        public string GetEnemyId(string enemyName)
        {
            var enemy = this.userRepository.All().FirstOrDefault(x => x.Email == enemyName);

            var enemyId = enemy.Id;

            return enemyId;
        }
    }
}
