namespace TheDarkPortal.Services.Data.Fuse
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using TheDarkPortal.Data.Common.Repositories;
    using TheDarkPortal.Data.Models;
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

                    var cardOneName = this.cardRepositiry.All().FirstOrDefault(x => x.Id == cardOneId);

                    if (cardOneName.Name == card.Name && cardOneId != id)
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
