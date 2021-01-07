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
        private readonly IRepository<BattleRoom> battleRoomRepository;
        private readonly IRepository<UserCard> userCardRepository;
        private readonly IRepository<UserBattleCard> userBattleCardRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IRepository<Card> cardRepository;
        private readonly IRepository<BattleCard> battleCardRepository;

        public PvPBattleService(
            IRepository<BattleRoom> battleRoomRepository,
            IRepository<UserCard> userCardRepository,
            IRepository<BattleCard> battleCardRepository,
            IRepository<UserBattleCard> userBattleCardRepository,
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

        public bool AllCardsHaveTakenTurn()
        {
            throw new NotImplementedException();
        }

        public async Task Attack(int attackingCardId, int defendingCardId, string currentPlayerId, int battleRoomId)
        {
            var attackingCard = this.battleCardRepository.All().FirstOrDefault(x => x.Id == attackingCardId);
            var defendingCard = this.battleCardRepository.All().FirstOrDefault(x => x.Id == defendingCardId);
            var battleRoom = this.battleRoomRepository.All().Where(x => x.Id == battleRoomId).FirstOrDefault();
            battleRoom.IsFirstPlayerTurn = !battleRoom.IsFirstPlayerTurn;

            defendingCard.CurrentHealth = defendingCard.Health - attackingCard.Attack;
            attackingCard.CurrentHealth = attackingCard.Health - defendingCard.Defense;
            attackingCard.HaveTakenTurn = true;

            //// Check If all cards have taken turn ////
            var currentPlayerBattleCards = this.GetUserBattleCards<PvPBattleCardViewModel>(currentPlayerId);
            await this.battleCardRepository.SaveChangesAsync();
        }

        public BattleRoomDataViewModel GetBattleRoomData(int roomId)
        {
            return this.battleRoomRepository.All()
                .Where(x => x.Id == roomId)
                .Select(x => new BattleRoomDataViewModel()
                {
                    FirstUserId = x.PlayerOneId,
                    SecondUserId = x.PlayerTwoId,
                    RoomId = x.Id,
                    IsFirstPlayerTurn = true,
                }).FirstOrDefault();
        }

        public Task<BattleCard> GetById()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PvPBattleCardViewModel> GetUserBattleCards<T>(string userId)
        {
            var cards = this.userBattleCardRepository.All().Where(x => x.UserId == userId)
                .Select(x => new BattleCard
                {
                    Id = x.BattleCard.Id,
                    Name = x.BattleCard.Name,
                    Tire = x.BattleCard.Tire,
                    Level = x.BattleCard.Level,
                    Attack = x.BattleCard.Attack,
                    Defense = x.BattleCard.Defense,
                    Health = x.BattleCard.Health,
                    Element = x.BattleCard.Element,
                    IsSelected = x.BattleCard.IsSelected,
                    IsAttacker = x.BattleCard.IsAttacker,
                    HaveTakenTurn = x.BattleCard.HaveTakenTurn,
                })
                .To<PvPBattleCardViewModel>()
                .ToList();

            return cards;
        }

        public bool IsInBattle(string userId)
        {
           return this.battleRoomRepository.All().Any(x => x.PlayerOneId == userId || x.PlayerTwoId == userId);
        }

        public async Task RemoveFinishedBattleTempData(int roomId)
        {
            var playerOneId = this.battleRoomRepository.All().Where(x => x.Id == roomId).Select(x => x.PlayerOneId).FirstOrDefault();
            var playerTwoId = this.battleRoomRepository.All().Where(x => x.Id == roomId).Select(x => x.PlayerTwoId).FirstOrDefault();

            //// Remove Battle Room ////

            var battleRoom = this.battleRoomRepository.All().Where(x => x.Id == roomId).FirstOrDefault();
            this.battleRoomRepository.Delete(battleRoom);


            //// Get Players BattleCards ////

            var playerOneBattleCards = this.userBattleCardRepository
                .All()
                .Where(x => x.UserId == playerOneId)
                .Select(x => x.BattleCard)
                .ToList();

            var playerTwoBattleCards = this.userBattleCardRepository
                .All()
                .Where(x => x.UserId == playerTwoId)
                .Select(x => x.BattleCard)
                .ToList();

            //// Get UserBattleCards Relation Entities ////

            var playerOneUserBattleCards = this.userBattleCardRepository
                .All().Where(x => x.UserId == playerOneId)
                .ToList();
            var playerTwoUserBattleCards = this.userBattleCardRepository
                .All()
                .Where(x => x.UserId == playerTwoId)
                .ToList();

            //// Remove userBattleCard entity relations ////

            foreach (var userBattleCard in playerOneUserBattleCards)
            {
                this.userBattleCardRepository.Delete(userBattleCard);
            }

            foreach (var userBattleCard in playerTwoUserBattleCards)
            {
                this.userBattleCardRepository.Delete(userBattleCard);
            }

            //// Remove Both players Temporary BattleCards Entities ////
            foreach (var battleCard in playerOneBattleCards)
            {
                this.battleCardRepository.Delete(battleCard);
            }

            foreach (var battleCard in playerTwoBattleCards)
            {
                this.battleCardRepository.Delete(battleCard);
            }

            await this.battleRoomRepository.SaveChangesAsync();
            await this.userBattleCardRepository.SaveChangesAsync();
            await this.battleRoomRepository.SaveChangesAsync();
        }

        public async Task SelectCard(int cardId)
        {
            var battleCard = this.battleCardRepository.All().FirstOrDefault(x => x.Id == cardId);
            battleCard.IsSelected = true;
            await this.battleCardRepository.SaveChangesAsync();
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
                    Attack = card.Attack,
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
                    Attack = card.Attack,
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
