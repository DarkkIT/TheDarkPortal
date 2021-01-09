namespace TheDarkPortal.Services.Data.User
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using TheDarkPortal.Data.Common.Repositories;
    using TheDarkPortal.Data.Models;
    using TheDarkPortal.Web.ViewModels.User;

    public class UserService : IUserService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public UserService(IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.userRepository = userRepository;
        }

        public UserCurrencyViewModel GetUserCurrencies(string userId)
        {
            var user = this.userRepository.All().FirstOrDefault(x => x.Id == userId);

            var model = new UserCurrencyViewModel
            {
                Silver = user.Silver,
                Gold = user.Gold,
                Platinum = user.Platinum,
            };

            return model;
        }

        public UserViewModel GetUserInfo(string attackerId)
        {
            var user = this.userRepository.All().FirstOrDefault(x => x.Id == attackerId);

            var userViewModel = new UserViewModel
            {
                Email = user.Email,
                ArenaPoints = user.ArenaPoints,
                Gold = user.Gold,
            };

            return userViewModel;
        }
    }
}
