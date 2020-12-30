namespace TheDarkPortal.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using TheDarkPortal.Data.Models.Equipment;

    public class HelmetSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.HelmetsLevelOne.Any())
            {
                return;
            }

            var helmetList = new List<HelmetLevelOne>();

            var helmet1 = new HelmetLevelOne
            {
                Name = "DragonHelmet",
                Power = 0,
                Defense = 500,
                Health = 1000,
                Price = 300,
            };

            helmetList.Add(helmet1);

            var helmet2 = new HelmetLevelOne
            {
                Name = "HunterHelmet",
                Power = 500,
                Defense = 0,
                Health = 1000,
                Price = 300,
            };

            helmetList.Add(helmet2);

            await dbContext.HelmetsLevelOne.AddRangeAsync(helmetList);
        }
    }
}
