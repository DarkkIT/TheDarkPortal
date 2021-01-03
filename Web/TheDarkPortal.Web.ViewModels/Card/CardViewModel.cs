namespace TheDarkPortal.Web.ViewModels.Card
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using TheDarkPortal.Data.Models.Cards;
    using TheDarkPortal.Services.Mapping;

    public class CardViewModel : IMapFrom<CardLevelOne>
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
    }
}
