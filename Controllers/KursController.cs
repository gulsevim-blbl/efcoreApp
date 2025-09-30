using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers
{
    public class KursController : Controller
    {
        private readonly DataContext _context;
        //Kursları getirmek için constructor
        public KursController(DataContext context)
        {
            _context = context;
        }
        //Kurs Listelemek için Index metodu
        public async Task<IActionResult> Index()
        {
            var kurslar = await _context.Kurslar.ToListAsync();
            return View(kurslar);
        }
        //Kurs Eklemek için Create metodu
        public IActionResult Create()
        {
            return View();
        }
        //Kurs Eklemek için Create metodu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Kurs model)
        {
            _context.Kurslar.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        //Kurs Güncellemek için Edit metodu
        [HttpGet]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var kurs = await _context
                        .Kurslar
                        .Include(k => k.KursKayitlari)
                        .ThenInclude(k => k.Ogrenci)
                        .FirstOrDefaultAsync(k => k.KursId == id);
            if (kurs == null)
            {
                return NotFound();
            }
            return View(kurs);

        }


        [HttpPost]
        [ValidateAntiForgeryToken] //side Attacks için güvenlik önlemi
        public async Task<IActionResult> Edit(int? id, Kurs model)
        {
            if (id != model.KursId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (!_context.Kurslar.Any(k => k.KursId == model.KursId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(model);

        }

       //Kursu silmeden önce verdiğimiz bir fırsat ver
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Kurs = await _context.Kurslar.FindAsync(id);
            if (Kurs == null)
            {
                return NotFound();
            }
            return View(Kurs);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            var kurs = await _context.Kurslar.FindAsync(id);
            if (kurs == null)
            {
                return NotFound();
            }
            _context.Kurslar.Remove(kurs);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}