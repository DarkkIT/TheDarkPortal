namespace TheDarkPortal.Services.Data.Fuse
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

    public class FuseService : IFuseService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IRepository<UserCard> userCardRepositiry;
        private readonly IRepository<UserFuseCouple> userFuseCoupleRepository;
        private readonly IRepository<Card> cardRepositiry;

        public FuseService(
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IRepository<UserCard> userCardRepositiry,
            IRepository<UserFuseCouple> userFuseCoupleRepository,
            IRepository<Card> cardRepositiry)
        {
            this.userRepository = userRepository;
            this.userCardRepositiry = userCardRepositiry;
            this.userFuseCoupleRepository = userFuseCoupleRepository;
            this.cardRepositiry = cardRepositiry;
        }

        public async Task AddToFuse(int id, string userId)
        {
            var user = this.userRepository.All().FirstOrDefault(x => x.Id == userId);

            var card = this.cardRepositiry.All().FirstOrDefault(x => x.Id == id);

            if (this.userFuseCoupleRepository.All().Where(x => x.UserId == userId).Count() < 2)
            {
                if (this.userFuseCoupleRepository.All().Where(x => x.UserId == userId).Count() == 0)
                {
                    var fuseCouple = new UserFuseCouple
                    {
                        Card = card,
                        User = user,
                    };

                    await this.userFuseCoupleRepository.AddAsync(fuseCouple);
                }
                else
                {
                    var cardOneId = this.userFuseCoupleRepository.All().FirstOrDefault(x => x.UserId == userId).CardId;

                    var cardOne = this.cardRepositiry.All().FirstOrDefault(x => x.Id == cardOneId);

                    if (cardOne.Name == card.Name && cardOneId != id && cardOne.Tire == card.Tire)
                    {
                        var fuseCouple = new UserFuseCouple
                        {
                            Card = card,
                            User = user,
                        };

                        await this.userFuseCoupleRepository.AddAsync(fuseCouple);
                    }
                    else
                    {
                        return;
                    }
                }
            }
            else
            {
                return;
            }

            await this.userFuseCoupleRepository.SaveChangesAsync();
        }

        public async Task Fuse(string userId)
        {
            var user = this.userRepository.All().FirstOrDefault(x => x.Id == userId);

            var fuseCards = this.userFuseCoupleRepository.All().Where(x => x.UserId == userId).ToList();


            if (fuseCards.Count() == 2)
            {
                var firstId = fuseCards[0].CardId;
                var secondId = fuseCards[1].CardId;

                var firstCard = this.cardRepositiry.All().FirstOrDefault(x => x.Id == firstId);
                var secondCard = this.cardRepositiry.All().FirstOrDefault(x => x.Id == secondId);

                firstCard.Attack += secondCard.Attack;
                firstCard.Defense += secondCard.Defense;
                firstCard.Health += secondCard.Health;
                firstCard.Level = 1;
                firstCard.Tire += 1;

                foreach (var fuseCard in fuseCards)
                {
                    this.userFuseCoupleRepository.Delete(fuseCard);
                }

                var userCard = this.userCardRepositiry.All().FirstOrDefault(x => x.UserId == userId && x.CardId == secondId);

                this.userCardRepositiry.Delete(userCard);

                await this.userCardRepositiry.SaveChangesAsync();

                this.cardRepositiry.Delete(secondCard);

                await this.userFuseCoupleRepository.SaveChangesAsync();
                await this.userCardRepositiry.SaveChangesAsync();
            }
            else
            {
                return;
            }
        }

        public IEnumerable<FuseCardViewModel> GetUserFuseCards<T>(string userId)
        {
            var model = this.userFuseCoupleRepository.All().Where(x => x.UserId == userId).To<FuseCardViewModel>().ToList();

            return model;
        }

        public async Task RemoveFromFuse(int id, string userId)
        {
            var fuseCouple = this.userFuseCoupleRepository.All().FirstOrDefault(x => x.UserId == userId && x.CardId == id);

            this.userFuseCoupleRepository.Delete(fuseCouple);
            await this.userFuseCoupleRepository.SaveChangesAsync();
        }
    }
}
