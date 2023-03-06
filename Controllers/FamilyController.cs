/*

using System.Text.Json;
using flowers.web.Data;
using flowers.web.ViewModel.Families;
using flowers.web.ViewModel.Flowers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using flowers.web.Models;

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

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            using var client = _httpClient.CreateClient();
            var response = await client.GetAsync($"{_baseUrl}/families/{id}");

            if(!response.IsSuccessStatusCode) return Content("did you want to se ONE family?? It didn't succed");

            var json = await response.Content.ReadAsStringAsync();
            var families = System.Text.Json.JsonSerializer.Deserialize<FamilyDetailView>(json, _options);

            return View("Detail", families);
        }


    [HttpGet("{name}/flowers")]

        public async Task<IActionResult> ListFlowersInThisFamily(string name)
        {
            var result = await _context.Families
            .Select(fa => new {
                Name = fa.Name,
                Flowers = fa.Flowers.Select(fl => new {
                    Id = fl.Id,
                    Name = fl.Name,
                    Color = fl.Color,
                    Height = fl.Height
                }).ToList()
            }).SingleOrDefaultAsync(c => c.Name.ToUpper() == name.ToUpper());

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var families = await CreateList();

            var model = new FamilyPostView 
            {
                Families = families
            };
            return View("create", model);
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



    }*/
