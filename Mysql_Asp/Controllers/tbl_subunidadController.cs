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
    public class tbl_subunidadController : Controller
    {
        private ModeloDatos db = new ModeloDatos();

        // GET: tbl_subunidad
        public ActionResult Index()
        {
            var tbl_subunidad = db.tbl_subunidad.Include(t => t.tbl_status).Include(t => t.tbl_unidad);
            return View(tbl_subunidad.ToList());
        }

        // GET: tbl_subunidad/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_subunidad tbl_subunidad = db.tbl_subunidad.Find(id);
            if (tbl_subunidad == null)
            {
                return HttpNotFound();
            }
            return View(tbl_subunidad);
        }

        // GET: tbl_subunidad/Create
        public ActionResult Create()
        {
            ViewBag.id_status = new SelectList(db.tbl_status, "id_status", "estatus");
            ViewBag.id_unidad = new SelectList(db.tbl_unidad, "id_unidad", "nombre");
            return View();
        }

        // POST: tbl_subunidad/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_resultado,nombre,id_unidad,id_status")] tbl_subunidad tbl_subunidad)
        {
            if (ModelState.IsValid)
            {
                db.tbl_subunidad.Add(tbl_subunidad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_status = new SelectList(db.tbl_status, "id_status", "estatus", tbl_subunidad.id_status);
            ViewBag.id_unidad = new SelectList(db.tbl_unidad, "id_unidad", "nombre", tbl_subunidad.id_unidad);
            return View(tbl_subunidad);
        }

        // GET: tbl_subunidad/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_subunidad tbl_subunidad = db.tbl_subunidad.Find(id);
            if (tbl_subunidad == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_status = new SelectList(db.tbl_status, "id_status", "estatus", tbl_subunidad.id_status);
            ViewBag.id_unidad = new SelectList(db.tbl_unidad, "id_unidad", "nombre", tbl_subunidad.id_unidad);
            return View(tbl_subunidad);
        }

        // POST: tbl_subunidad/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_resultado,nombre,id_unidad,id_status")] tbl_subunidad tbl_subunidad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_subunidad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_status = new SelectList(db.tbl_status, "id_status", "estatus", tbl_subunidad.id_status);
            ViewBag.id_unidad = new SelectList(db.tbl_unidad, "id_unidad", "nombre", tbl_subunidad.id_unidad);
            return View(tbl_subunidad);
        }

        // GET: tbl_subunidad/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_subunidad tbl_subunidad = db.tbl_subunidad.Find(id);
            if (tbl_subunidad == null)
            {
                return HttpNotFound();
            }
            return View(tbl_subunidad);
        }

        // POST: tbl_subunidad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_subunidad tbl_subunidad = db.tbl_subunidad.Find(id);
            db.tbl_subunidad.Remove(tbl_subunidad);
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
