using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Overtime_Core_WEB.OvertimeDto;
using OvertimeApi.DataAceess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Overtime_Core_WEB.Controllers
{
    public class OvertimeController : Controller
    {

        public async Task<IActionResult> Index()
        {

            var httpclient = new HttpClient();
            var httpresponse = await httpclient.GetAsync("https://localhost:44352/api/Overtime/all");
            var jsonstring = await httpresponse.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<Ovr>>(jsonstring);
            ViewBag.CountOvertimes = values.Count();
            return View(values);

        }

        [HttpGet]

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Add(Ovr p)
        {
            var httpclient = new HttpClient();
            var jsonstring = JsonConvert.SerializeObject(p);
            var stringcontent = new StringContent(jsonstring, Encoding.UTF8, "application/json");
            var responsemessage = await httpclient.PostAsync("https://localhost:44352/api/Overtime/create", stringcontent);
            if (responsemessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var httpclient = new HttpClient();
            var responsemessage = await httpclient.GetAsync("https://localhost:44352/api/Overtime/get/" + id);
            if (responsemessage.IsSuccessStatusCode)
            {
                var jsonstring = await responsemessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<Ovr>(jsonstring);
                return View(values);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Ovr p)
        {
            var httpclient = new HttpClient();
            var jsonstring = JsonConvert.SerializeObject(p);
            var content = new StringContent(jsonstring, Encoding.UTF8, "application/json");
            var responsemessage = await httpclient.PutAsync("https://localhost:44352/api/Overtime/edit", content);
            if (responsemessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(p);
        }



        public async Task<IActionResult> Delete(int id)
        {
            var httpclient = new HttpClient();
            var responsemessage = await httpclient.DeleteAsync("https://localhost:44352/api/Overtime/delete/" + id);
            if (responsemessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
