namespace TheDarkPortal.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using TheDarkPortal.Data.Models.Equipment;

    public class PantsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.PantsLevelOne.Any())
            {
                return;
            }

            var pantsList = new List<PantsLevelOne>();

            var pants1 = new PantsLevelOne
            {
                Name = "DragonPants",
                Power = 0,
                Defense = 500,
                Health = 1000,
                Price = 300,
            };

            pantsList.Add(pants1);

            var pants2 = new PantsLevelOne
            {
                Name = "HunterPants",
                Power = 500,
                Defense = 0,
                Health = 1000,
                Price = 300,
            };

            pantsList.Add(pants2);

            await dbContext.PantsLevelOne.AddRangeAsync(pantsList);
        }
    }
}
