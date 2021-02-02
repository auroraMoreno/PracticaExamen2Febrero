using Microsoft.AspNetCore.Mvc;
using PracticaExamen2Febrero.Models;
using PracticaExamen2Febrero.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaExamen2Febrero.Controllers
{
    public class CochesController : Controller
    {
        IRepositoryCoches repo;
        public CochesController(IRepositoryCoches repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            List<Coche> coches = this.repo.GetCoches();
            return View(coches);
        }

        public IActionResult Details(int idcoche)
        {
            Coche coche = this.repo.BuscarCoche(idcoche);
            return View(coche);
        }

        public IActionResult Edit(int idcoche)
        {
            Coche coche = this.repo.BuscarCoche(idcoche);
            return View(coche);
        }

        [HttpPost]
        public IActionResult Edit(Coche coche)
        {
            this.repo.ModificarCoche
                (coche.IdCoche,coche.Marca,coche.Modelo,coche.Conductor,coche.Imagen);
            return RedirectToAction("Index");
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Coche coche)
        {
            this.repo.InsertarCoche(coche.Marca,coche.Modelo,coche.Conductor,coche.Imagen);
            return RedirectToAction("Index");
        }

        public IActionResult BuscarCochesModelo()
        {
            return View();
        }


        public IActionResult Delete(int idcoche)
        {
            this.repo.EliminarCoche(idcoche);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult BuscarCochesModelo(String modelo)
        {
            Coche coche = this.repo.BuscarCoche(modelo);
            return View(coche);
        }



    }
}
