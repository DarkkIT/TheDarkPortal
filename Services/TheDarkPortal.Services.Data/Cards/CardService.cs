namespace TheDarkPortal.Services.Data.Cards
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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

        public CardService(
            IDeletableEntityRepository<CardLevelOne> cardLevelOneRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IRepository<Card> cardRepository,
            IRepository<UserCard> userCardRepository)
        {
            this.cardLevelOneRepository = cardLevelOneRepository;
            this.userRepository = userRepository;
            this.cardRepository = cardRepository;
            this.userCardRepository = userCardRepository;
        }

        public async Task AddCardToMyCards(int cardId, string userId)
        {
            var user = this.userRepository.All().FirstOrDefault(x => x.Id == userId);
            var cardLevelOne = this.cardLevelOneRepository.All().FirstOrDefault(x => x.Id == cardId);

            if (user.Silver >= (int)cardLevelOne.Price)
            {
                user.Silver -= (int)cardLevelOne.Price;

                var card = new Card
                {
                    Name = cardLevelOne.Name,
                    Tire = cardLevelOne.Tire,
                    Level = cardLevelOne.Level,
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
                    Power = Math.Floor((x.Card.Power + (x.Card.Level * 100)) * x.Card.Tire),
                    Defense = Math.Floor((x.Card.Defense + (x.Card.Level * 100)) * x.Card.Tire),
                    Health = Math.Floor((x.Card.Health + (x.Card.Level * 200)) * x.Card.Tire),
                    Element = x.Card.Element,
                    Price = x.Card.Price,
                })
                .To<CardViewModel>()
                .ToList();

            return cards;
        }

        public async Task DeleteCard(int id, string userId)
        {
            var userCard = this.userCardRepository.All().FirstOrDefault(x => x.UserId == userId && x.CardId == id);
            var cardId = userCard.CardId;

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
                Tire = card.Tire,
                Power = card.Power,
                Defense = card.Defense,
                Health = card.Health,
                Element = card.Element,
            };

            return cardViewModel;
        }
    }
}
