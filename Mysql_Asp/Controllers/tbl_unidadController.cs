using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mysql_Asp.Models;

namespace Mysql_Asp.Controllers
{
    public class tbl_unidadController : Controller
    {
        private ModeloDatos db = new ModeloDatos();

        // GET: tbl_unidad
        public ActionResult Index()
        {
            var tbl_unidad = db.tbl_unidad.Include(t => t.tbl_materias).Include(t => t.tbl_status);
            return View(tbl_unidad.ToList());
        }

        // GET: tbl_unidad/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_unidad tbl_unidad = db.tbl_unidad.Find(id);
            if (tbl_unidad == null)
            {
                return HttpNotFound();
            }
            return View(tbl_unidad);
        }

        // GET: tbl_unidad/Create
        public ActionResult Create()
        {
            ViewBag.siglema = new SelectList(db.tbl_materias, "siglema", "nombre");
            ViewBag.id_status = new SelectList(db.tbl_status, "id_status", "estatus");
            return View();
        }

        // POST: tbl_unidad/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_unidad,nombre,siglema,id_status")] tbl_unidad tbl_unidad)
        {
            if (ModelState.IsValid)
            {
                db.tbl_unidad.Add(tbl_unidad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.siglema = new SelectList(db.tbl_materias, "siglema", "nombre", tbl_unidad.siglema);
            ViewBag.id_status = new SelectList(db.tbl_status, "id_status", "estatus", tbl_unidad.id_status);
            return View(tbl_unidad);
        }

        // GET: tbl_unidad/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_unidad tbl_unidad = db.tbl_unidad.Find(id);
            if (tbl_unidad == null)
            {
                return HttpNotFound();
            }
            ViewBag.siglema = new SelectList(db.tbl_materias, "siglema", "nombre", tbl_unidad.siglema);
            ViewBag.id_status = new SelectList(db.tbl_status, "id_status", "estatus", tbl_unidad.id_status);
            return View(tbl_unidad);
        }

        // POST: tbl_unidad/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_unidad,nombre,siglema,id_status")] tbl_unidad tbl_unidad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_unidad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.siglema = new SelectList(db.tbl_materias, "siglema", "nombre", tbl_unidad.siglema);
            ViewBag.id_status = new SelectList(db.tbl_status, "id_status", "estatus", tbl_unidad.id_status);
            return View(tbl_unidad);
        }

        // GET: tbl_unidad/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_unidad tbl_unidad = db.tbl_unidad.Find(id);
            if (tbl_unidad == null)
            {
                return HttpNotFound();
            }
            return View(tbl_unidad);
        }

        // POST: tbl_unidad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_unidad tbl_unidad = db.tbl_unidad.Find(id);
            db.tbl_unidad.Remove(tbl_unidad);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
