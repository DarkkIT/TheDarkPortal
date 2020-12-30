namespace TheDarkPortal.Web.ViewModels.User
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using TheDarkPortal.Data.Models;
    using TheDarkPortal.Services.Mapping;

    public class UserCurrencyViewModel ////: IMapFrom<ApplicationUser>
    {
        public int Silver { get; set; }

        public int Gold { get; set; }

        public int Platinum { get; set; }
    }
}
