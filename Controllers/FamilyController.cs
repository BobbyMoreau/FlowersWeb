using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using flowers.web.ViewModel.Families;
using System.Text;
using flowers.web.Data;

[Route("families")]
    public class FamilyController: Controller
    {
         private readonly FlowersContext _context;
        private readonly JsonSerializerOptions _options;
         private readonly IHttpClientFactory _httpClient;
         private readonly string _baseUrl;
        public FamilyController(FlowersContext context, IConfiguration config, IHttpClientFactory httpClient )
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
            var response = await client.GetAsync($"{_baseUrl}/families");

            if(!response.IsSuccessStatusCode) return Content("What did you want?? It didn't succed");

            var json = await response.Content.ReadAsStringAsync();

            var families = JsonSerializer.Deserialize<IList<FamilyListView>>(json, _options);

            return View("Index", families);
        }

        [HttpGet("detail/{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            using var client = _httpClient.CreateClient();
            var response = await client.GetAsync($"{_baseUrl}/families/{id}");

            if(!response.IsSuccessStatusCode) return Content("did you want to se ONE family?? It didn't succed");

            var json = await response.Content.ReadAsStringAsync();
            var families = System.Text.Json.JsonSerializer.Deserialize<FamilyDetailView>(json, _options);

            return View("Detail", families);
        }


        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
             using var client = _httpClient.CreateClient();
            

            var families = new FamilyPostView();
            return View("create", families);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(FamilyPostView families)
        {
             if(!ModelState.IsValid) return View("Create", families);

            var addClient = _httpClient.CreateClient();
            
            
            var jsonFamily = JsonSerializer.Serialize(families);
            var content = new StringContent(jsonFamily, UnicodeEncoding.UTF8, "application/json");
               
            var response = await addClient.PostAsync($"{_baseUrl}/families", content);

            if (!response.IsSuccessStatusCode) return Content("Failed to add family");
 
            var json = await response.Content.ReadAsStringAsync();
                
                
                return RedirectToAction(nameof(Index));

        } 
        private async Task<IList<FamilyListView>> CreateList()
        {
            var families = await _context.Families
                .OrderBy(c => c.Name)
                .Select(m => new FamilyListView
                {
                    Id = m.Id,
                    Name = m.Name
                })
                .ToListAsync();

            return families;
        }
    }