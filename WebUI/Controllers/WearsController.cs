using WebUI.HtmlHelpers;
using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;
using Domain.Entities;

namespace WebUI.Controllers
{
    public class WearsController : Controller
    {
        // GET: Wears
        private IWearRepository repository;

        public int pageSize = 4;
        public WearsController(IWearRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(string category, int page = 1)
        {
            WearsListViewModel model = new WearsListViewModel
            {
                Wears = repository.Wears
               .Where(b => category == null || b.Category == category)
                .OrderBy(wear => wear.WearId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ?
                    repository.Wears.Count() :
                    repository.Wears.Where(wear => wear.Category == category).Count()
                },
                CurrentCategory = category
            };

            return View(model);

        }



        public FileContentResult GetImage(int wearId)
        {
            Wear wear = repository.Wears
                .FirstOrDefault(g => g.WearId == wearId);

            if (wear != null)
            {
                
                return File(wear.ImageData, wear.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}
