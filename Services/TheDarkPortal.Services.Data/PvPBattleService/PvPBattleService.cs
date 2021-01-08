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
            battleRoom.IsAttackerTurn = !battleRoom.IsAttackerTurn;

            defendingCard.Health -= attackingCard.Attack;
            attackingCard.Health -= defendingCard.Defense;

            if (attackingCard.Health <= 0)
            {
                attackingCard.IsDestroyed = true;
            }

            attackingCard.HaveTakenTurn = true;
            attackingCard.IsSelected = false;

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
                    AttackerId = x.AttackerId,
                    DefenderId = x.DefenderId,
                    RoomId = x.Id,
                    isAttackerTurn = x.IsAttackerTurn,
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
           return this.battleRoomRepository.All().Any(x => x.AttackerId == userId || x.DefenderId == userId);
        }

        public async Task RemoveFinishedBattleTempData(int roomId)
        {
            var playerOneId = this.battleRoomRepository.All().Where(x => x.Id == roomId).Select(x => x.AttackerId).FirstOrDefault();
            var playerTwoId = this.battleRoomRepository.All().Where(x => x.Id == roomId).Select(x => x.DefenderId).FirstOrDefault();

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
            var attacker = this.userRepository.All().FirstOrDefault(x => x.Id == firstUserId);
            var defender = this.userRepository.All().FirstOrDefault(x => x.Id == secondUserId);

            var battleRoom = new BattleRoom() { Attacker = attacker, Defender = defender, IsAttackerTurn = true };

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

                var userBattleCard = new UserBattleCard { User = attacker, BattleCard = tempBattleCard };
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

                var userBattleCard = new UserBattleCard { User = defender, BattleCard = tempBattleCard };
                await this.userBattleCardRepository.AddAsync(userBattleCard);
            }

            await this.userBattleCardRepository.SaveChangesAsync();
            await this.battleRoomRepository.SaveChangesAsync();
            return battleRoom.Id;
        }
    }
}
