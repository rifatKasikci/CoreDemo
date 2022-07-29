using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CoreDemo.Controllers
{
    public class RegisterController : Controller
    {
        WriterManager writerManager = new WriterManager(new EfWriterRepository());

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Cities = ToListItem(GetCityList());
            return View();
        }

        [HttpPost]
        public IActionResult Index(Writer writer, string passwordRepeat, string writerCity)
        {
            WriterValidator writerValidator = new WriterValidator();
            ValidationResult result = writerValidator.Validate(writer);
            if (result.IsValid && writer.WriterPassword.Equals(passwordRepeat))
            {
                writer.WriterStatus = true;
                writer.WriterAbout = "Default explanation.";
                writerManager.AddWriter(writer);
                return RedirectToAction("Index", "Blog");
            }
            else if (!result.IsValid)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                ViewBag.Cities = ToListItem(GetCityList());
            } else
            {
                ModelState.AddModelError("WriterPassword", "Şifreler uyuşmuyor!");
                ViewBag.Cities = ToListItem(GetCityList());
            }
            return View();

        }

        private List<SelectListItem> ToListItem(string[] list)
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            foreach (var item in list)
            {
                listItems.Add(new()
                {
                    Text = item
                });
            }
            return listItems;
        }

        private string[] GetCityList()
        {
            return new string[] { "Ankara","İstanbul","Bursa","Elazığ","Erzurum","Adıyaman","Mardin","Şırnak","İzmir","Muğla","Diğer"};
        }
    }
}
