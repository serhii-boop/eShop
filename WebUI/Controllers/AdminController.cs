using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        IWearRepository repository;

        public AdminController(IWearRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Wears);
        }

        public ViewResult Edit(int wearId)
        {
            Wear wear = repository.Wears.FirstOrDefault(b => b.WearId == wearId);

            return View(wear);
        }
        
        [HttpPost]
        public ActionResult Edit(Wear wear, HttpPostedFileBase image = null)
        {

            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    wear.ImageMimeType = image.ContentType;
                    wear.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(wear.ImageData, 0, image.ContentLength);
                }
                repository.SaveWear(wear);
                TempData["message"] = string.Format("Зміна \"{0}\" була збережена", wear.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(wear);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Wear());
        }


        [HttpPost]
        public ActionResult Delete(int wearId)
        {
            Wear deletedGame = repository.DeleteWear(wearId);
            if (deletedGame != null)
            {
                TempData["message"] = string.Format("Товар \"{0}\" був видалений",
                    deletedGame.Name);
            }
            return RedirectToAction("Index");
        }


        public ActionResult Report()
        {
            return View();
        }

    }
}