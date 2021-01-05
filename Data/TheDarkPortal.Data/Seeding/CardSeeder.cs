namespace TheDarkPortal.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TheDarkPortal.Data.Models.Cards;

    public class CardSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.CardsLevelOne.Any())
            {
                return;
            }

            var cardList = new List<CardLevelOne>();

            var card1 = new CardLevelOne
            {
                Name = "Dragon",
                Tire = 1,
                Level = 1,
                LevelPrice = 50,
                Power = (2000 + 2000 + 5000) / 3,
                Attack = 2000,
                Defense = 2000,
                Health = 5000,
                Element = "Air",
                Price = 250,
            };

            cardList.Add(card1);

            var card101 = new CardLevelOne
            {
                Name = "Demon",
                Tire = 1,
                Level = 1,
                LevelPrice = 50,
                Power = (2200 + 2000 + 4000) / 3,
                Attack = 2200,
                Defense = 2000,
                Health = 4000,
                Element = "Dark",
                Price = 2250,
            };

            cardList.Add(card101);

            var card2 = new CardLevelOne
            {
                Name = "Witch",
                Tire = 1,
                Level = 1,
                LevelPrice = 50,
                Power = (800 + 800 + 3000) / 3,
                Attack = 800,
                Defense = 800,
                Health = 3000,
                Element = "Magic",
                Price = 250,
            };

            cardList.Add(card2);

            var card3 = new CardLevelOne
            {
                Name = "Wolf",
                Tire = 1,
                Level = 1,
                LevelPrice = 50,
                Power = (700 + 700 + 4000) / 3,
                Attack = 700,
                Defense = 700,
                Health = 4000,
                Element = "Forest",
                Price = 250,
            };

            cardList.Add(card3);

            var card4 = new CardLevelOne
            {
                Name = "Paladin",
                Tire = 1,
                Level = 1,
                LevelPrice = 50,
                Power = (1700 + 1700 + 4000) / 3,
                Attack = 1700,
                Defense = 1700,
                Health = 4000,
                Element = "Earth",
                Price = 250,
            };

            cardList.Add(card4);

            var card5 = new CardLevelOne
            {
                Name = "Warrior",
                Tire = 1,
                Level = 1,
                LevelPrice = 50,
                Power = (1700 + 1700 + 4000) / 3,
                Attack = 1700,
                Defense = 1700,
                Health = 4000,
                Element = "Earth",
                Price = 250,
            };

            cardList.Add(card5);

            await dbContext.CardsLevelOne.AddRangeAsync(cardList);
        }
    }
}
