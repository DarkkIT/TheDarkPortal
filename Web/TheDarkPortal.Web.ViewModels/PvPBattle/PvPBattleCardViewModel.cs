namespace TheDarkPortal.Web.ViewModels.PvPBattle
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using TheDarkPortal.Data.Models;
    using TheDarkPortal.Data.Models.Cards;
    using TheDarkPortal.Services.Mapping;

    public class PvPBattleCardViewModel : IMapFrom<BattleCard>
    {
        public int Id { get; set; }

        //public int CardId { get; set; }

        public string Name { get; set; }

        public int Tire { get; set; }

        public int Level { get; set; }

        public double Power => Math.Floor((this.Attack + this.Defense + this.Health) / 3);

        public double Attack { get; set; }

        public double Defense { get; set; }

        public double Health { get; set; }

        public string Element { get; set; }

        public bool IsAttacker { get; set; }

        public bool IsDestroyed { get; set; }

        public bool HaveTakenTurn { get; set; }

        public bool IsSelected { get; set; }

        public string ImagePath => "/images/" + this.Name + "-" + this.Tire + ".jpg";

        //public void CreateMappings(IProfileExpression configuration)
        //{
        //    configuration.CreateMap<BattleCard, PvPBattleCardViewModel>()
        //        .ForMember(s => s.Id, t => t.MapFrom(x => x.Id))
        //        .ForMember(s => s.IsAttacker, t => t.MapFrom(x => x.IsAttacker))
        //        .ForMember(s => s.HaveTakenTurn, t => t.MapFrom(x => x.HaveTakenTurn))
        //        .ForMember(s => s.IsSelected, t => t.MapFrom(x => x.IsSelected))
        //        .ForMember(s => s.Power, t => t.MapFrom(x => x.Power))
        //        .ForMember(s => s.Attack, t => t.MapFrom(x => x.Attack))
        //        .ForMember(s => s.Defense, t => t.MapFrom(x => x.Defense))
        //        .ForMember(s => s.Health, t => t.MapFrom(x => x.Health))
        //        .ForMember(s => s.Element, t => t.MapFrom(x => x.Element))
        //        .ForMember(s => s.Tire, t => t.MapFrom(x => x.Tire))
        //        .ForMember(s => s.Name, t => t.MapFrom(x => x.Name))
        //        .ForMember(s => s.IsDestroyed, t => t.MapFrom(x => x.IsDestroyed))
        //        .ForMember(s => s.Level, t => t.MapFrom(x => x.Level));
        //}
    }
}
