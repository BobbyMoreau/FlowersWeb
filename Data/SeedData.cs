using System.Text.Json;
using flowers.web.Models;

namespace flowers.web.Data
{
    public static class SeedData
    {
        /*
        public static async Task LoadFamilies(FlowersContext context)
        {
            var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive = true
            };

            if(context.Families.Any()) return;

            var dataInJsonFile = System.IO.File.ReadAllText("Data/json/family.json");

            var families = JsonSerializer.Deserialize<List<FamilyModel>>(dataInJsonFile, options);

            if (families != null && families.Count > 0) 
            {
                    await context.Families.AddRangeAsync(families);
                    await context.SaveChangesAsync();
            }
        }*/


         public static async Task LoadFlowers(FlowersContext context)
        {
            var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive = true
            };

            if(context.Flowers.Any()) return;

            var dataInJsonFile = System.IO.File.ReadAllText("Data/json/flowers.json");

            var flowers = JsonSerializer.Deserialize<List<FlowerModel>>(dataInJsonFile, options);

            if (flowers != null && flowers.Count > 0) 
            {
                    await context.Flowers.AddRangeAsync(flowers);
                    await context.SaveChangesAsync();
            }
        }
    }
}