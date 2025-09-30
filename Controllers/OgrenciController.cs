using System.Threading.Tasks;
using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers
{
    public class OgrenciController : Controller
    {
        private readonly DataContext _context;
        public OgrenciController(DataContext context)
        {
            _context = context;
        }
        //ogrencileri listlemek için

        public async Task<IActionResult> Index()
        {
            return View(await _context.Ogrenciler.ToListAsync());
        }

        public IActionResult Create() //Formu getirir bu action
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create(Ogrenci model) //Formu Karşılayacak olan action
        {
            _context.Ogrenciler.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ogr = await _context
            .Ogrenciler
            .Include(o => o.KursKayitlari)
            .ThenInclude(o => o.Kurs) //gitmiş olduğumuz modelden başka bir modele geçiş yapacağımız içini yani doğrudan öğrencilerle bağlantılı olan modle gitmediğimiz için theninclude kullanılır.
            .FirstOrDefaultAsync(o => o.OgrenciId == id);
            //ogrenciler modelinin id sine göre arama yapıyoruz buna alternatif olarakta
            //var ogr = await _context.Ogrenciler.FirstOrDefaultAsync(o => o.OgrenciId == id); bu da sadece id değil başka bir kritere göre de arama yapabiliriz. İlk eşleşen ilk kaydı geriye gönderir.
            if (ogr == null)
            {
                return NotFound();
            }
            return View(ogr);
        }


        [HttpPost]
        [ValidateAntiForgeryToken] //side Attacks için güvenlik önlemi
        public async Task<IActionResult> Edit(int? id, Ogrenci model)
        {
            if (id != model.OgrenciId)
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
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Ogrenciler.Any(o => o.OgrenciId == model.OgrenciId))
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



        //Kullanıcıya silmeden önce verdiğimiz bir fırsat ver
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ogrenci = await _context.Ogrenciler.FindAsync(id);
            if (ogrenci == null)
            {
                return NotFound();
            }
            return View(ogrenci);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            var ogrenci = await _context.Ogrenciler.FindAsync(id);
            if (ogrenci == null)
            {
                return NotFound();
            }
            _context.Ogrenciler.Remove(ogrenci);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


    }
}