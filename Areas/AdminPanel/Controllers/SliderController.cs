using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DAL;
using WebApp.Helpers;
using WebApp.Models;

namespace WebApp.Areas.AdminPanel.Controllers
{
  [Area("AdminPanel")]
    public class SliderController : Controller
    {
        private AppDbContext _context { get; }
        private IWebHostEnvironment _env { get; }
        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {

            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.Slides);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slide slide)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (slide.Photo.CheckFileSize(200)) {

                ModelState.AddModelError("Photo", "Image size error ");
                return View();
            
            }
            if (!slide.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "type error ");
                return View();
            }

        
            slide.Url = await slide.Photo.SaveFileAsync(_env.WebRootPath,"img");

            await _context.Slides.AddAsync(slide);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id==null)
            {
                return BadRequest();

            }
            var slider = _context.Slides.Find(id);
            if (slider==null)
            {
                return NotFound();
            }
           var path= Helper.GetPath(_env.WebRootPath, "img", slider.Url);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);

            }
            _context.Slides.Remove(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
       

        public  async  Task< IActionResult> Update(int? Id, Slide slide)
        {
            if (Id == null)
            {
                return BadRequest();

            }
            
            Slide Dbslider = _context.Slides.Find(Id);
            if (Dbslider == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (slide.Photo.CheckFileSize(200))
            {

                ModelState.AddModelError("Photo", "Image size error ");
                return View();

            }
            if (!slide.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "type error ");
                return View();
            }
            var path = Helper.GetPath(_env.WebRootPath, "img", Dbslider.Url);

                Slide newSlide = new Slide
                {

                   Url = slide.Url

                };
               _context.Slides.Add(newSlide);
            
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);

            }
            
            Dbslider.Url = slide.Url;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));



        }
    }
}
