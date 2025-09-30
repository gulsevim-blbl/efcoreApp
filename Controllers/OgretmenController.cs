using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers
{
    public class OgretmenController : Controller
    {
        private readonly DataContext _context;
        public OgretmenController(DataContext context)
        {
            _context = context;
        }
        //ogrencileri listlemek için

        public async Task<IActionResult> Index()
        {
            return View(await _context.Ogretmenler.ToListAsync());
        }
        //ogretmen Eklemek için Create metodu
        public IActionResult Create() //Formu getirir bu action
        {
            return View();

        }
         //ogretmen Eklemek için Create metodu
       
        [HttpPost]
        public async Task<IActionResult> Create(Ogretmen model) //Formu Karşılayacak olan action
        {
            _context.Ogretmenler.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        

        //ogretmen Güncellemek için Edit metodu
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ogretmen = await _context
                                .Ogretmenler
                                .FirstOrDefaultAsync(o => o.OgretmenId == id);
            
            if (ogretmen == null)
            {
                return NotFound();
            }
            return View(ogretmen);
        }

        //ogretmen Güncellemek için Edit metodu
        [HttpPost]
        [ValidateAntiForgeryToken] //side Attacks için güvenlik önlemi
        public async Task<IActionResult> Edit(int? id, Ogretmen model)
        {
            if (id != model.OgretmenId)
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
                    if (!_context.Ogretmenler.Any(o => o.OgretmenId == model.OgretmenId))
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

    }
}