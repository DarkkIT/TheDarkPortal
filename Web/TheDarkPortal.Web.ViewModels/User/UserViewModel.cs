﻿namespace TheDarkPortal.Web.ViewModels.User
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using TheDarkPortal.Data.Models;
    using TheDarkPortal.Services.Mapping;
    using TheDarkPortal.Web.ViewModels.Card;

    public class UserViewModel : BaseUserStatsViewModel, IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public int Gold { get; set; }

        public string NickName { get; set; }

        public int ArenaPoints { get; set; }

        public IEnumerable<CardViewModel> UserBattleCards { get; set; }
    }
}
