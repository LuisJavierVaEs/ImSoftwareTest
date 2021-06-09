using System;
using System.Net.Http;
using Newtonsoft.Json;
using ImSoftwareMVC.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace ImSoftwareMVC.Controllers
{
    public class UsuarioController : Controller
    {
        //----------------------------------------VISTA PARA VISUALIZAR CONSULTA DE PERSONAS-------------------------------
        // GET: UsuarioController
        public async Task<ActionResult> IndexAsync()
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("http://localhost:53291/api/usuarios");
            List<Usuario> lstUsers = JsonConvert.DeserializeObject<List<Usuario>>(json);
            return View(lstUsers);
        }

        //--------------------------------------------VISTA PARA EL REGISTRO DE PERSONAS------------------------------------
         // GET: UsuarioController/Create
        public async Task<ActionResult> Create()
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("http://localhost:53291/api/usuarios");
            List<Usuario> lstUsers = JsonConvert.DeserializeObject<List<Usuario>>(json);
            return View();
        }
        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario user)
        {
            try
            {
                var prod = JsonConvert.SerializeObject(user);
                var buffer = System.Text.Encoding.UTF8.GetBytes(prod);
                var bytecontent = new ByteArrayContent(buffer);
                bytecontent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                var httpClient = new HttpClient();
                var result = httpClient.PostAsync("http://localhost:53291/api/usuarios", bytecontent).Result;
                return RedirectToAction(nameof(Index));
            }
            catch{ return View(); }
        }
        //---------------------------------------------------------------------------------------------------------------------
    }
}