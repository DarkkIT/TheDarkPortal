namespace TheDarkPortal.Services.Data.PvPBattleService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TheDarkPortal.Data.Common.Repositories;
    using TheDarkPortal.Data.Models;

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


            var secondUserBattleCards = this.userCardRepository
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
