namespace TheDarkPortal.Services.Data.Arena
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using TheDarkPortal.Data.Common.Repositories;
    using TheDarkPortal.Data.Models;
    using TheDarkPortal.Services.Mapping;
    using TheDarkPortal.Web.ViewModels.Card;
    using TheDarkPortal.Web.ViewModels.User;

    public class ArenaService : IArenaService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IRepository<UserCard> userCardsRepository;
        private readonly IRepository<Card> cardRepository;

        public ArenaService(
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IRepository<UserCard> userCardsRepository,
            IRepository<Card> cardRepository)
        {
            this.userRepository = userRepository;
            this.userCardsRepository = userCardsRepository;
            this.cardRepository = cardRepository;
        }

        public IEnumerable<UserViewModel> GetAllArenaUsers<T>()
        {
            var arenaUsers = this.userRepository.All().Where(x => x.ArenaPoints >= 0).To<UserViewModel>().OrderByDescending(x => x.ArenaPoints).ToList();

            foreach (var arenaUser in arenaUsers)
            {
                var battleCards = new List<CardViewModel>();

                var userId = arenaUser.Id;

                var cards = this.userCardsRepository.All().Where(x => x.UserId == userId);

                foreach (var card in cards)
                {
                    var cardId = card.CardId;

                    var battleCard = this.cardRepository.All().FirstOrDefault(x => x.Id == cardId && x.IsBattleSetCard == true);

                    if (battleCard != null)
                    {
                        var cardViewModel = new CardViewModel
                        {
                            Name = battleCard.Name,
                            Tire = battleCard.Tire,
                        };

                        battleCards.Add(cardViewModel);
                    }
                }

                arenaUser.BattleCards = battleCards;
            }

            return arenaUsers;
        }
    }
}
