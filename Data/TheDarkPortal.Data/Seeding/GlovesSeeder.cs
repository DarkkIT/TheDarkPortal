namespace TheDarkPortal.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using TheDarkPortal.Data.Models.Equipment;

    public class GlovesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.GlovesLevelOne.Any())
            {
                return;
            }

            var glovesList = new List<GlovesLevelOne>();

            var gloves1 = new GlovesLevelOne
            {
                Name = "DragonGloves",
                Power = 0,
                Defense = 500,
                Health = 1000,
                Price = 300,
            };

            glovesList.Add(gloves1);

            var gloves2 = new GlovesLevelOne
            {
                Name = "HunterGloves",
                Power = 500,
                Defense = 0,
                Health = 1000,
                Price = 300,
            };

            glovesList.Add(gloves2);

            await dbContext.GlovesLevelOne.AddRangeAsync(glovesList);
        }
    }
}
