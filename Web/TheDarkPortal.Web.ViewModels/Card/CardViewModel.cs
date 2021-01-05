namespace TheDarkPortal.Web.ViewModels.Card
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using TheDarkPortal.Data.Models.Cards;
    using TheDarkPortal.Services.Mapping;

    public class CardViewModel : BaseUserStatsViewModel, IMapFrom<CardLevelOne>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double PowerN { get; set; }

        public int Tire { get; set; }

        public int Level { get; set; }

        public double LevelPrice { get; set; }

        public double Power => Math.Floor((this.Attack + this.Defense + this.Health) / 3);

        public double Attack { get; set; }

        public double Defense { get; set; }

        public double Health { get; set; }

        public string Element { get; set; }

        public bool IsBattleSetCard { get; set; }

        public string ImagePath => "/images/" + this.Name + "-" + this.Tire + ".jpg";
    }
}
