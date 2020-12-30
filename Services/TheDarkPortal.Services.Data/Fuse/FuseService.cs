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

        public async Task AddToFuseCouple(int id, string userId)
        {
            var user = this.userRepository.All().FirstOrDefault(x => x.Id == userId);

            FuseCard card = this.userCardRepositiry.All().Where(x => x.CardId == id).Select(x => new FuseCard
            {
                Name = x.Card.Name,
                Power = x.Card.Power,
                Defense = x.Card.Defense,
                Health = x.Card.Health,
                Level = x.Card.Level,
                Tire = x.Card.Tire,
            }).FirstOrDefault();

            var fuseCouple = new UserFuseCouple
            {
                FuseCard = card,
                User = user,
            };

            await this.userFuseCoupleRepository.AddAsync(fuseCouple);
            await this.userFuseCoupleRepository.SaveChangesAsync();
        }
    }
}
