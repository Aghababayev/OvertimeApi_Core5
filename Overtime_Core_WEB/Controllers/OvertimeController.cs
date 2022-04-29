﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Overtime_Core_WEB.OvertimeDto;
using OvertimeApi.DataAceess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Overtime_Core_WEB.Controllers
{
    public partial class OvertimeController : Controller
    {

        public async Task<IActionResult> Index()
        {

            var httpclient = new HttpClient();
            var httpresponse = await httpclient.GetAsync("https://localhost:44352/api/Overtime/all");
            var jsonstring = await httpresponse.Content.ReadAsStringAsync();
            var values=JsonConvert.DeserializeObject<List<Ovr>>(jsonstring);
           ViewBag.CountOvertimes=values.Count();   
            return View(values);
            
        }
    
        [HttpGet]
     
        public IActionResult Add()
        {
            return View();
        }




        public async Task <IActionResult> Delete(int id)
        {
            var httpclient=new HttpClient();
            var responsemessage = await httpclient.DeleteAsync("https://localhost:44352/api/Overtime/delete/" + id);
            if (responsemessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();   
        }

    }
}
