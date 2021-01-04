namespace TheDarkPortal.Services.Data.PvPBattleService
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
    using TheDarkPortal.Web.ViewModels.PvPBattle;

    public class PvPBattleService : IPvPBattleService
    {
        private readonly IDeletableEntityRepository<BattleRoom> battleRoomRepository;
        private readonly IRepository<UserCard> userCardRepository;
        private readonly IDeletableEntityRepository<UserBattleCard> userBattleCardRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IRepository<Card> cardRepository;
        private readonly IDeletableEntityRepository<BattleCard> battleCardRepository;

        public PvPBattleService(
            IDeletableEntityRepository<BattleRoom> battleRoomRepository,
            IRepository<UserCard> userCardRepository,
            IDeletableEntityRepository<BattleCard> battleCardRepository,
            IDeletableEntityRepository<UserBattleCard> userBattleCardRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IRepository<Card> cardRepository)
        {
            this.battleRoomRepository = battleRoomRepository;
            this.userCardRepository = userCardRepository;
            this.userBattleCardRepository = userBattleCardRepository;
            this.userRepository = userRepository;
            this.cardRepository = cardRepository;
            this.battleCardRepository = battleCardRepository;
        }

        public BattleRoomDataViewModel GetBattleRoomData(int roomId)
        {
            return this.battleRoomRepository.All()
                .Where(x => x.Id == roomId)
                .Select(x => new BattleRoomDataViewModel()
                {
                    FirstUserId = x.PlayerOneId,
                    SecondUserId = x.PlayerTwoId,
                    RoomId = x.Id
                }).FirstOrDefault();
        }

        public IEnumerable<CardViewModel> GetUserCardsCollection<T>(string userId)
        {
            //var user = this.userBattleCardRepository.All().FirstOrDefault(x => x.Id == userId);

            var cards = this.userBattleCardRepository.All()
                .Where(x => x.UserId == userId)
                .Select(x => new CardLevelOne
                {
                    Id = x.BattleCard.Id,
                    Name = x.BattleCard.Name,
                    Tire = x.BattleCard.Tire,
                    Level = x.BattleCard.Level,
                    Power = x.BattleCard.Power,
                    Defense = x.BattleCard.Defense,
                    Health = x.BattleCard.Health,
                    Element = x.BattleCard.Element,
                })
                .To<CardViewModel>()
                .ToList();

            return cards;
        }

        public Task RemoveFinishedBattleTempData(int roomId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> SetUpBattleRoom(string firstUserId, string secondUserId)
        {
            var firstUser = this.userRepository.All().FirstOrDefault(x => x.Id == firstUserId);
            var secondUser = this.userRepository.All().FirstOrDefault(x => x.Id == secondUserId);

            var battleRoom = new BattleRoom() { PlayerOne = firstUser, PlayerTwo = secondUser };

            await this.battleRoomRepository.AddAsync(battleRoom);

            var firstUserBattleCards = this.userCardRepository
              .All()
              .Where(x => x.UserId == firstUserId && x.Card.IsBattleSetCard)
              .Select(x => x.Card)
              .ToList();



            foreach (var card in firstUserBattleCards)
            {
                var tempBattleCard = new BattleCard
                {
                    Health = card.Health,
                    Defense = card.Defense,
                    Power = card.Power,
                    Element = card.Element,
                    Level = card.Level,
                    Name = card.Name,
                    Tire = card.Tire,
                };

                var userBattleCard = new UserBattleCard { User = firstUser, BattleCard = tempBattleCard };
                await this.userBattleCardRepository.AddAsync(userBattleCard);
            }

            var secondUserBattleCards = this.userCardRepository
              .All()
              .Where(x => x.UserId == secondUserId && x.Card.IsBattleSetCard)
              .Select(x => x.Card)
              .ToList();

            foreach (var card in secondUserBattleCards)
            {
                var tempBattleCard = new BattleCard
                {
                    Health = card.Health,
                    Defense = card.Defense,
                    Power = card.Power,
                    Element = card.Element,
                    Level = card.Level,
                    Name = card.Name,
                    Tire = card.Tire,
                };

                var userBattleCard = new UserBattleCard { User = secondUser, BattleCard = tempBattleCard };
                await this.userBattleCardRepository.AddAsync(userBattleCard);
            }


            await this.userBattleCardRepository.SaveChangesAsync();
            await this.battleRoomRepository.SaveChangesAsync();
            return battleRoom.Id;
        }

    }
}
