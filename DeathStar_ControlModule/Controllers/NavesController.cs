using DeathStar_ControlModule.Data;
using DeathStar_ControlModule.Models;
using DeathStar_ControlModule.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeathStar_ControlModule.Controllers {
    public class NavesController : Controller
    {
        private readonly DeathStarContext _context;
        private const string URL_NAVES = "http://swapi.dev/api/starships/";

        public NavesController(DeathStarContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Sync()
        {
            var navesDelete = await _context.Naves.ToListAsync();

            foreach (var nave in navesDelete)
            {
                _context.Naves.Remove(nave);
            }

            await _context.SaveChangesAsync();

            var httpClient = new HttpClient();
            var lista = new List<NaveViewModel>();
            APIResultsModel<NaveViewModel> resultadoApi = null;

            do
            {
                resultadoApi = await httpClient.GetFromJsonAsync<APIResultsModel<NaveViewModel>>(resultadoApi?.Next ?? URL_NAVES);
                lista.AddRange(resultadoApi.Results);
            } while (resultadoApi.Next != null);

            var naves = lista.Select(item => new Nave
            {
                Nome = item.Name,
                Carga = item.Carga,
                Classe = item.Starship_Class,
                Modelo = item.Model,
                Passageiros = item.Passageiros
            }).ToList();

            _context.AddRange(naves);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        // GET: Books
        public async Task<IActionResult> Index()
        {
            var naves = await _context.Naves.ToListAsync();

            return View(naves);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var nave = await _context.Naves.Include(s => s.Pilotos).ThenInclude(s => s.Planetas).AsNoTracking().FirstOrDefaultAsync(b => b.NaveId == id);

            if (nave == null)
                return NotFound();
            else
                return View(nave);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Modelo,Passageiros,Carga,Classe")] Nave nave)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nave);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nave);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var nave = await _context.Naves.SingleOrDefaultAsync(b => b.NaveId == id);

            if (nave == null)
                return NotFound();
            else
                return View(nave);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Nave nave)
        {
            if (ModelState.IsValid)
            {
                _context.Update(nave);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
                return View(nave);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nave = await _context.Naves
                .FirstOrDefaultAsync(m => m.NaveId == id);
            if (nave == null)
            {
                return NotFound();
            }

            return View(nave);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nave = await _context.Naves.FindAsync(id);
            _context.Naves.Remove(nave);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Naves.Any(e => e.NaveId == id);
        }
    }
}