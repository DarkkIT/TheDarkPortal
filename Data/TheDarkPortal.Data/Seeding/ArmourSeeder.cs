namespace TheDarkPortal.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using TheDarkPortal.Data.Models.Equipment;

    public class ArmourSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.ArmorsLevelOne.Any())
            {
                return;
            }

            var armorList = new List<ArmorLevelOne>();

            var armor1 = new ArmorLevelOne
            {
                Name = "DragonArmor",
                Power = 0,
                Defense = 500,
                Health = 1000,
                Price = 300,
            };

            armorList.Add(armor1);

            var armor2 = new ArmorLevelOne
            {
                Name = "HunterArmor",
                Power = 500,
                Defense = 0,
                Health = 1000,
                Price = 300,
            };

            armorList.Add(armor2);

            await dbContext.ArmorsLevelOne.AddRangeAsync(armorList);
        }
    }
}
