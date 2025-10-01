using efcoreApp.Data;
using efcoreApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var kurslar = await _context.Kurslar.Include(k => k.Ogretmen).ToListAsync();
            return View(kurslar);
        }
        //Kurs Eklemek için Create metodu
        public async Task<IActionResult> Create()
        {
            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(), "OgretmenId", "AdSoyad");
            return View();
        }
        //Kurs Eklemek için Create metodu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KursViewModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Kurslar.Add(new Kurs() { KursId = model.KursId, Baslik = model.Baslik, OgretmenId = model.OgretmenId });
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(), "OgretmenId", "AdSoyad");
            return View(model);
           
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

            var vm = new KursViewModel
            {
                KursId = kurs.KursId,
                Baslik = kurs.Baslik,
                OgretmenId = kurs.OgretmenId,
                KursKayitlari = kurs.KursKayitlari
            };

            ViewBag.Ogretmenler = new SelectList(
                await _context.Ogretmenler.ToListAsync(),
                "OgretmenId",
                "AdSoyad",
                kurs.OgretmenId
            );

            return View(vm);

        }


        [HttpPost]
        [ValidateAntiForgeryToken] // CSRF için güvenlik
        public async Task<IActionResult> Edit(int? id, KursViewModel model)
        {
            if (id != model.KursId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var kurs = await _context.Kurslar.FindAsync(model.KursId);
                    if (kurs == null) return NotFound();

                    kurs.Baslik = model.Baslik;
                    kurs.OgretmenId = model.OgretmenId;

                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
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
            }

            // Hata olursa tekrar öğretmen listesini doldur
            ViewBag.Ogretmenler = new SelectList(
                await _context.Ogretmenler.ToListAsync(),
                "OgretmenId",
                "AdSoyad",
                model.OgretmenId
            );

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