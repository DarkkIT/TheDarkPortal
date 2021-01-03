namespace TheDarkPortal.Services.Data.Cards
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using TheDarkPortal.Common;
    using TheDarkPortal.Data.Common.Repositories;
    using TheDarkPortal.Data.Models;
    using TheDarkPortal.Data.Models.Cards;
    using TheDarkPortal.Services.Mapping;
    using TheDarkPortal.Web.ViewModels.Card;

    public class CardService : ICardService
    {
        private readonly IDeletableEntityRepository<CardLevelOne> cardLevelOneRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IRepository<Card> cardRepository;
        private readonly IRepository<UserCard> userCardRepository;
        private readonly IRepository<UserFuseCouple> userFuseCoupleRepository;

        public CardService(
            IDeletableEntityRepository<CardLevelOne> cardLevelOneRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IRepository<Card> cardRepository,
            IRepository<UserCard> userCardRepository,
            IRepository<UserFuseCouple> userFuseCoupleRepository)
        {
            this.cardLevelOneRepository = cardLevelOneRepository;
            this.userRepository = userRepository;
            this.cardRepository = cardRepository;
            this.userCardRepository = userCardRepository;
            this.userFuseCoupleRepository = userFuseCoupleRepository;
        }

        public async Task AddCardToMyCards(int cardId, string userId)
        {
            var user = this.userRepository.All().FirstOrDefault(x => x.Id == userId);
            var cardLevelOne = this.cardLevelOneRepository.All().FirstOrDefault(x => x.Id == cardId);

            if (user.Gold >= (int)cardLevelOne.Price)
            {
                user.Gold -= (int)cardLevelOne.Price;

                var card = new Card
                {
                    Name = cardLevelOne.Name,
                    Tire = cardLevelOne.Tire,
                    Level = cardLevelOne.Level,
                    LevelPrice = cardLevelOne.LevelPrice,
                    Power = cardLevelOne.Power,
                    Defense = cardLevelOne.Defense,
                    Health = cardLevelOne.Health,
                    Element = cardLevelOne.Element,
                    Price = cardLevelOne.Price,
                };

                var userCard = new UserCard
                {
                    Card = card,
                    UserId = user.Id,
                };

                await this.userCardRepository.AddAsync(userCard);
                await this.userCardRepository.SaveChangesAsync();
                await this.userRepository.SaveChangesAsync();
            }
            else
            {
                return;
            }
        }

        public IEnumerable<CardViewModel> GetAllCards<T>(int page, int itemsPerPage)
        {
            var model = this.cardLevelOneRepository.All().OrderByDescending(x => x.Name).Skip((page - 1) * itemsPerPage).Take(itemsPerPage).To<CardViewModel>().ToList();

            return model;
        }

        public IEnumerable<CardViewModel> GetAllSearchedCards<T>(int page, int itemsPerPage, string searchString)
        {
            var model = this.cardLevelOneRepository.All().Where(x => x.Name.Contains(searchString) || x.Name.Contains(searchString)).OrderByDescending(x => x.Id).Skip((page - 1) * itemsPerPage).Take(itemsPerPage).To<CardViewModel>().ToList();

            return model;
        }

        public int GetCount()
        {
            return this.cardLevelOneRepository.All().Count();
        }

        public int GetUserCardCount(string userId)
        {
            return this.userCardRepository.All().Where(x => x.UserId == userId).Count();
        }

        public IEnumerable<CardViewModel> GetUserCardsCollection<T>(int page, int itemsPerPage, string userId)
        {
            var user = this.userRepository.All().FirstOrDefault(x => x.Id == userId);

            var cards = this.userCardRepository.All()
                .Where(x => x.UserId == userId)
                .Select(x => new CardLevelOne
                {
                    Id = x.Card.Id,
                    Name = x.Card.Name,
                    Tire = x.Card.Tire,
                    Level = x.Card.Level,
                    LevelPrice = x.Card.LevelPrice,
                    Power = x.Card.Power,
                    Defense = x.Card.Defense,
                    Health = x.Card.Health,
                    Element = x.Card.Element,
                    Price = x.Card.Price,
                    IsBattleSetCard = x.Card.IsBattleSetCard,
                })
                .To<CardViewModel>()
                .OrderByDescending(x => x.Tire)
                .ThenByDescending(x => x.Name)
                .ThenByDescending(x => x.Level)
                .ToList();

            return cards;
        }

        public async Task DeleteCard(int id, string userId)
        {
            var userCard = this.userCardRepository.All().FirstOrDefault(x => x.UserId == userId && x.CardId == id);
            var cardId = userCard.CardId;

            var userFuseCouple = this.userFuseCoupleRepository.All().FirstOrDefault(x => x.CardId == cardId);

            if (userFuseCouple != null)
            {
                this.userFuseCoupleRepository.Delete(userFuseCouple);
                await this.userFuseCoupleRepository.SaveChangesAsync();
            }

            this.userCardRepository.Delete(userCard);
            await this.userCardRepository.SaveChangesAsync();

            var card = this.cardRepository.All().FirstOrDefault(x => x.Id == cardId);

            this.cardRepository.Delete(card);
            await this.cardRepository.SaveChangesAsync();
        }

        public CardViewModel CardDetails(int id)
        {
            var card = this.cardRepository.All().FirstOrDefault(x => x.Id == id);

            var cardViewModel = new CardViewModel
            {
                Id = card.Id,
                Name = card.Name,
                Level = card.Level,
                LevelPrice = card.LevelPrice,
                Tire = card.Tire,
                Power = card.Power,
                Defense = card.Defense,
                Health = card.Health,
                Element = card.Element,
            };

            return cardViewModel;
        }

        public async Task LevelUp(int id, string userId)
        {
            var card = this.cardRepository.All().FirstOrDefault(x => x.Id == id);
            var user = this.userRepository.All().FirstOrDefault(x => x.Id == userId);

            if (card.Level >= GlobalConstants.CardMinLevel && card.Level < GlobalConstants.CardMaxLevel)
            {
                if (user.Silver >= (int)Math.Floor(card.LevelPrice))
                {
                    card.Level += 1;
                    user.Silver -= (int)Math.Floor(card.LevelPrice);

                    card.LevelPrice = (int)Math.Floor(card.LevelPrice * 1.08);
                    card.Power = Math.Floor(card.Power * 1.02);
                    card.Defense = Math.Floor(card.Defense * 1.02);
                    card.Health = Math.Floor(card.Health * 1.02);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }

            await this.cardRepository.SaveChangesAsync();
            await this.userRepository.SaveChangesAsync();
        }

        public async Task AddCardToBattleCardsSet(int cardId, string userId)
        {

            var user = this.userRepository.All().FirstOrDefault(x => x.Id == userId);

            //TO DO: Verify this user is allowed to change the card status

            var card = this.cardRepository.All().FirstOrDefault(x => x.Id == cardId);
            var cardUser = this.userCardRepository.All().FirstOrDefault(x => x.UserId == userId && x.CardId == cardId);

            if (cardUser == null)
            {
                return;
            }

            var battleSetCardsCount = this.userCardRepository
                .All()
                .Where(x => x.UserId == userId && x.Card.IsBattleSetCard == true)
                .Count();

            if (card.IsBattleSetCard == true)
            {
                throw new ArgumentException("That card is already part of your battle set!");
            }

            if (battleSetCardsCount >= 4)
            {
                throw new ArgumentException("You can't have more than 4 cards your battle set!");
            }

            card.IsBattleSetCard = true;

            await this.cardRepository.SaveChangesAsync();
        }

        public async Task RemoveCardFromBattleCardsSet(int cardId, string userId)
        {

            var user = this.userRepository.All().FirstOrDefault(x => x.Id == userId);

            //TO DO: Verify this user is allowed to change the card status

            var cardUser = this.userCardRepository.All().FirstOrDefault(x => x.UserId == userId && x.CardId == cardId);

            if (cardUser == null)
            {
                return;
            }

            var card = this.cardRepository.All().FirstOrDefault(x => x.Id == cardId);

            if (card.IsBattleSetCard == false)
            {
                throw new ArgumentException("That card is not part of your battle set!");
            }

            card.IsBattleSetCard = false;

            await this.cardRepository.SaveChangesAsync();
        }
    }
}
