﻿using Business.Concrete;
using DataAccess.EntityFramework;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class TitleController : Controller
    {
        // GET: Title
        TitleManager titleManager = new TitleManager(new EfTitleDal());
        CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
        AuthorManager authorManager = new AuthorManager(new EfAuthorDal());
        public ActionResult Index()
        {
            var titleValues = titleManager.GetList();
            return View(titleValues);
        }
        [HttpGet]
        public ActionResult AddTitle()
        {
            List<SelectListItem> categoryValues = (from x in categoryManager.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryId.ToString()
                                                   }).ToList();
            ViewBag.vlc = categoryValues;

            List<SelectListItem> authorValues = (from x in authorManager.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.AuthorName+ " "+ x.AuthorSurname,
                                                       Value = x.AuthorId.ToString()
                                                   }).ToList();
            ViewBag.aut = authorValues;
            return View();
        }
        [HttpPost]
        public ActionResult AddTitle(Title title)
        {
            title.TitleDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            titleManager.AddTitle(title);
            return RedirectToAction("Index");
        }
    }
}