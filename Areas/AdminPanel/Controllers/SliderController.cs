using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DAL;
using WebApp.Models;

namespace WebApp.Areas.AdminPanel.Controllers
{
  [Area("AdminPanel")]
    public class SliderController : Controller
    {
        private AppDbContext _context { get; }
        public SliderController(AppDbContext context)
        {

            _context = context;
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
        public IActionResult Create(Slide slide)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (slide.Photo.Length / 1024 > 200) {

                ModelState.AddModelError("Photo", "Image size error ");
                return View();
            
            }
            if (!slide.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "type error ");
                return View();
            }

            
            return Json(slide.Photo.FileName);
        }
    }
}
