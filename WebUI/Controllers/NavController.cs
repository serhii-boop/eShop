﻿using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class NavController : Controller
    {
            private IWearRepository repository;

            public NavController(IWearRepository repo)
            {
                repository = repo;
            }

            public PartialViewResult Menu(string category = null)
            {
                ViewBag.SelectedCategory = category;

                IEnumerable<string> categories = repository.Wears
                    .Select(wear => wear.Category)
                    .Distinct()
                    .OrderBy(x => x);

                return PartialView(categories);
            }
        
    }
}