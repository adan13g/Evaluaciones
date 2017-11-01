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
    public class tbl_asignacionController : Controller
    {
        private ModeloDatos db = new ModeloDatos();

        // GET: tbl_asignacion
        public ActionResult Index()
        {
            var tbl_asignacion = db.tbl_asignacion.Include(t => t.tbl_carreras).Include(t => t.tbl_grupos).Include(t => t.tbl_materias).Include(t => t.tbl_profesores);
            return View(tbl_asignacion.ToList());
        }

        // GET: tbl_asignacion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_asignacion tbl_asignacion = db.tbl_asignacion.Find(id);
            if (tbl_asignacion == null)
            {
                return HttpNotFound();
            }
            return View(tbl_asignacion);
        }

        // GET: tbl_asignacion/Create
        public ActionResult Create()
        {
            ViewBag.id_carrera = new SelectList(db.tbl_carreras, "id_carrera", "carrera");
            ViewBag.id_grupos = new SelectList(db.tbl_grupos, "id_grupos", "grupos");
            ViewBag.siglema = new SelectList(db.tbl_materias, "siglema", "nombre");
            ViewBag.id_profesor = new SelectList(db.tbl_profesores, "id_profesor", "Profesor");
            return View();
        }

        // POST: tbl_asignacion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_asignacion,id_carrera,siglema,id_profesor,id_grupos")] tbl_asignacion tbl_asignacion)
        {
            if (ModelState.IsValid)
            {
                db.tbl_asignacion.Add(tbl_asignacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_carrera = new SelectList(db.tbl_carreras, "id_carrera", "carrera", tbl_asignacion.id_carrera);
            ViewBag.id_grupos = new SelectList(db.tbl_grupos, "id_grupos", "grupos", tbl_asignacion.id_grupos);
            ViewBag.siglema = new SelectList(db.tbl_materias, "siglema", "nombre", tbl_asignacion.siglema);
            ViewBag.id_profesor = new SelectList(db.tbl_profesores, "id_profesor", "Profesor", tbl_asignacion.id_profesor);
            return View(tbl_asignacion);
        }

        // GET: tbl_asignacion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_asignacion tbl_asignacion = db.tbl_asignacion.Find(id);
            if (tbl_asignacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_carrera = new SelectList(db.tbl_carreras, "id_carrera", "carrera", tbl_asignacion.id_carrera);
            ViewBag.id_grupos = new SelectList(db.tbl_grupos, "id_grupos", "grupos", tbl_asignacion.id_grupos);
            ViewBag.siglema = new SelectList(db.tbl_materias, "siglema", "nombre", tbl_asignacion.siglema);
            ViewBag.id_profesor = new SelectList(db.tbl_profesores, "id_profesor", "Profesor", tbl_asignacion.id_profesor);
            return View(tbl_asignacion);
        }

        // POST: tbl_asignacion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_asignacion,id_carrera,siglema,id_profesor,id_grupos")] tbl_asignacion tbl_asignacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_asignacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_carrera = new SelectList(db.tbl_carreras, "id_carrera", "carrera", tbl_asignacion.id_carrera);
            ViewBag.id_grupos = new SelectList(db.tbl_grupos, "id_grupos", "grupos", tbl_asignacion.id_grupos);
            ViewBag.siglema = new SelectList(db.tbl_materias, "siglema", "nombre", tbl_asignacion.siglema);
            ViewBag.id_profesor = new SelectList(db.tbl_profesores, "id_profesor", "Profesor", tbl_asignacion.id_profesor);
            return View(tbl_asignacion);
        }

        // GET: tbl_asignacion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_asignacion tbl_asignacion = db.tbl_asignacion.Find(id);
            if (tbl_asignacion == null)
            {
                return HttpNotFound();
            }
            return View(tbl_asignacion);
        }

        // POST: tbl_asignacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_asignacion tbl_asignacion = db.tbl_asignacion.Find(id);
            db.tbl_asignacion.Remove(tbl_asignacion);
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
