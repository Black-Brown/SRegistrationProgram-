using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SRegisterApp.Models;

namespace SRegisterApp.Frontend.Controllers
{
    public class StudentsController : Controller
    {
        private readonly HttpClient _httpClient;

        public StudentsController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5199/api/Students"); // ⚠️ Ajusta la URL de la API
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var students = await _httpClient.GetFromJsonAsync<List<StudentsViewModel>>("");
            return View(students);
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var student = await _httpClient.GetFromJsonAsync<StudentsViewModel>($"/{id}");
            if (student == null) return NotFound();
            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentsViewModel student)
        {
            if (!ModelState.IsValid) return View(student);

            var response = await _httpClient.PostAsJsonAsync("", student);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _httpClient.GetFromJsonAsync<StudentsViewModel>($"/{id}");
            if (student == null) return NotFound();
            return View(student);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StudentsViewModel student)
        {
            if (id != student.Id) return BadRequest();

            var response = await _httpClient.PutAsJsonAsync($"/{id}", student);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _httpClient.GetFromJsonAsync<StudentsViewModel>($"/{id}");
            if (student == null) return NotFound();
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"/{id}");
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return RedirectToAction(nameof(Delete), new { id });
        }
    }
}
