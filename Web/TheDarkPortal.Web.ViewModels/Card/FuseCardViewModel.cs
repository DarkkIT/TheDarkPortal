namespace TheDarkPortal.Web.ViewModels.Card
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using TheDarkPortal.Data.Models;
    using TheDarkPortal.Services.Mapping;

    public class FuseCardViewModel : IMapFrom<UserFuseCouple>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Tire { get; set; }

        public int Level { get; set; }

        public double LevelPrice { get; set; }

        public double Power { get; set; }

        public double Defense { get; set; }

        public double Health { get; set; }

        public string Element { get; set; }

        public string ImagePath => "/images/" + this.Name + "-" + this.Tire + ".jpg";

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<UserFuseCouple, FuseCardViewModel>().ForMember(
                m => m.Id,
                opt => opt.MapFrom(x => x.Card.Id)).ForMember(
                m => m.Name,
                opt => opt.MapFrom(x => x.Card.Name)).ForMember(
                m => m.Tire,
                opt => opt.MapFrom(x => x.Card.Tire)).ForMember(
                m => m.Level,
                opt => opt.MapFrom(x => x.Card.Level)).ForMember(
                m => m.LevelPrice,
                opt => opt.MapFrom(x => x.Card.LevelPrice)).ForMember(
                m => m.Power,
                opt => opt.MapFrom(x => x.Card.Power)).ForMember(
                m => m.Defense,
                opt => opt.MapFrom(x => x.Card.Defense)).ForMember(
                m => m.Health,
                opt => opt.MapFrom(x => x.Card.Health)).ForMember(
                m => m.Element,
                opt => opt.MapFrom(x => x.Card.Element));
        }
    }
}
