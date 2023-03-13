
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using flowers.web.ViewModel.Flowers;
using flowers.web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using flowers.web.Models;

using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using System.Text;
using System.Net.Http.Headers;

[Route("flowers")]
    public class FlowersController : Controller
    {
         private readonly FlowersContext _context;
         private readonly JsonSerializerOptions _options;
         private readonly IHttpClientFactory _httpClient;
         private readonly string _baseUrl;


        public FlowersController(FlowersContext context, IConfiguration config, IHttpClientFactory httpClient )
        {
            _httpClient = httpClient;          
            _context = context;
            _baseUrl = config.GetSection("apiSettings:baseUrl").Value;
            _options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};
        }

        [HttpGet("list")] 
        public async Task<IActionResult> Index()
        {
            using var client = _httpClient.CreateClient();
            var response = await client.GetAsync($"{_baseUrl}/flowers");

            if(!response.IsSuccessStatusCode) return Content("What did you want?? It didn't succed");

            var json = await response.Content.ReadAsStringAsync();

            var flowers = System.Text.Json.JsonSerializer.Deserialize<IList<FlowerListView>>(json, _options);

            return View("Index", flowers);
        }

        [HttpGet("detail/{id}")] 
        public async Task<IActionResult> Detail(int id)
        {
            using var client = _httpClient.CreateClient();
            var response = await client.GetAsync($"{_baseUrl}/flowers/{id}");

            if(!response.IsSuccessStatusCode) return Content("did you want to se ONE flower?? It didn't succed");

            var json = await response.Content.ReadAsStringAsync();

            var flowers = System.Text.Json.JsonSerializer.Deserialize<FlowerDetailView>(json, _options);

            return View("Detail", flowers);
        }


        [HttpGet("Name/{name}")] 
        public async Task<IActionResult> GetByName(string name)
        {
            
            var result = await _context.Flowers.SingleOrDefaultAsync(c => c.Name == name);

            return Ok(result);
        }

        
   

    [HttpGet("create")]

        public async Task<IActionResult> Create()
        {
            
            using var client = _httpClient.CreateClient();
        //var response = await client.GetAsync($"{_baseUrl}/flowers");

        //if(!response.IsSuccessStatusCode) return Content("Did you want to create?? It didn't succed");


        //var json = await response.Content.ReadAsStringAsync();


        //var flowers = JsonSerializer.Deserialize<FlowerPostView>(json, _options);
            var flowers = new FlowerPostView();

            return View("create", flowers);
            
        }

           [HttpPost("Create")]
         public async Task<IActionResult> Create(FlowerPostView flowers)
         {
             if(!ModelState.IsValid) return View("Create", flowers);

            var addClient = _httpClient.CreateClient();
            
            
            var jsonflower = JsonSerializer.Serialize(flowers);
            var content = new StringContent(jsonflower, UnicodeEncoding.UTF8, "application/json");
           
    //        var encoding = System.Text.Encoding.UTF8.GetBytes(jsonflower);
    //        var objektInBytes = new ByteArrayContent(encoding);
    //        objektInBytes.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            
            var response = await addClient.PostAsync($"{_baseUrl}/flowers", content);

            if (!response.IsSuccessStatusCode) return Content("Failed to add flower");
 
            var json = await response.Content.ReadAsStringAsync();
                
                
                return RedirectToAction(nameof(Index));

             } 

     private async Task<List<FlowerListView>> CreateList()
        {
                var flowers = await _context.Flowers
                .OrderBy(f => f.Name)
                .Select(b => new FlowerListView
                {
                    Id = b.Id,
                    Name = b.Name,
                    Color = b.Color,
                    Height = b.Height
                 })
                 .ToListAsync();

           return flowers;
         }
       
    }


             /* 
         
                 [HttpPost]
         public async Task<IActionResult> Create(FlowerPostView flower)
         {
             if(!ModelState.IsValid) return View("Create", flower);


            //var fam = await _context.Families.FindAsync(flower.Family);
            var fam = await _context.Families.SingleOrDefaultAsync(f => f.Id == flower.FamilyId);

             if (fam is not null)
             {
                var newFlower = new FlowerModel
                {
                    
                    Name = flower.Name,
                    Families = fam,
                    Color = flower.Color,
                    Height = flower.Height,
                    ImageUrl = "flowers.png"
                };
                 await _context.Flowers.AddAsync(newFlower);

                 if (await _context.SaveChangesAsync() > 0) 
                { 
                    return RedirectToAction(nameof(Index));

                }
             return View("Errors");

             }
             return View("Errors");
            
           
         }
         */

/*
              

     [HttpGet("create")]

        public async Task<IActionResult> Create()
        {
            
            using var client = _httpClient.CreateClient();
            var response = await client.GetAsync($"{_baseUrl}/flowers");

            if(!response.IsSuccessStatusCode) return Content("Did you want to create?? It didn't succed");

            //var json = await response.Content.ReadAsStreamAsync();
            var json = await response.Content.ReadAsStringAsync();
            //var json = await response.Content.ReadFromJsonAsync();

            var flowers = JsonSerializer.Deserialize<FlowerPostView>(json, _options);
    
            //var flowers = JsonSerializer.DeserializeAsyncEnumerable<FlowerPostView>(json, _options);

            return View("create", flowers);
            
            //Det handlar kanske om postningen till databasen? Hur är mitt objekt
            //Eller är min länk i menyn korrupt
            //Ta bort all koppling mellan tabellerna och lägg bara till en blomma.
        }

    [HttpPost]
    public async Task<IActionResult> Create(FlowerPostView flower)
    {
        if (!ModelState.IsValid) return View("Create", flower);

        var selectedFamily = await _context.Families.SingleOrDefaultAsync(c => c.Id == flower.FamilyId);


        if (selectedFamily is not null)
        {
            var flowerToAdd = new FlowerModel
            {
                
                Name = flower.Name,
                Height = flower.Height,
                Color = flower.Color,
                Family = selectedFamily,             
                ImageUrl = "flowers.png"
            };

            await _context.Flowers.AddAsync(flowerToAdd);

            if (await _context.SaveChangesAsync() > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View("Errors");
        }

        return View("Errors");
    }*/

