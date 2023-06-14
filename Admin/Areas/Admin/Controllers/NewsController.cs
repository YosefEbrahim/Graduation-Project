using Admin.Areas.Admin.Repository;
using Admin.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models.Models;
using System.Data;
using System.Security.Claims;

namespace Admin.Areas.Admin.Controllers
{

    public class NewsController : GenericController
    {
        private readonly INewsService _service;
        private readonly IGlobal _global;

        public NewsController(INewsService service,IGlobal global)
        {
            this._service = service;
            this._global = global;
        }
        public IActionResult Index()
        {
            var result = _service.GetALL().ToList();
            return View(result);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(PostNewViewModel model)
        {
            var input = new NewsViewModels
            {
                AdminId =await _global.GetAdminIdAsync(this.User.FindFirstValue(ClaimTypes.NameIdentifier)),
                Body = model.Body,
                CreateTime = DateTime.UtcNow.AddHours(3),
                Title=model.Title,
            };
            using (var dataStream = new MemoryStream())
            {
                await model.Image.CopyToAsync(dataStream);
                
                input.Image= dataStream.ToArray();
            }
            await _service.CreateAsync(input);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteAsync(string Id)
        {
            if(!String.IsNullOrEmpty(Id))
            {
                var item = await _service.GetbyIdAsync(Id);
                return View(item);
            }
            return View();
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteItem(string Id)
        {
            if (!String.IsNullOrEmpty(Id))
            {
               await _service.DeleteAsync(Id);
                return RedirectToAction("Index");
            }

                return View();
        }
        public async Task<IActionResult> Details(string Id)
        {
            if (!String.IsNullOrEmpty(Id))
            {
                var item = await _service.GetbyIdAsync(Id);
                return View(item);
            }
            return View();
        }
        public async Task<IActionResult> Edit(string Id)
        {
            if (!String.IsNullOrEmpty(Id))
            {
                var item = await _service.GetbyIdAsync(Id);
                return View(item);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string Id,PostNewViewModel model)
        {
            if (!String.IsNullOrEmpty(Id))
            {
                var item = await _service.GetbyIdAsync(Id);
                item.Title = model.Title;
                item.Body = model.Body;
                item.CreateTime = DateTime.UtcNow.AddHours(3);
                using (var dataStream = new MemoryStream())
                {
                    await model.Image.CopyToAsync(dataStream);

                    item.Image = dataStream.ToArray();
                }
               await _service.UpdateAsync(item);
                return RedirectToAction("Index");
            }
            return View();

        }


    }
}
