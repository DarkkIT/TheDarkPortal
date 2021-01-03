namespace TheDarkPortal.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using TheDarkPortal.Data.Models;
    using TheDarkPortal.Services.Mapping;
    using TheDarkPortal.Web.ViewModels.User;

    public abstract class BaseUserStatsViewModel : IMapFrom<ApplicationUser> /*IHaveCustomMappings*/
    {
        public UserCurrencyViewModel Currencies { get; set; }

        //public void CreateMappings(IProfileExpression configuration)
        //{
        //    configuration.CreateMap<ApplicationUser, BaseUserStatsViewModel>()
        //        .ForMember(s => s.Currencies.Silver, t => t.MapFrom(x => x.Silver))
        //        .ForMember(s => s.Currencies.Gold, t => t.MapFrom(x => x.Gold))
        //        .ForMember(s => s.Currencies.Silver, t => t.MapFrom(x => x.Platinum));
        //}
    }
}
