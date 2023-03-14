using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using flowers.web.ViewModel.Flowers;
using flowers.web.Data;
using Microsoft.EntityFrameworkCore;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using System.Text;
using flowers.web.ViewModel.Families;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            var response = await client.GetAsync($"{_baseUrl}/families");

            if(!response.IsSuccessStatusCode) return Content("It didn't succed");


            var json = await response.Content.ReadAsStringAsync();


            var families = JsonSerializer.Deserialize<IList<FamilyPostView>>(json, _options);
            var listedFamilies = new List<SelectListItem>();
            foreach (var family in families) 
            {
                listedFamilies.Add(new SelectListItem {Value = family.Name, Text= family.Name});
            }
            var flowers = new FlowerPostView();
            flowers.Families = listedFamilies;

            return View("create", flowers);
            
        }

           [HttpPost("Create")]
         public async Task<IActionResult> Create(FlowerPostView flowers)
         {
             if(!ModelState.IsValid) return View("Create", flowers);

            var addClient = _httpClient.CreateClient();
            
            
            var jsonflower = JsonSerializer.Serialize(flowers);
            var content = new StringContent(jsonflower, UnicodeEncoding.UTF8, "application/json");
                    
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
